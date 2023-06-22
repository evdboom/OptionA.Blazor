namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Base classes for posts, can be inherited to construct posts.
    /// </summary>
    public abstract class Post : IPost
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

        /// <summary>
        /// Method where Post content can be set for actual post classes
        /// </summary>
        /// <param name="builder"></param>
        public abstract void OnBuildPost(PostBuilder builder);

        /// <inheritdoc/>
        public IEnumerable<(string Value, string Id, int Size)> GetHeaders()
        {
            foreach(var content in Content)
            {
                foreach(var header in GetHeaders(content))
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

            foreach(var child in content.ChildContent)
            {
                foreach(var childHeader in GetHeaders(child))
                {
                    yield return childHeader;
                }
            }
        }
    }
}
