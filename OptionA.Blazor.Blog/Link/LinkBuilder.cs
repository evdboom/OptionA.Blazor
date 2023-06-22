namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Builder for the <see cref="LinkContent"/>
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public class LinkBuilder<Parent> : ContentBuilderBase<LinkBuilder<Parent>, Parent, LinkContent>, IParentBuilder
        where Parent : IParentBuilder
    {
        /// <inheritdoc/>
        public IPost? Post => _result.Post;
        /// <summary>
        /// Sets the color to <see cref="BlogColor.Link"/>
        /// </summary>
        protected override BlogColor OwnColor => BlogColor.Link;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="parent"></param>
        public LinkBuilder(Parent parent) : base(parent)
        {
        }

        /// <summary>
        /// Sets the href
        /// </summary>
        /// <param name="href"></param>
        /// <returns></returns>
        public LinkBuilder<Parent> WithHref(string href)
        {
            _content.Href = href;
            return this;
        }

        /// <summary>
        /// Sets the href and mode
        /// </summary>
        /// <param name="href"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public LinkBuilder<Parent> WithHref(string href, LinkMode mode)
        {
            WithHref(href);
            WithMode(mode);

            return this;
        }

        /// <summary>
        /// Sets the text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public LinkBuilder<Parent> WithText(string text)
        {
            _content.Text = text;
            return this;
        }

        /// <summary>
        /// Sets if the link opens internally (default), over self or in a new tab
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public LinkBuilder<Parent> WithMode(LinkMode mode)
        {
            _content.Mode = mode;
            return this;
        }

        /// <inheritdoc/>
        public void AddContent(IContent content)
        {
            _content.ChildContent.Add(content);
        }

        /// <summary>
        /// Add multiple contents to this builder, properties are not copied to these children
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public LinkBuilder<Parent> AddContents(IEnumerable<IContent> contents)
        {
            foreach (var content in contents)
            {
                _content.ChildContent.Add(content);
            }
            return this;
        }

        /// <inheritdoc/>
        protected override LinkBuilder<Parent> This()
        {
            return this;
        }
    }
}
