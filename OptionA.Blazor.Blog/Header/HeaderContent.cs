using System.Text.Json;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for the <see cref="Header.Header"/> component
    /// </summary>
    public class HeaderContent : BlockContent
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public HeaderContent() : base() { }
        /// <summary>
        /// Constructor for use in deserialization
        /// </summary>
        /// <param name="items"></param>
        public HeaderContent(Dictionary<string, JsonElement> items) : base(items)
        {
            if (items.TryGetValue(nameof(HeaderSize), out var size))
            {
                HeaderSize = JsonSerializer.Deserialize<HeaderSize>(size);
            }
        }

        /// <summary>
        /// Size of the header
        /// </summary>
        public HeaderSize HeaderSize { get; set; } = HeaderSize.One;
        /// <inheritdoc/>
        public override ComponentType Type => ComponentType.Header;

        /// <inheritdoc/>
        public override IDictionary<string, object?> Attributes
        {
            get 
            {                
                var attributes = base.Attributes;

                if (string.IsNullOrEmpty(Text))
                {
                    return attributes;
                }

                var value = Text
                    .ToLowerInvariant()
                    .Replace(' ', '-');

                if (!attributes.ContainsKey("name"))
                {
                    attributes["name"] = value;
                }

                if (!attributes.ContainsKey("id"))
                {
                    attributes["id"] = value;
                }

                return attributes;
            }
        }

        /// <inheritdoc />
        protected override void OnSerialize(Dictionary<string, object> items)
        {
            base.OnSerialize(items);
            items[nameof(HeaderSize)] = HeaderSize;
        }
    }
}
