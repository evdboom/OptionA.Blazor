namespace OptionA.Blazor.Blog.Code.Parsers
{
    /// <summary>
    /// Abstract base class for code parsers
    /// </summary>
    public abstract class ParserBase : ICodeParser
    {
        /// <inheritdoc/>
        public abstract CodeLanguage Language { get; }

        /// <summary>
        /// Supported markers
        /// </summary>
        protected readonly Dictionary<string, MarkerType> _markers = new()
        {
            { "*S*", MarkerType.Selection },
            { "*C*", MarkerType.Class },
            { "*I*", MarkerType.Interface },
            { "*E*", MarkerType.Enum },
            { "*T*", MarkerType.Struct },
        };

        /// <summary>
        /// The string starters for determinging words
        /// </summary>
        protected abstract Dictionary<string, WordTypeModel> StringStarters { get; }

        /// <summary>
        /// Array of special characters for determining words
        /// </summary>
        protected abstract char[] Specials { get; }

        /// <summary>
        /// list of methods to determine the correct CodeTypes (other then strings and comments)
        /// </summary>
        protected List<Func<string, string, string, CodeType>> _partCheckers = [];

        /// <summary>
        /// gets the parts of the given code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="newLine"></param>
        /// <returns></returns>
        protected virtual IEnumerable<(string Part, CodeType Type, MarkerType Marker)> GetParts(string code, string newLine)
        {           
            var current = string.Empty;
            var previous = string.Empty;
            var activeMarkers = MarkerType.None;
            WordTypeModel? incompleteModel = null;
            var endedInside = false;
            while (!string.IsNullOrEmpty(code))
            {
                var word = FindNextWord(code, incompleteModel, newLine, out WordTypeModel wordType, out bool incomplete);
                code = RemoveFromStart(code, word);

                if (wordType.Type == WordType.Marker)
                {
                    var marker = _markers[word];
                    if (!string.IsNullOrEmpty(current))
                    {
                        yield return (current, CodeType.Text, activeMarkers);
                        current = string.Empty;
                    }

                    if (activeMarkers.HasFlag(marker))
                    {
                        activeMarkers &= ~marker;
                    }
                    else
                    {
                        activeMarkers |= marker;
                    }

                    continue;
                }

                incompleteModel = incomplete
                    ? wordType
                    : null;

                switch (wordType.Type)
                {
                    case WordType.NewLine:
                        current += word;
                        previous = current;
                        break;
                    case WordType.String:
                        if (!string.IsNullOrEmpty(current))
                        {
                            yield return (current, CodeType.Text, activeMarkers);
                            current = string.Empty;
                        }
                        previous = word;
                        yield return (word, CodeType.String, activeMarkers);
                        break;
                    case WordType.Interpolated:
                        if (!string.IsNullOrEmpty(current))
                        {
                            yield return (current, CodeType.Text, activeMarkers);
                            current = string.Empty;
                        }
                        foreach (var (part, type, inside) in ParseInterpolatedString(word, endedInside, newLine))
                        {
                            endedInside = inside;
                            previous = part;
                            yield return (part, type, activeMarkers);
                        }
                        break;
                    case WordType.Comment:
                        if (!string.IsNullOrEmpty(current))
                        {
                            yield return (current, CodeType.Text, activeMarkers);
                            current = string.Empty;
                        }
                        previous = word;
                        yield return (word, CodeType.Comment, activeMarkers);
                        break;
                    case WordType.Other:
                        var found = false;
                        foreach (var checker in _partCheckers)
                        {
                            var type = checker(previous, word, code);
                            found = type != CodeType.Text;

                            if (found)
                            {
                                if (!string.IsNullOrEmpty(current))
                                {
                                    yield return (current, CodeType.Text, activeMarkers);
                                    current = string.Empty;
                                }
                                previous = word;
                                yield return (word, type, activeMarkers);
                                break;
                            }
                        }

                        if (!found)
                        {
                            current += word;
                            previous = current;
                        }
                        break;
                }
            }
            if (!string.IsNullOrEmpty(current))
            {
                yield return (current, CodeType.Text, activeMarkers);
            }
        }

        /// <summary>
        /// Try to parse an interpolated string
        /// </summary>
        /// <param name="word"></param>
        /// <param name="beginInside"></param>
        /// <param name="newLine"></param>
        /// <returns></returns>
        protected virtual IEnumerable<(string Part, CodeType Type, bool Inside)> ParseInterpolatedString(string word, bool beginInside, string newLine)
        {
            var interpolated = word.Split('{', '}');
            var inside = beginInside;
            for (int i = 0; i < interpolated.Length; i++)
            {
                var part = interpolated[i];
                var last = i == interpolated.Length - 1;

                if (inside)
                {
                    var start = beginInside
                        ? string.Empty
                        : "{";
                    var end = last
                        ? string.Empty
                        : "}";

                    var insidePart = $"{start}{part}{end}";
                    beginInside = false;

                    foreach (var p in GetParts(insidePart, newLine))
                    {
                        yield return (p.Part, p.Type, last);
                    }
                }
                else
                {
                    yield return (part, CodeType.String, false);
                }
                inside = !inside;
            }
        }

        /// <summary>
        /// returns the given text with the to removed removed from the start, if it is the start.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="toRemove"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        protected static string RemoveFromStart(string text, string toRemove)
        {
            if (!text.StartsWith(toRemove))
            {
                throw new ArgumentException($"{toRemove} is not the start of {text})");
            }

            return text[toRemove.Length..];
        }

        /// <summary>
        /// Finds the substring up untill the searchvalue, returns the original string if the value was not found
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        protected static string FindTillValue(string text, int start, string searchValue)
        {
            var next = text.IndexOf(searchValue, start);
            if (next < 0)
            {
                return text;
            }
            var untill = next + searchValue.Length;
            var result = text[..untill];
            return result;
        }

        /// <summary>
        /// Finds the next word in the given string
        /// </summary>
        /// <param name="code"></param>
        /// <param name="incompleteModel"></param>
        /// <param name="newLine"></param>
        /// <param name="wordType"></param>
        /// <param name="incomplete"></param>
        /// <returns></returns>
        protected virtual string FindNextWord(string code, WordTypeModel? incompleteModel, string newLine, out WordTypeModel wordType, out bool incomplete)
        {
            incomplete = false;
            if (string.IsNullOrEmpty(code))
            {
                wordType = WordTypeModel.Empty;
                return string.Empty;
            }

            if (code.StartsWith(newLine))
            {
                wordType = WordTypeModel.NewLine;
                return newLine;
            }

            var marker = _markers.Keys.FirstOrDefault(code.StartsWith);
            if (!string.IsNullOrEmpty(marker))
            {
                wordType = WordTypeModel.Marker;
                return marker;
            }

            if (incompleteModel is not null)
            {
                wordType = incompleteModel;
            }
            else
            {
                var starter = StringStarters.FirstOrDefault(s => code.StartsWith(s.Key));
                wordType = starter.Value ?? WordTypeModel.Empty;
            }

            var word = string.Empty;
            if (wordType.Type != WordType.Other)
            {
                var ender = string.Equals(wordType.Ender, Environment.NewLine)
                    ? newLine
                    : wordType.Ender;
                word = FindTillValue(code, wordType.SearchFromIndex, ender);
            }
            else
            {
                var firstChar = code[0];
                var space = firstChar == ' ';
                var special = Specials.Contains(firstChar);
                word += firstChar;

                var found = false;
                var counter = 1;
                while (!found && counter < code.Length)
                {
                    var c = code[counter];
                    var isSpecial = Specials.Contains(c);
                    if (c == '\n')
                    {
                        found = true;
                    }
                    else if (space && c != ' ')
                    {
                        found = true;
                    }
                    else if (!space && c == ' ')
                    {
                        found = true;
                    }
                    else if ((special && !isSpecial) || (!special && isSpecial))
                    {
                        found = true;
                    }
                    else
                    {
                        word += c;
                    }
                    counter++;
                }
            }

            if (_markers.Keys.Any(word.Contains))
            {
                incomplete = true;
            }

            return word
                .Split(_markers.Keys.ToArray(), StringSplitOptions.None)
                .FirstOrDefault() ?? string.Empty;
        }

        /// <inheritdoc/>
        public IEnumerable<IContent> Parse(string? content, string? newLine)
        {
            return GetParts(content ?? string.Empty, newLine ?? Environment.NewLine)
                .Select(ToContent);
        }

        private IContent ToContent((string Part, CodeType Type, MarkerType Marker) part)
        {
            var result = new InlineContent
            {
                Content = part.Part,
                NotMarkdown = true
            };
            
          
            if (part.Marker.HasFlag(MarkerType.Selection))
            {
                if (result.Attributes.ContainsKey("opta-code-marker"))
                {
                    result.Attributes["opta-code-marker"] += " selected";
                }
                else
                {
                    result.Attributes["opta-code-marker"] = "selected";
                }
            }
            if (part.Type == CodeType.Text)
            {                
                if (part.Marker.HasFlag(MarkerType.Class))
                {
                    result.Attributes["opta-code"] = MarkerType.Class.ToAttribute();
                }
                else if (part.Marker.HasFlag(MarkerType.Interface))
                {
                    result.Attributes["opta-code"] = MarkerType.Interface.ToAttribute();
                }
                else if (part.Marker.HasFlag(MarkerType.Enum))
                {
                    result.Attributes["opta-code"] = MarkerType.Enum.ToAttribute();
                }
                else if (part.Marker.HasFlag(MarkerType.Struct))
                {
                    result.Attributes["opta-code"] = MarkerType.Struct.ToAttribute();
                }
                else
                {
                    result.Attributes["opta-code"] = part.Type.ToAttribute();
                }
            }
            else
            {
                result.Attributes["opta-code"] = part.Type.ToAttribute();
            }

            return result;
        }
    }
}
