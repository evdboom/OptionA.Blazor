using System.Text.Json;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for the <see cref="Table"/> component
    /// </summary>
    public class TableContent : Content
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TableContent() : base() { }
        /// <summary>
        /// Constructor for use in deserialization
        /// </summary>
        /// <param name="items"></param>
        public TableContent(Dictionary<string, JsonElement> items) : base(items)
        {
            if (items.TryGetValue(nameof(Columns), out var columns))
            {
                var content = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(columns) ?? new();
                Columns = new TableRowContent(content);
            }
        }
        /// <inheritdoc/>
        public override ComponentType Type => ComponentType.Table;

        /// <summary>
        /// Columns for the table
        /// </summary>
        public TableRowContent Columns { get; set; } = new();

        /// <inheritdoc />
        protected override void OnSerialize(Dictionary<string, object> items)
        {
            items[nameof(Columns)] = Columns.GetSerializationData();
        }

    }
}
