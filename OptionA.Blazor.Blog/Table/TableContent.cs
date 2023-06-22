namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for the <see cref="Table.Table"/> component
    /// </summary>
    public class TableContent : Content
    {
        /// <inheritdoc/>
        public override ComponentType Type => ComponentType.Table;

        /// <summary>
        /// Columns for the table
        /// </summary>
        public TableRowContent Columns { get; set; } = new();

    }
}
