using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OptionA.Blazor.Blog.Builder.HelperComponents
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
        public EventCallback<IContent> OnAddContent { get; set; }
        [Inject]
        private IBlogBuilderDataProvider DataProvider { get; set; } = null!;

        private async Task AddContent(ContentType type)
        {
            switch (type)
            {
                case ContentType.Paragraph:
                    await OnAddContent.InvokeAsync(new ParagraphContent());
                    break;
                case ContentType.Header:
                    await OnAddContent.InvokeAsync(new HeaderContent());
                    break;
            }
        }
    }
}
