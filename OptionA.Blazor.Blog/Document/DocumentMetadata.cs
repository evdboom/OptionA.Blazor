using System;
using System.Collections.Generic;
using System.Linq;

namespace OptionA.Blazor.Blog;

public class DocumentMetadata
{
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public DateTime? Date { get; set; }
    public List<string> Tags { get; } = new List<string>();

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
