using System.Diagnostics.CodeAnalysis;

namespace OptionA.Blazor.Blog.Text.Parser
{
    /// <summary>
    /// Markdown italic part
    /// </summary>
    public class ItalicMarker : AsterixMarker
    {
        /// <inheritdoc/>
        public override int Priority => 20;
        /// <inheritdoc/>
        protected override int AsterixCount => 1;
        /// <inheritdoc/>
        public override MarkerType Type => MarkerType.Italic;        
        /// <inheritdoc/>
        public override IContent CreateContent(string content)
        {
            return new ItalicContent { Content = content };
        }
    }
}
