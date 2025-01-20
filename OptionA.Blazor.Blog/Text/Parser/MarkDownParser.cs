namespace OptionA.Blazor.Blog.Text.Parser;

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
            var part = FindNextPart(content, out var definition, out var originalLength);
            if (definition == null)
            {
                currentPart += part;
                content = RemoveFromStart(content, originalLength);
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

                content = RemoveFromStart(content, originalLength);
                yield return definition.CreateContent(part);
            }
        }

        if (!string.IsNullOrEmpty(currentPart))
        {
            yield return new TextContent
            {
                Content = currentPart
            };
        }
    }

    private string RemoveFromStart(string text, int lengthToRemove)
    {
        if (lengthToRemove > text.Length)
        {
            throw new ArgumentException($"{lengthToRemove} is higher the length of text");
        }

        return text[lengthToRemove..];
    }

    private string FindNextPart(string text, out IMarkerDefinition? definition, out int originalLength)
    {
        if (string.IsNullOrEmpty(text))
        {
            definition = null;
            originalLength = 0;
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
                    originalLength = $"{definition.Starter}{content}{definition.Ender}".Length;
                    return content;
                }
            }
        }
        
        var maybeEscape = firstChar == Escape;
        part += firstChar;        

        var found = false;
        var counter = 1;
       
        while (!found && counter < text.Length)
        {
            var c = text[counter];
            var mayBeSpecial = _specials.Contains(c);
            var isEscape = false;
            if (maybeEscape && mayBeSpecial)
            {
                // is an escaped special, remove the \
                part = part[..^1];
                isEscape = true;
            }

            var isSpecial = !isEscape && mayBeSpecial;
            maybeEscape = c == Escape;

            if (isSpecial)
            {
                found = true;
            }
            else 
            {
                part += c;
                counter++;
            }                                
        }

        originalLength = counter;
        definition = null;
        return part;
    }
}
