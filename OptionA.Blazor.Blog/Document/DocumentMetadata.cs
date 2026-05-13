using System;
using System.Collections.Generic;
using System.Linq;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Holds metadata parsed from YAML front-matter of a Markdown document.
/// </summary>
public class DocumentMetadata
{
    /// <summary>Gets or sets the document title.</summary>
    public string? Title { get; set; }
    /// <summary>Gets or sets the document subtitle.</summary>
    public string? Subtitle { get; set; }
    /// <summary>Gets or sets the publication date.</summary>
    public DateTime? Date { get; set; }
    /// <summary>Gets the list of tags associated with the document.</summary>
    public List<string> Tags { get; } = new List<string>();

    /// <summary>
    /// Parses optional YAML front-matter from <paramref name="markdown"/> and returns the
    /// extracted <see cref="DocumentMetadata"/> and the body text (without the front-matter block).
    /// Returns <c>null</c> metadata when no front-matter is present.
    /// </summary>
    public static (DocumentMetadata? metadata, string body) ParseFromMarkdown(string? markdown)
    {
        if (string.IsNullOrWhiteSpace(markdown))
            return (null, string.Empty);

        // tolerant front-matter extraction: look for top-of-file '---' block
        var s = markdown!;
        var first = s.IndexOf("---");
        if (first == -1 || s.Substring(0, first).Trim().Length != 0)
            return (null, markdown);

        var second = s.IndexOf("---", first + 3);
        if (second == -1)
            return (null, markdown);

        // extract between the two delimiters
        var fmRaw = s.Substring(first + 3, second - (first + 3));
        // body after the closing delimiter
        var bodyStart = second + 3;
        var body = s.Substring(bodyStart).TrimStart('\r','\n');

        var md = new DocumentMetadata();
        var lines = fmRaw.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var rawLine in lines)
        {
            var line = rawLine.Trim();
            var colon = line.IndexOf(':');
            if (colon <= 0) continue;
            var key = line.Substring(0, colon).Trim();
            var val = line.Substring(colon + 1).Trim().Trim('"').Trim('\'');

            if (string.Equals(key, "title", StringComparison.OrdinalIgnoreCase)) md.Title = val;
            else if (string.Equals(key, "subtitle", StringComparison.OrdinalIgnoreCase)) md.Subtitle = val;
            else if (string.Equals(key, "date", StringComparison.OrdinalIgnoreCase)) { if (DateTime.TryParse(val, out var dt)) md.Date = dt; }
            else if (string.Equals(key, "tags", StringComparison.OrdinalIgnoreCase))
            {
                if (val.StartsWith("[") && val.EndsWith("]"))
                {
                    var inner = val.Substring(1, val.Length - 2);
                    foreach (var t in inner.Split(',', StringSplitOptions.RemoveEmptyEntries)) md.Tags.Add(t.Trim().Trim('"').Trim('\''));
                }
                else
                {
                    foreach (var t in val.Split(',', StringSplitOptions.RemoveEmptyEntries)) md.Tags.Add(t.Trim().Trim('"').Trim('\''));
                }
            }
        }

        return (md, body);
    }
}
