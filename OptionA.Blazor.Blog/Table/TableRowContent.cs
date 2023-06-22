namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content of a table row
    /// </summary>
    public class TableRowContent : Content
    {
        /// <summary>
        /// True if this is the column (header) tow
        /// </summary>
        public bool ColumnRow { get; set; }
        /// <inheritdoc/>
        public override ComponentType Type => ComponentType.Row;
    }
}
