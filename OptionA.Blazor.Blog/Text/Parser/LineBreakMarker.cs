using System.Diagnostics.CodeAnalysis;

namespace OptionA.Blazor.Blog.Text.Parser
{
    /// <summary>
    /// Markdown bold part
    /// </summary>
    public class LineBreakMarker : MarkerDefinition
    {
        /// <inheritdoc/>
        public override int Priority => 40;
        /// <inheritdoc/>
        public override string Starter => "\n";
        /// <inheritdoc/>
        public override string Ender => string.Empty;
        /// <inheritdoc/>
        public override MarkerType Type => MarkerType.Bold;
        /// <inheritdoc/>
        public override bool IsValidForMarker(string input, [NotNullWhen(true)] out string? content)
        {
            if (string.IsNullOrEmpty(input))
            {
                content = null;
                return false;
            }
            else if (input.StartsWith(Starter))
            {
                content = string.Empty;
                return true;
            }

            content = null;
            return false;
        }

        /// <inheritdoc/>
        public override IContent CreateContent(string content)
        {
            return new LineBreakContent();
        }
    }
}
