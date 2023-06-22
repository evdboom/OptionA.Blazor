namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Builder for an Icon component
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public class IconBuilder<Parent> : ContentBuilderBase<IconBuilder<Parent>, Parent, IconContent>
        where Parent : IParentBuilder
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="parent"></param>
        public IconBuilder(Parent parent) : base(parent)
        {
        }

        /// <summary>
        /// Sets the mode
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public IconBuilder<Parent> WithMode(IconMode mode)
        {
            _content.Mode = mode;
            return This();
        }

        /// <summary>
        /// Adds a path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IconBuilder<Parent> AddPath(string path)
        {
            _content.Paths.Add(path);
            return This();
        }

        /// <summary>
        /// Sets the size
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public IconBuilder<Parent> WithSize(object? width, object? height)
        {
            _content.Width = $"{width}";
            _content.Height = $"{height}";
            return This();
        }

        /// <summary>
        /// Sets the viewbox size
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <param name="three"></param>
        /// <param name="four"></param>
        /// <returns></returns>
        public IconBuilder<Parent> WithViewBox(int one, int two, int three, int four)
        {
            _content.ViewBoxValues[0] = one;
            _content.ViewBoxValues[1] = two;
            _content.ViewBoxValues[2] = three;
            _content.ViewBoxValues[3] = four;
            return This();
        }

        /// <inheritdoc/>
        protected override IconBuilder<Parent> This()
        {
            return this;
        }
    }
}
