using System.Diagnostics.CodeAnalysis;

namespace OptionA.Blazor.Blog.Text.Parser
{
    /// <summary>
    /// Markdown italic part
    /// </summary>
    public class ItalicMarker : MarkerDefinition
    {
        /// <inheritdoc/>
        public override int Priority => 20;
        /// <inheritdoc/>
        public override string Starter => "*";
        /// <inheritdoc/>
        public override string Ender => "*";
        /// <inheritdoc/>
        public override MarkerType Type => MarkerType.Italic;
        /// <inheritdoc/>
        public override bool IsValidForMarker(string input, [NotNullWhen(true)] out string? content)
        {
            if (string.IsNullOrEmpty(input) || input.Length < 3 || input[0] == '\\')
            {
                content = null;
                return false;
            }
            else if (input.StartsWith(Starter) && input[2] != ' ')
            {
                var withAsterix = input[Starter.Length..].IndexOf($"*{Ender}");
                var end = input[Starter.Length..].IndexOf(Ender);

                if (end > 0)
                {
                    if (withAsterix == end)
                    {
                        var newEnd = input[(Starter.Length + end)..].IndexOf(Ender);
                    }

                    var endIndex = end + Starter.Length;
                    if (withAsterix == end)
                    {
                        endIndex++;
                    }

                    if (input[endIndex - 1] != ' ' && input[endIndex - 1] != '\\')
                    {
                        content = input[Starter.Length..endIndex];
                        return true;
                    }
                }
            }

            content = null;
            return false;
        }

        /// <inheritdoc/>
        public override IContent CreateContent(string content)
        {
            return new ItalicContent { Content = content };
        }
    }
}
