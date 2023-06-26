using System.Text.Json;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Base classes for posts, can be inherited to construct posts.
    /// </summary>
    public class Post : IPost
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        protected Post()
        {
            var builder = PostBuilder.CreatePost(this);
            OnBuildPost(builder);
            builder.Build();
        }

        private Post(bool empty)
        {

        }

        /// <summary>
        /// Creates a builder with an empty post
        /// </summary>
        /// <returns></returns>
        public static PostBuilder CreateEmptyBuilder()
        {
            var post = new Post(true);
            var builder = PostBuilder.CreatePost(post);
            return builder;
        }

        /// <summary>
        /// Tries to create a post form the given json string
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        public static IPost Deserialize(string json)
        {
            var items = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
            if (items == null)
            {
                throw new InvalidCastException("No valid json for post");
            }

            var post = new Post(true);
            if (items.TryGetValue(nameof(Tags), out var tags))
            {
                post.Tags.AddRange(JsonSerializer.Deserialize<string[]>(tags)!);
            }
            if (items.TryGetValue(nameof(PostDate), out var postDate))
            {
                post.PostDate = JsonSerializer.Deserialize<DateTime>(postDate);
            }
            if (items.TryGetValue(nameof(Title), out var title))
            {
                post.Title = JsonSerializer.Deserialize<string>(title) ?? string.Empty;
            }
            if (items.TryGetValue(nameof(Subtitle), out var subtitle))
            {
                post.Subtitle = JsonSerializer.Deserialize<string>(subtitle);
            }
            if (items.TryGetValue(nameof(Content), out var content))
            {
                var contentList = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(content);
                foreach (var contentItem in contentList!)
                {
                    post.Content.Add(Blog.Content.Deserialize(contentItem));
                }
            }


            return post;
        }

        /// <inheritdoc/>
        public IList<string> Tags { get; } = new List<string>();

        /// <inheritdoc/>
        public IList<IContent> Content { get; } = new List<IContent>();

        private DateTime _postDate;
        /// <inheritdoc/>
        public DateTime PostDate
        {
            get => _postDate;
            set
            {
                _postDate = value;
                _dateId = $"{_postDate:yyyyMMddHH}";
            }
        }

        private string? _title;
        /// <inheritdoc/>
        public string Title
        {
            get => _title ?? string.Empty;
            set
            {
                _title = value;
                _titleId = value
                    .Replace(" ", "-")
                    .ToLowerInvariant();
            }
        }

        /// <inheritdoc/>
        public string? Subtitle { get; set; }

        private string? _dateId;
        /// <inheritdoc/>
        public string DateId => _dateId ?? string.Empty;

        private string? _titleId;
        /// <inheritdoc/>
        public string TitleId => _titleId ?? string.Empty;

        /// <inheritdoc/>
        public string SearchString => $"{_title} {Subtitle} {string.Join(' ', Tags)}".ToLowerInvariant();

        /// <inheritdoc/>
        public string Serialize(JsonSerializerOptions? options = null)
        {
            var items = new Dictionary<string, object>();
            if (Tags.Any())
            {
                items[nameof(Tags)] = Tags;
            }
            items[nameof(PostDate)] = PostDate;
            items[nameof(Title)] = Title;
            if (!string.IsNullOrEmpty(Subtitle))
            {
                items[nameof(Subtitle)] = Subtitle;
            }
            items[nameof(Content)] = Content.Select(content => content.GetSerializationData());

            return JsonSerializer.Serialize(items, options);
        }

        /// <summary>
        /// Method where Post content can be set for actual post classes
        /// </summary>
        /// <param name="builder"></param>
        public virtual void OnBuildPost(PostBuilder builder)
        {

        }

        /// <inheritdoc/>
        public IEnumerable<(string Value, string Id, int Size)> GetHeaders()
        {
            foreach (var content in Content)
            {
                foreach (var header in GetHeaders(content))
                {
                    yield return header;
                }
            }
        }

        private IEnumerable<(string Value, string Id, int Size)> GetHeaders(IContent content)
        {
            if (content is HeaderContent header && header.Attributes.TryGetValue("id", out var id))
            {
                var value = string.IsNullOrEmpty(header.Text)
                    ? $"{id}"
                    : header.Text;
                yield return (value, $"{id}", (int)header.HeaderSize);
            }

            foreach (var child in content.ChildContent)
            {
                foreach (var childHeader in GetHeaders(child))
                {
                    yield return childHeader;
                }
            }
        }
    }
}
