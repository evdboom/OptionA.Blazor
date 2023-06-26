using System.Text.Json;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for the <see cref="ListItem"/> component, inherits from <see cref="BlockContent"/>
    /// </summary>
    public class ListItemContent : BlockContent
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ListItemContent() : base() { }
        /// <summary>
        /// Constructor for use in deserialization
        /// </summary>
        /// <param name="items"></param>
        public ListItemContent(Dictionary<string, JsonElement> items) : base(items) { }

        /// <inheritdoc/>
        public override ComponentType Type => ComponentType.Row;
    }
}
