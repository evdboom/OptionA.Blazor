using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.Parts
{
    /// <summary>
    /// Builder for paragraphs
    /// </summary>
    public partial class OptaParagraphBuilder
    {
        private const string ParagraphId = "opta-paragraph";

        /// <summary>
        /// Index of the current content in the collection
        /// </summary>
        [Parameter]
        public int ContentIndex { get; set; }
        /// <summary>
        /// Total number of content (for disabling move up, move down)
        /// </summary>
        [Parameter]
        public int TotalContentCount { get; set; }
        /// <summary>
        /// Content to create
        /// </summary>
        [Parameter]
        public ParagraphContent? Content { get; set; }
        /// <summary>
        /// Called when something changes
        /// </summary>
        [Parameter]
        public EventCallback ContentChanged { get; set; }
        /// <summary>
        /// Called when content should be removed
        /// </summary>
        [Parameter]
        public EventCallback ContentRemoved { get; set; }
        /// <summary>
        /// Occurs when move up is clicked
        /// </summary>
        [Parameter]
        public EventCallback MovedUp { get; set; }
        /// <summary>
        /// Occurs when move down is clicked
        /// </summary>
        [Parameter]
        public EventCallback MovedDown { get; set; }
        [Inject]
        private IBlogBuilderDataProvider DataProvider { get; set; } = null!;

        private Dictionary<string, object?> GetAttributes()
        {
            var defaultAttributes = new Dictionary<string, object?>
            {
                ["placeholder"] = "Text...",
                ["id"] = $"{ParagraphId}-{ContentIndex}"
            };

            return DataProvider.GetAttributes(BuilderType.TextAreaInput, defaultAttributes);
        }

        private Dictionary<string, object?> GetLabelAttributes(string id)
        {
            var defaultAttributes = new Dictionary<string, object?>
            {
                ["for"] = $"{id}-{ContentIndex}"
            };

            return DataProvider.GetAttributes(BuilderType.Label, defaultAttributes);
        }
    }
}
