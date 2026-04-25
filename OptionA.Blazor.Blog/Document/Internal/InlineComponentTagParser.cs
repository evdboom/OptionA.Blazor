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

    [GeneratedRegex(@"([\w-]+)(?:\s*=\s*(?:""([^""]*)""|'([^']*)'))?", RegexOptions.None)]
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
        // Trim everything at and after the first `/`, `>` or new-line to avoid parsing inner content.
        var closingIndex = afterName.IndexOfAny(['/', '>']);
        var attrSection = closingIndex >= 0 ? afterName[..closingIndex] : afterName;

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
            else
            {
                // Boolean shorthand — attribute present but no value
                attributes[name] = null;
            }
        }

        return (tagName, attributes);
    }
}
