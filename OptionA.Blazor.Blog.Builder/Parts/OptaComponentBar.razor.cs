using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.Parts
{
    /// <summary>
    /// Bar for adding content to the post
    /// </summary>
    public partial class OptaComponentBar
    {
        /// <summary>
        /// Called whenevet content is added
        /// </summary>
        [Parameter]
        public EventCallback<IContent> ContentAdded { get; set; }
        [Inject]
        private IBlogBuilderDataProvider DataProvider { get; set; } = null!;

        private async Task AddContent(ContentType type)
        {
            await ContentAdded.InvokeAsync(DataProvider.CreateContentForType(type));
        }
    }
}
