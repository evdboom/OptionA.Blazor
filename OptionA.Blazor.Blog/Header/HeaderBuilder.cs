namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Builder for creating <see cref="HeaderContent"/>
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public class HeaderBuilder<Parent> : ContentBuilderBase<HeaderBuilder<Parent>, Parent, HeaderContent>, IParentBuilder
        where Parent : IParentBuilder
    {
        /// <summary>
        /// Sets the color to <see cref="BlogColor.Header"/>
        /// </summary>
        protected override BlogColor OwnColor => BlogColor.Header;

        /// <inheritdoc />
        public IPost? Post => _result.Post;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="parent"></param>
        public HeaderBuilder(Parent parent) : base(parent)
        {
        }

        /// <summary>
        /// Sets the text for the header
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public HeaderBuilder<Parent> WithText(string text)
        {
            _content.Text = text;
            return this;
        }

        /// <summary>
        /// Sets the size of the header
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public HeaderBuilder<Parent> OfSize(HeaderSize size)
        {
            _content.HeaderSize = size;
            return this;
        }

        /// <inheritdoc/>
        protected override HeaderBuilder<Parent> This()
        {
            return this;
        }

        /// <inheritdoc/>
        public void AddContent(IContent content)
        {
            _content.ChildContent.Add(content);
        }
    }

}
