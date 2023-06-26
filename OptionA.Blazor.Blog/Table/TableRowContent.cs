using System.Text.Json;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content of a table row
    /// </summary>
    public class TableRowContent : Content
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TableRowContent() : base() { }
        /// <summary>
        /// Constructor for use in deserialization
        /// </summary>
        /// <param name="items"></param>
        public TableRowContent(Dictionary<string, JsonElement> items) : base(items)
        {
            if (items.TryGetValue(nameof(ColumnRow), out var columnRow))
            {
                ColumnRow = JsonSerializer.Deserialize<bool>(columnRow);
            }
        }

        /// <summary>
        /// True if this is the column (header) tow
        /// </summary>
        public bool ColumnRow { get; set; }
        /// <inheritdoc/>
        public override ComponentType Type => ComponentType.Row;

        /// <inheritdoc />
        protected override void OnSerialize(Dictionary<string, object> items)
        {
            items[nameof(ColumnRow)] = ColumnRow;
        }
    }
}
