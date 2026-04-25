namespace OptionA.Blazor.Blog.Document.Internal;

/// <summary>
/// Parses the body of a synthetic <c>opta-playground</c> fenced block into a descriptor id
/// and an optional dictionary of parameter overrides.
/// </summary>
/// <remarks>
/// The expected format is a simple YAML-subset where the first line (injected by
/// <see cref="PlaygroundDirectivePreprocessor"/>) is <c>id: &lt;value&gt;</c>, optionally
/// followed by a <c>parameters:</c> mapping with indented key-value pairs.
/// </remarks>
internal static class PlaygroundYamlParser
{
    /// <summary>
    /// Parses <paramref name="content"/> and returns the descriptor id and parameter overrides.
    /// </summary>
    internal static (string? Id, IReadOnlyDictionary<string, string> Parameters) Parse(string content)
    {
        string? id = null;
        var parameters = new Dictionary<string, string>(StringComparer.Ordinal);
        var inParameters = false;

        foreach (var rawLine in content.Split('\n'))
        {
            var line = rawLine.TrimEnd('\r');

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (!inParameters)
            {
                if (line.StartsWith("id:", StringComparison.OrdinalIgnoreCase))
                {
                    id = line.Substring("id:".Length).Trim();
                    if (string.IsNullOrWhiteSpace(id))
                    {
                        id = null;
                    }
                }
                else if (line.TrimStart().Equals("parameters:", StringComparison.OrdinalIgnoreCase)
                         || line.TrimStart().StartsWith("parameters: ", StringComparison.OrdinalIgnoreCase))
                {
                    inParameters = true;
                }
            }
            else
            {
                // Any non-indented line ends the parameters section
                if (line.Length > 0 && line[0] != ' ' && line[0] != '\t')
                {
                    inParameters = false;
                    continue;
                }

                var kvPart = line.Trim();
                var colonIndex = kvPart.IndexOf(':', StringComparison.Ordinal);
                if (colonIndex > 0)
                {
                    var key = kvPart.Substring(0, colonIndex).Trim();
                    var value = kvPart.Substring(colonIndex + 1).Trim();
                    if (!string.IsNullOrEmpty(key))
                    {
                        parameters[key] = value;
                    }
                }
            }
        }

        return (id, parameters);
    }
}
