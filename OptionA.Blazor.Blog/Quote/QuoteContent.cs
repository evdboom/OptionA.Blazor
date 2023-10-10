namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Component for quotes
    /// </summary>
    public class QuoteContent : Content
    {
        /// <summary>
        /// Additional classes for source element
        /// </summary>
        public List<string> AdditionalSourceClasses { get; set; } = new List<string>();
        /// <summary>
        /// Removed classes for source element
        /// </summary>
        public List<string> RemovedSourceClasses { get; set; } = new List<string>();
        /// <summary>
        /// Attributes for source element
        /// </summary>
        public Dictionary<string, object?> SourceAttributes { get; set; } = new Dictionary<string, object?>();
        /// <summary>
        /// Actual quote
        /// </summary>
        public string? Quote { get; set; }
        /// <summary>
        /// Quote source
        /// </summary>
        public string? Source { get; set; }
        /// <summary>
        /// url of quote source
        /// </summary>
        public string? SourceUrl { get; set; }
        /// <inheritdoc/>
        public override ContentType Type => ContentType.Quote;
    }
}
