using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Post component
    /// </summary>
    public partial class OptaPost
    {
        /// <summary>
        /// Post to display
        /// </summary>
        [Parameter]
        public Post? Post { get; set; }
        [Inject]
        private IBlogDataProvider DataProvider { get; set; } = null!;

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            if (Post == null)
            {
                _content = null;
                return;
            }
            var title = new HeaderContent
            {
                Content = Post.Title,
                Size = DataProvider.PostHeaderSize
            }
            _content = new List<IContent>
            {

            }
                .Concat(Post.Content);

        }

    }
}
