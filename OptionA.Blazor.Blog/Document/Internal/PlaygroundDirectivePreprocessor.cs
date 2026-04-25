namespace OptionA.Blazor.Blog.Document.Internal;

/// <summary>
/// Pre-processes a Markdown source string to replace <c>::: playground ... :::</c>
/// directives with synthetic fenced code blocks that Markdig can parse as leaf blocks,
/// making the raw body text accessible via <see cref="Markdig.Syntax.LeafBlock.Lines"/>.
/// </summary>
/// <remarks>
/// The synthetic block uses the info string <c>opta-playground</c> so that
/// <see cref="BlockConverter"/> can identify it after parsing.
/// The playground id (from the opening line) is prepended to the body as
/// <c>id: &lt;value&gt;</c> so all directive data travels together.
/// </remarks>
internal static class PlaygroundDirectivePreprocessor
{
    internal const string SyntheticFenceInfo = "opta-playground";
    private const string FenceMarker = ":::";
    private const string PlaygroundKeyword = "playground";

    /// <summary>
    /// Scans <paramref name="markdown"/> for playground directives and replaces each with a
    /// synthetic fenced code block understood by <see cref="BlockConverter"/>.
    /// Returns the original string unchanged when no directives are present.
    /// </summary>
    internal static string Process(string markdown)
    {
        if (!markdown.Contains(FenceMarker, StringComparison.Ordinal))
        {
            return markdown;
        }

        var lines = markdown.Split('\n');
        var result = new System.Text.StringBuilder(markdown.Length);
        var i = 0;

        while (i < lines.Length)
        {
            var line = lines[i].TrimEnd('\r');

            if (TryParseDirectiveOpen(line, out var id))
            {
                var bodyLines = new List<string>();
                i++;

                while (i < lines.Length)
                {
                    var bodyLine = lines[i].TrimEnd('\r');
                    if (bodyLine.Trim() == FenceMarker)
                    {
                        break;
                    }
                    bodyLines.Add(bodyLine);
                    i++;
                }

                // Emit as synthetic fenced code block
                result.AppendLine($"```{SyntheticFenceInfo}");
                result.AppendLine($"id: {id}");
                foreach (var bodyLine in bodyLines)
                {
                    result.AppendLine(bodyLine);
                }
                result.AppendLine("```");
            }
            else
            {
                result.AppendLine(line);
            }

            i++;
        }

        return result.ToString();
    }

    private static bool TryParseDirectiveOpen(string line, out string id)
    {
        id = string.Empty;
        var trimmed = line.TrimStart();

        var prefix = $"{FenceMarker} {PlaygroundKeyword}";
        if (!trimmed.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        var rest = trimmed.Substring(prefix.Length).Trim();

        // Try id="..."
        var match = System.Text.RegularExpressions.Regex.Match(rest, @"id=""([^""]+)""");
        if (match.Success)
        {
            id = match.Groups[1].Value;
            return true;
        }

        // Try id=value (unquoted)
        match = System.Text.RegularExpressions.Regex.Match(rest, @"id=(\S+)");
        if (match.Success)
        {
            id = match.Groups[1].Value;
            return true;
        }

        // No id attribute — still a playground directive, but id will be empty
        id = string.Empty;
        return true;
    }
}
