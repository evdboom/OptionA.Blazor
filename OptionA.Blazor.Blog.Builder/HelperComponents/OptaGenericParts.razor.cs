using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.HelperComponents
{
    /// <summary>
    /// Generic parts for blog components
    /// </summary>
    public partial class OptaGenericParts
    {
        /// <summary>
        /// Content
        /// </summary>
        [Parameter]
        public IContent? Content { get; set; }
        /// <summary>
        /// type of builder
        /// </summary>
        [Parameter]
        public BuilderType BuilderType { get; set; }
        /// <summary>
        /// Called when the component gets updated
        /// </summary>
        [Parameter]
        public EventCallback OnChange { get; set; }
        /// <summary>
        /// Called when the component should be removed
        /// </summary>
        [Parameter]
        public EventCallback<IContent> OnRemove { get; set; }
        /// <summary>
        /// Specific content for builder
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        /// <summary>
        /// Name of the component
        /// </summary>
        [Parameter]
        public string? Label { get; set; }
        [Inject]
        private IBlogBuilderDataProvider DataProvider { get; set; } = null!;
    }
}
