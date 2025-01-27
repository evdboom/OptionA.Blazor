namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for tables
    /// </summary>
    public class TableContent : Content
    {
        /// <inheritdoc/>
        public override ContentType Type => ContentType.Table;
        /// <inheritdoc/>
        public override bool IsInvalid => !Rows.Any();

        /// <summary>
        /// Items to display in the table
        /// </summary>
        public IList<IList<string>> Rows { get; set; } = [];
        /// <summary>
        /// Headers for the table
        /// </summary>
        public IList<string> Headers { get; set; } = [];
        /// <summary>
        /// Footer for the table
        /// </summary>
        public IList<string> Footer { get; set; } = [];
    }
}
