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
        /// Items to display in the list, set either Items or ItemsFromString though Items takes precedence if both are set
        /// </summary>
        public IList<string> Items { get; } = [];
        /// <summary>
        /// Items to display in the list as a string, set either Items or ItemsFromString though Items takes precedence if both are set
        /// </summary>
        public string ItemsFromString { get; set; } = string.Empty;

        /// <inheritdoc/>
        public override bool IsInvalid => Items.Count == 0;
    }
}