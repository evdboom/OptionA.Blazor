using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.Parts
{
    /// <summary>
    /// Builder for paragraphs
    /// </summary>
    public partial class OptaParagraphBuilder
    {
        /// <summary>
        /// Content to create
        /// </summary>
        [Parameter]
        public ParagraphContent? Content { get; set; }
        /// <summary>
        /// Called when something changes
        /// </summary>
        [Parameter]
        public EventCallback OnChange { get; set; }
        /// <summary>
        /// Called when content should be removed
        /// </summary>
        [Parameter]
        public EventCallback<IContent> OnRemove { get; set; }
        [Inject]
        private IBlogBuilderDataProvider DataProvider { get; set; } = null!;
    }
}
