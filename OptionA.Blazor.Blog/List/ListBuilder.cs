namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Builder for the <see cref="ListContent"/>
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public class ListBuilder<Parent> : ContentBuilderBase<ListBuilder<Parent>, Parent, ListContent>, IContentParentBuilder
        where Parent : IParentBuilder
    {
        /// <inheritdoc/>
        public IPost? Post => _result.Post;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="parent"></param>
        public ListBuilder(Parent parent) : base(parent)
        {
        }

        /// <summary>
        /// will set the ordered property of the content to the given value, resulting in either an &lt;ul&gt; or &lt;ol&gt; tag
        /// </summary>
        /// <param name="ordered"></param>
        /// <returns></returns>
        public ListBuilder<Parent> IsOrdered(bool ordered)
        {
            _content.Ordered = ordered;
            return this;
        }

        /// <summary>
        /// Sets the start for the ordered lists
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public ListBuilder<Parent> WithStart(int start)
        {
            _content.Start = start;
            return this;
        }

        /// <summary>
        /// Sets the list style for the list (bullet style)
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public ListBuilder<Parent> WithListStyle(ListStyle style)
        {
            _content.ListStyle = style;
            return this;
        }

        /// <inheritdoc/>
        public void AddContent(IContent content)
        {
            if (content is not ListItemContent)
            {
                throw new InvalidOperationException($"Can only add {nameof(ListItemContent)} to a list");
            }

            _content.ChildContent.Add(content);            
        }

        /// <summary>
        /// Creates a new List item builder for this list, to create a row
        /// </summary>
        /// <returns></returns>
        public ListItemBuilder<Parent> CreateRow()
        {
            return new ListItemBuilder<Parent>(this);
        }

        /// <inheritdoc/>
        protected override ListBuilder<Parent> This()
        {
            return this;
        }
    }
}
