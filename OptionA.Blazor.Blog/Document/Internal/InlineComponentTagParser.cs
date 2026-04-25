using System.Text.RegularExpressions;

namespace OptionA.Blazor.Blog.Document.Internal;

/// <summary>
/// Parses a raw HTML block string to extract an OptA component tag name and its attributes.
/// Supports double-quoted, single-quoted, and boolean-shorthand attribute forms.
/// </summary>
internal static partial class InlineComponentTagParser
{
    [GeneratedRegex(@"^<(OptA\w+)", RegexOptions.IgnoreCase)]
    private static partial Regex TagNameRegex();

    [GeneratedRegex(@"([\w-]+)(?:\s*=\s*(?:""((?:[^""\\]|\\.)*)""|'((?:[^'\\]|\\.)*)'|([^\s""'>/]+)))?", RegexOptions.None)]
    private static partial Regex AttributeRegex();

    /// <summary>
    /// Attempts to parse the leading tag name and attributes from <paramref name="raw"/>.
    /// </summary>
    /// <param name="raw">Raw content of the Markdig <c>HtmlBlock</c>.</param>
    /// <returns>
    /// The tag name (e.g. <c>OptAButton</c>) and a dictionary of attribute names to their raw string
    /// values, or <see langword="null"/> tag name when the block is not an OptA tag.
    /// A <see langword="null"/> dictionary value means the attribute is a boolean shorthand.
    /// </returns>
    internal static (string? TagName, IReadOnlyDictionary<string, string?> Attributes) Parse(string raw)
    {
        var trimmed = raw.Trim();

        var nameMatch = TagNameRegex().Match(trimmed);
        if (!nameMatch.Success)
        {
            return (null, new Dictionary<string, string?>());
        }

        var tagName = nameMatch.Groups[1].Value;

        // The attributes section is everything between the tag name and the end of the opening tag.
        var afterName = trimmed[nameMatch.Length..];
        // Scan until a '>' or '/' that's not inside quotes to avoid breaking on characters inside attribute values.
        char quote = '\0';
        int idx = 0;
        for (; idx < afterName.Length; idx++)
        {
            var c = afterName[idx];
            if (c == '"' || c == '\'')
            {
                if (quote == '\0')
                {
                    quote = c;
                }
                else if (quote == c)
                {
                    // If previous char is backslash, this quote is escaped; ignore.
                    var prev = idx > 0 ? afterName[idx - 1] : '\0';
                    if (prev != '\\') quote = '\0';
                }
            }
            else if ((c == '>' || c == '/') && quote == '\0')
            {
                break;
            }
        }
        var attrSection = idx > 0 ? afterName[..idx] : string.Empty;

        var attributes = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

        foreach (Match m in AttributeRegex().Matches(attrSection))
        {
            var name = m.Groups[1].Value;

            // Double-quoted value
            if (m.Groups[2].Success)
            {
                attributes[name] = m.Groups[2].Value;
            }
            // Single-quoted value
            else if (m.Groups[3].Success)
            {
                attributes[name] = m.Groups[3].Value;
            }
            // Unquoted value
            else if (m.Groups.Count >= 5 && m.Groups[4].Success)
            {
                attributes[name] = m.Groups[4].Value;
            }
            else
            {
                // Boolean shorthand — attribute present but no value
                attributes[name] = null;
            }
        }

        return (tagName, attributes);
    }
}
