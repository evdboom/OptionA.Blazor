namespace OptionA.Blazor.Blog.Text.Parser
{
    /// <summary>
    /// Markdown bold part
    /// </summary>
    public class BoldMarker : AsterixMarker
    {
        /// <inheritdoc/>
        public override int Priority => 10;
        /// <inheritdoc/>
        protected override int AsterixCount => 2;
        /// <inheritdoc/>
        public override MarkerType Type => MarkerType.Bold;
        /// <inheritdoc/>
        public override IContent CreateContent(string content)
        {
            return new BoldContent { Content = content };
        }
    }
}
