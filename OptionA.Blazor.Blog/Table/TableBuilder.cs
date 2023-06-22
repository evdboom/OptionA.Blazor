namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Builder for <see cref="TableContent"/>
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public class TableBuilder<Parent> : ContentBuilderBase<TableBuilder<Parent>, Parent, TableContent>, IContentParentBuilder
        where Parent : IParentBuilder
    {
        /// <inheritdoc/>
        public IPost? Post => _result.Post;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="parent"></param>
        public TableBuilder(Parent parent) : base(parent)
        {
        }

        /// <summary>
        /// Creates a tablerow builder to set the columns of the table
        /// </summary>
        /// <returns></returns>
        public TableRowBuilder<Parent> CreateColumns()
        {
            return new TableRowBuilder<Parent>(this, true);
        }

        /// <summary>
        /// Adds a column to the table
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public TableBuilder<Parent> AddColumn(IContent column)
        {
            _content.Columns.ChildContent.Add(column);
            return this;
        }

        /// <inheritdoc/>
        public void AddContent(IContent content)
        {
            if (content is not TableRowContent row)
            {
                throw new InvalidOperationException($"Can only add {nameof(TableRowContent)} to a table");
            }

            if (row.ColumnRow)
            {
                _content.Columns = row;
            }
            else
            {
                _content.ChildContent.Add(row);
            }

        }

        /// <summary>
        /// Creates a builder to add a row to the table
        /// </summary>
        /// <returns></returns>
        public TableRowBuilder<Parent> CreateRow()
        {
            return new TableRowBuilder<Parent>(this, false);
        }

        /// <inheritdoc/>
        protected override TableBuilder<Parent> This()
        {
            return this;
        }
    }
}
