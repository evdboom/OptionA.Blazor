namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Builder for the <see cref="ImageContent"/>
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public class ImageBuilder<Parent> : ContentBuilderBase<ImageBuilder<Parent>, Parent, ImageContent>
        where Parent : IParentBuilder
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="parent"></param>
        public ImageBuilder(Parent parent) : base(parent)
        {
            _content.Post = _result.Post;
            _blockAlignment = PositionType.Center;
        }

        /// <summary>
        /// Sets the size of the image
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public ImageBuilder<Parent> WithSize(object? width, object? height)
        {
            _content.Width = $"{width}";
            _content.Height = $"{height}";
            return this;
        }

        /// <summary>
        /// Sets the width of the image
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public ImageBuilder<Parent> WithWidth(object? width)
        {
            _content.Width = $"{width}";

            return this;
        }

        /// <summary>
        /// Sets the width of the image
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public ImageBuilder<Parent> WithHeight(object? height)
        {
            _content.Height = $"{height}";

            return this;
        }

        /// <summary>
        /// Sets the image source, sets mode to post
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public ImageBuilder<Parent> WithSource(string source)
        {
            return WithSource(source, ImageMode.LocalPost);
        }        

        /// <summary>
        /// Sets the image source, and mode
        /// </summary>
        /// <param name="source"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public ImageBuilder<Parent> WithSource(string source, ImageMode mode)
        {
            _content.Source = source;
            _content.Mode = mode;
            return this;
        }

        /// <inheritdoc/>
        protected override ImageBuilder<Parent> This()
        {
            return this;
        }
    }
}
