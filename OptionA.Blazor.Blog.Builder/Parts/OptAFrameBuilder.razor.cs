using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.Parts
{
    /// <summary>
    /// Builds an external iframe
    /// </summary>
    public partial class OptAFrameBuilder
    {
        private const string SourceId = "opta-frame-source";
        private const string TitleId = "opta-frame-title";
        private const string WidthId = "opta-frame-width";
        private const string HeightId = "opta-frame-height";

        /// <summary>
        /// Index of content in post (for id uniqueness)
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
        public FrameContent? Content { get; set; }
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

        private Dictionary<string, object?> GetAttributes(string id, string placeholder)
        {
            var defaultAttributes = new Dictionary<string, object?>
            {
                ["placeholder"] = placeholder,
                ["id"] = $"{id}-{ContentIndex}"
            };

            return DataProvider.GetAttributes(BuilderType.TextInput, defaultAttributes);
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
