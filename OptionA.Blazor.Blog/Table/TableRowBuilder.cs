namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Builder for creating table rows
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public class TableRowBuilder<Parent> : ContentBuilderBase<TableRowBuilder<Parent>, TableBuilder<Parent>, TableRowContent>, IParentBuilder
        where Parent : IParentBuilder
    {
        /// <inheritdoc/>
        public IPost? Post => _result.Post;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="columns"></param>
        public TableRowBuilder(TableBuilder<Parent> parent, bool columns) : base(parent)
        {
            _content.ColumnRow = columns;
        }

        /// <inheritdoc/>
        protected override TableRowBuilder<Parent> This()
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
