namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Builder for a list item <see cref="ListItemContent"/>
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public class ListItemBuilder<Parent> : ContentBuilderBase<ListItemBuilder<Parent>, ListBuilder<Parent>, ListItemContent>, IParentBuilder
        where Parent : IParentBuilder
    {
        /// <inheritdoc/>
        public IPost? Post => _result.Post;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="parent"></param>
        public ListItemBuilder(ListBuilder<Parent> parent) : base(parent)
        {
        }      
        
        /// <summary>
        /// Sets the text for the content
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public ListItemBuilder<Parent> WithText(string text) 
        {
            _content.Text = text;
            return this;
        }

        /// <inheritdoc/>
        public void AddContent(IContent content)
        {
            _content.ChildContent.Add(content);
        }

        /// <inheritdoc/>
        protected override ListItemBuilder<Parent> This()
        {
            return this;
        }
    }
}
