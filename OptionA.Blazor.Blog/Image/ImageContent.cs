using System.Text.Json;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for the <see cref="Image"/> component
    /// </summary>
    public class ImageContent : Content
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ImageContent() : base() { }
        /// <summary>
        /// Constructor for use in deserialization
        /// </summary>
        /// <param name="items"></param>
        public ImageContent(Dictionary<string, JsonElement> items) : base(items)
        {
            if (items.TryGetValue(nameof(Source), out var source))
            {
                Source = JsonSerializer.Deserialize<string>(source) ?? string.Empty;
            }
            if (items.TryGetValue(nameof(Width), out var width))
            {
                Width = JsonSerializer.Deserialize<string>(width);
            }
            if (items.TryGetValue(nameof(Height), out var height))
            {
                Height = JsonSerializer.Deserialize<string>(height);
            }
            if (items.TryGetValue(nameof(Mode), out var mode))
            {
                Mode = JsonSerializer.Deserialize<ImageMode>(mode);
            }
        }

        /// <summary>
        /// Source of the image
        /// </summary>
        public string Source { get; set; } = string.Empty;
        /// <summary>
        /// Sets the width of the image
        /// </summary>
        public string? Width { get; set; }
        /// <summary>
        /// Sets the height of the image
        /// </summary>
        public string? Height { get; set; }
        /// <summary>
        /// Mode for the image
        /// </summary>
        public ImageMode Mode { get; set; }
        /// <inheritdoc/>
        public override IDictionary<string, object?> Attributes
        {
            get
            {
                var attributes = base.Attributes;
                if (!attributes.ContainsKey("title"))
                {
                    attributes["title"] = Source;
                }
                if (!attributes.ContainsKey("alt"))
                {
                    attributes["alt"] = attributes["title"];
                }
                if (!string.IsNullOrEmpty(Width))
                {
                    attributes["width"] = Width;
                }
                if (!string.IsNullOrEmpty(Height))
                {
                    attributes["height"] = Height;
                }

                attributes["src"] = Source;

                return attributes;
            }
        }
        /// <inheritdoc/>
        public override ComponentType Type => ComponentType.Image;

        /// <inheritdoc/>
        protected override void OnSerialize(Dictionary<string, object> items)
        {
            items[nameof(Source)] = Source;
            if (!string.IsNullOrEmpty(Width))
            {
                items[nameof(Width)] = Width;
            }
            if (!string.IsNullOrEmpty(Height))
            {
                items[nameof(Height)] = Height;
            }
            items[nameof(Mode)] = Mode;
        }
    }
}
