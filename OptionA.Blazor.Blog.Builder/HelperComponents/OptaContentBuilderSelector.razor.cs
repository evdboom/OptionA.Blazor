using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.HelperComponents
{
    /// <summary>
    /// Component for selecting correct builder
    /// </summary>
    public partial class OptaContentBuilderSelector
    {
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
        /// Content to select
        /// </summary>
        [Parameter]
        public IContent? Content { get; set; }
        /// <summary>
        /// Called whenever content changes
        /// </summary>
        [Parameter]
        public EventCallback ContentChanged { get; set; }
        /// <summary>
        /// Called whenever content should be removed
        /// </summary>
        [Parameter]
        public EventCallback<IContent> ContentRemoved { get; set; }
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
    }
}
