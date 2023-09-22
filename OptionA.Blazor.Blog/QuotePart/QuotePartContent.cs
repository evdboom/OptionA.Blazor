using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Component for quotes
    /// </summary>
    public class QuotePartContent : Content
    {
        /// <inheritdoc/>
        public List<string> AdditionalSourceClasses { get; } = new List<string>();
        /// <inheritdoc/>
        public List<string> RemovedSourceClasses { get; } = new List<string>();
        /// <inheritdoc/>
        public Dictionary<string, object?> SourceAttributes { get; } = new Dictionary<string, object?>();
        /// <summary>
        /// Actual quote
        /// </summary>
        public string? Quote { get; set; }
        /// <summary>
        /// Quote source
        /// </summary>
        public string? Source { get; set; }
        /// <inheritdoc/>
        public override ContentType Type => ContentType.QuotePart;
    }
}
