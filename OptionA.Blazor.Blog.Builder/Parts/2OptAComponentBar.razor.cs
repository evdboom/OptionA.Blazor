using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.Parts
{
    /// <summary>
    /// Bar for adding content to the post
    /// </summary>
    public partial class OptAComponentBar
    {
        private readonly Dictionary<ContentType, (string Content, string Title)> _contentTypes = new()
        {
            [ContentType.Header] = ("Add header", "Add a header to the post"),
            [ContentType.Paragraph] = ("Add paragraph", "Add a paragraph to the post"),
            [ContentType.Image] = ("Add image", "Add an Image to the post"),
            [ContentType.Code] = ("Add Code", "Add a block of code to the post"),
            [ContentType.Quote] = ("Add quote", "Add a quote to the post"),
            [ContentType.Frame] = ("Add frame", "Add a (external) frame to the post")
        };

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

        private Dictionary<string, object?> GetAttributes()
        {
            var defaultAttributes = new Dictionary<string, object?>
            {
                ["opta-component-bar"] = true,
            };

            return DataProvider.GetAttributes(BuilderType.ComponentBar, defaultAttributes);
        }

        private Dictionary<string, object?> GetButtonAttributes(string title)
        {
            var defaultAttributes = new Dictionary<string, object?>
            {
                ["type"] = "button",
                ["title"] = title
            };

            return DataProvider.GetAttributes(BuilderType.AddContentButton, defaultAttributes);
        }
    }
}
