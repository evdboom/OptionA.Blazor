using System.Diagnostics.CodeAnalysis;

namespace OptionA.Blazor.Blog.Text.Parser
{
    /// <summary>
    /// Markdown icon (html) part
    /// </summary>
    public class IconMarker : MarkerDefinition
    {
        /// <inheritdoc/>
        public override int Priority => 10;
        /// <inheritdoc/>
        public override string Starter => "<i>";
        /// <inheritdoc/>
        public override string Ender => "</i>";
        /// <inheritdoc/>
        public override MarkerType Type => MarkerType.Icon;
        /// <inheritdoc/>
        public override bool IsValidForMarker(string input, [NotNullWhen(true)] out string? content)
        {
            if (string.IsNullOrEmpty(input) || input.Length < 7 || input[0] == '\\')
            {
                content = null;
                return false;
            }
            else if (input.StartsWith(Starter))
            {
                var end = input[Starter.Length..].IndexOf(Ender);
                if (end > 0)
                {
                    var endIndex = end + Starter.Length;
                    if (input[endIndex - 1] != '\\')
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
            var result = new IconContent();
            result.AdditionalClasses.AddRange(content.Split(" "));

            return result;
        }
    }
}
