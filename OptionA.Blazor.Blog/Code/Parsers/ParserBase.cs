namespace OptionA.Blazor.Blog.Parsers
{
    /// <summary>
    /// base class for code parsers   
    /// </summary>
    public abstract class ParserBase : IParser
    {
        /// <summary>
        /// Supported markers
        /// </summary>
        protected readonly Dictionary<string, MarkerType> _markers = new()
        {
            { "*M*", MarkerType.Selection }
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
        /// list of methofs to determine the correct codeparts (other then strings and comments)
        /// </summary>
        protected List<Func<string, string, string, CodePart>> _partCheckers = new(); 

        /// <inheritdoc/>
        public virtual IEnumerable<(string Part, CodePart Type, MarkerType Marker)> GetParts(string code)
        {
            var current = string.Empty;
            var previous = string.Empty;
            var activeMarkers = MarkerType.None;
            WordTypeModel? incomplete = null;
            var endedInside = false;
            while (!string.IsNullOrEmpty(code))
            {
                var word = FindNextWord(code, incomplete, out WordTypeModel wordType);
                code = RemoveFromStart(code, word);

                if (wordType.WordType == WordType.Marker)
                {
                    var marker = _markers[word];
                    if (!string.IsNullOrEmpty(current))
                    {
                        yield return (current, CodePart.Text, activeMarkers);
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

                var isIncomplete = wordType.WordType.HasFlag(WordType.Incomplete);
                wordType.WordType &= ~WordType.Incomplete;

                incomplete = isIncomplete
                    ? wordType
                    : null;

                switch (wordType.WordType)
                {
                    case WordType.String:
                        if (!string.IsNullOrEmpty(current))
                        {
                            yield return (current, CodePart.Text, activeMarkers);
                            current = string.Empty;
                        }
                        previous = word;
                        yield return (word, CodePart.String, activeMarkers);
                        break;
                    case WordType.Interpolated:
                        if (!string.IsNullOrEmpty(current))
                        {
                            yield return (current, CodePart.Text, activeMarkers);
                            current = string.Empty;
                        }
                        foreach (var (part, type, inside) in ParseInterpolatedString(word, endedInside))
                        {
                            endedInside = inside;
                            previous = part;
                            yield return (part, type, activeMarkers);
                        }
                        break;
                    case WordType.Comment:
                        if (!string.IsNullOrEmpty(current))
                        {
                            yield return (current, CodePart.Text, activeMarkers);
                            current = string.Empty;
                        }
                        previous = word;
                        yield return (word, CodePart.Comment, activeMarkers);
                        break;
                    case WordType.Unknown:
                        var found = false;
                        foreach(var checker in _partCheckers)
                        {
                            var type = checker(code, word, previous);
                            found = type != CodePart.Text;

                            if (found)
                            {
                                if (!string.IsNullOrEmpty(current))
                                {
                                    yield return (current, CodePart.Text, activeMarkers);
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
                yield return (current, CodePart.Text, activeMarkers);
            }
        }

        /// <summary>
        /// Try to parse an interpolated string
        /// </summary>
        /// <param name="word"></param>
        /// <param name="beginInside"></param>
        /// <returns></returns>
        protected virtual IEnumerable<(string Part, CodePart Type, bool Inside)> ParseInterpolatedString(string word, bool beginInside)
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

                    foreach (var p in GetParts(insidePart))
                    {
                        yield return (p.Part, p.Type, last);
                    }
                }
                else
                {
                    yield return (part, CodePart.String, false);
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
        /// <param name="incomplete"></param>
        /// <param name="wordType"></param>
        /// <returns></returns>
        protected virtual string FindNextWord(string code, WordTypeModel? incomplete, out WordTypeModel wordType)
        {

            if (string.IsNullOrEmpty(code))
            {
                wordType = WordTypeModel.Empty;
                return string.Empty;
            }

            var marker = _markers.Keys.FirstOrDefault(m => code.StartsWith(m));
            if (!string.IsNullOrEmpty(marker))
            {
                wordType = WordTypeModel.Marker;
                return marker;
            }
                       
            if (incomplete is not null)
            {
                wordType = incomplete;
            }
            else
            {
                var starter = StringStarters.FirstOrDefault(s => code.StartsWith(s.Key));
                wordType = starter.Value ?? WordTypeModel.Empty;
            }

            var word = string.Empty;
            if (wordType.WordType != WordType.Unknown)
            {
                word = FindTillValue(code, wordType.SearchFromIndex, wordType.Ender);
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
                    if (space && c != ' ')
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
                wordType.WordType |= WordType.Incomplete;
            }

            return word
                .Split(_markers.Keys.ToArray(), StringSplitOptions.None)
                .FirstOrDefault() ?? string.Empty;
        }
    }
}
