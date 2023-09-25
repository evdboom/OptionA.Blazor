using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.HelperComponents
{
    /// <summary>
    /// Component for selecting correct builder
    /// </summary>
    public partial class OptaContentBuilderSelector
    {
        /// <summary>
        /// Content to select
        /// </summary>
        [Parameter]
        public IContent? Content { get; set; }
        /// <summary>
        /// Called whenever content changes
        /// </summary>
        [Parameter]
        public EventCallback OnChange { get; set; }
        /// <summary>
        /// Called whenever content should be removed
        /// </summary>
        [Parameter]
        public EventCallback<IContent> OnRemove { get; set; }
    }
}
