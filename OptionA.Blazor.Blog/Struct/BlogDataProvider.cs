namespace OptionA.Blazor.Blog.Struct
{
    /// <summary>
    /// Implementation of <see cref="IBlogDataProvider"/>
    /// </summary>
    public class BlogDataProvider : IBlogDataProvider
    {
        private readonly BlogOptions _options;

        /// <summary>
        /// Configured options for this provider
        /// </summary>
        public BlogOptions Options => _options;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public BlogDataProvider(Action<BlogOptions>? configuration = null)
        {
            _options = new();
            configuration?.Invoke(_options);
        }

        /// <inheritdoc/>
        public List<string> DefaultClassesForType(ContentType type)
        {
            if (_options.DefaultClassesPerType != null && _options.DefaultClassesPerType.TryGetValue(type, out var classes))
            {
                return classes;
            }

            return new();
        }
    }
}
