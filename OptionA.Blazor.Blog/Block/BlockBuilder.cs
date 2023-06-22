namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Builder for the <see cref="BlockContent"/>
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public class BlockBuilder<Parent> : ContentBuilderBase<BlockBuilder<Parent>, Parent, BlockContent>, IParentBuilder
        where Parent : IParentBuilder
    {
        /// <inheritdoc/>
        public IPost? Post => _result.Post;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="parent"></param>
        public BlockBuilder(Parent parent) : base(parent) 
        {
        }

        /// <summary>
        /// Sets the text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public BlockBuilder<Parent> WithText(string text)
        {
            _content.Text = text;
            return this;
        }

        /// <inheritdoc/>
        public void AddContent(IContent content)
        {
            _content.ChildContent.Add(content);
        }

        /// <summary>
        /// Adds the content to this builder, does not set any properties.
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public BlockBuilder<Parent> AddContents(IEnumerable<IContent> contents)
        {
            foreach(var content in contents)
            {
                _content.ChildContent.Add(content);
            }

            return this;
        }


        /// <inheritdoc/>
        protected override BlockBuilder<Parent> This()
        {
            return this;
        }
    }
}
