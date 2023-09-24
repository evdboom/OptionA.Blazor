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

        private HeaderContent? _title;
        private IEnumerable<IContent>? _tags;
        private BlockContent? _date;
        private BlockContent? _subtitle;

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            if (Post == null)
            {
                _title = null;
                _tags = null;
                _date = null;
                _subtitle = null;
                return;
            }
            _title = new HeaderContent
            {
                Content = Post.Title,
                Size = DataProvider.PostHeaderSize
            };
            if (!string.IsNullOrEmpty(DataProvider.PostTitleClass))
            {
                _title.AdditionalClasses.Add(DataProvider.PostTitleClass);
            }
            _tags = Post.Tags
                .Select(GetTagContent);
            _date = new BlockContent
            {
                Content = DataProvider.PostDateDisplay.ToDateFormat(Post.Date)
            };
            if (!string.IsNullOrEmpty(DataProvider.PostDateClass))
            {
                _date.AdditionalClasses.Add(DataProvider.PostDateClass);
            }
            if (!string.IsNullOrEmpty(Post.Subtitle))
            {
                _subtitle = new BlockContent
                {
                    Content = Post.Subtitle
                };
                if (!string.IsNullOrEmpty(DataProvider.PostSubtitleClass))
                {
                    _date.AdditionalClasses.Add(DataProvider.PostSubtitleClass);
                }
            }
        }

        private IContent GetTagContent(string tag)
        {
            IContent result = !string.IsNullOrEmpty(DataProvider.TagOverviewHref)
                ? new LinkContent
                {
                    Content = tag,
                    Href = $"{DataProvider.TagOverviewHref}/{tag}".ToLowerInvariant(),
                    Target = "_self"
                }
                : new BlockContent
                {
                    Content = tag
                };
            if (!string.IsNullOrEmpty(DataProvider.TagClass))
            {
                result.AdditionalClasses.Add(DataProvider.TagClass);
            }

            return result;
        }
    }
}
