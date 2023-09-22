namespace OptionA.Blazor.Blog.Text.Parser
{
    /// <summary>
    /// Implementation of the <see cref="IMarkDownParser"/>
    /// </summary>
    public class MarkDownParser : IMarkDownParser
    {
        private const char Escape = '\\';
        private readonly List<IMarkerDefinition> _markers;
        private readonly List<char> _specials;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="markers"></param>
        public MarkDownParser(IEnumerable<IMarkerDefinition> markers)
        {
            _markers = markers
                .OrderBy(marker => marker.Priority)
                .ToList();
            _specials = markers
                .Select(marker => marker.FirstChar)
                .Distinct()
                .ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<IContent> Parse(string? content)
        {
            string currentPart = string.Empty;

            while (!string.IsNullOrEmpty(content))
            {
                var part = FindNextPart(content, out var definition);
                if (definition == null)
                {
                    currentPart += part;
                    content = RemoveFromStart(content, part);
                }
                else
                {
                    if (!string.IsNullOrEmpty(currentPart))
                    {
                        yield return new TextContent
                        {
                            Content = currentPart
                        };
                        currentPart = string.Empty;
                    }

                    content = RemoveFromStart(content, $"{definition.Starter}{part}{definition.Ender}");
                    yield return definition.CreateContent(part);
                }
            }

            if (!string.IsNullOrEmpty(currentPart))
            {
                yield return new TextContent
                {
                    Content = currentPart
                };
                currentPart = string.Empty;
            }
        }

        private string RemoveFromStart(string text, string toRemove)
        {
            if (!text.StartsWith(toRemove))
            {
                throw new ArgumentException($"{toRemove} is not the start of {text})");
            }

            return text[toRemove.Length..];
        }

        private string FindNextPart(string text, out IMarkerDefinition? definition)
        {
            if (string.IsNullOrEmpty(text))
            {
                definition = null;
                return string.Empty;
            }

            var part = string.Empty;
            var firstChar = text[0];

            if (_specials.Contains(firstChar))
            {
                foreach (var marker in _markers)
                {
                    if (marker.IsValidForMarker(text, out var content))
                    {
                        definition = marker;
                        return content;
                    }
                }
            }

            part += firstChar;
            var isEscape = firstChar == Escape;
            var found = false;
            var counter = 1;
            while (!found && counter < text.Length)
            {
                var c = text[counter];
                var isSpecial = !isEscape && _specials.Contains(c);
                isEscape = c == Escape;

                if (isSpecial)
                {
                    found = true;
                }
                else
                {
                    part += c;
                }
                counter++;
            }

            definition = null;
            return part;
        }
    }
}
