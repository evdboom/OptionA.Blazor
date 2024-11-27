namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for list parts
    /// </summary>
    public class ListContent : Content
    {
        /// <inheritdoc/>
        public override ContentType Type => ContentType.List;
        /// <summary>
        /// Type of list, ordered or unordered
        /// </summary>
        public ListType ListType { get; set; }
        /// <summary>
        /// Items to display in the list
        /// </summary>
        public List<string> Items { get; } = [];

        /// <inheritdoc/>
        public override bool IsInvalid => Items.Count == 0;
    }
}