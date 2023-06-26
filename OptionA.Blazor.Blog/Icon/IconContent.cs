using System.Text.Json;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for Icon component
    /// </summary>
    public class IconContent : Content
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public IconContent() : base() { }
        /// <summary>
        /// Constructor for use in deserialization
        /// </summary>
        /// <param name="items"></param>
        public IconContent(Dictionary<string, JsonElement> items) : base(items)
        {
            if (items.TryGetValue(nameof(Paths), out var paths))
            {
                Paths = JsonSerializer.Deserialize<List<string>>(paths) ?? new();
            }
            if (items.TryGetValue(nameof(Height), out var height))
            {
                Height = JsonSerializer.Deserialize<string>(height);
            }
            if (items.TryGetValue(nameof(Width), out var width))
            {
                Width = JsonSerializer.Deserialize<string>(width);
            }
            if (items.TryGetValue(nameof(ViewBoxValues), out var viewBox))
            {
                ViewBoxValues = JsonSerializer.Deserialize<int[]>(viewBox) ?? new int[4];
            }
            if (items.TryGetValue(nameof(Mode), out var mode))
            {
                Mode = JsonSerializer.Deserialize<IconMode>(mode);
            }
        }

        /// <summary>
        /// Paths to render
        /// </summary>
        public List<string> Paths { get; } = new();
        /// <summary>
        /// Width when in Pathing mode
        /// </summary>
        public string? Width { get; set; }
        /// <summary>
        /// Height when in Pathing mode
        /// </summary>
        public string? Height { get; set; }
        /// <summary>
        /// Viewbox for when path is set
        /// </summary>
        public int[] ViewBoxValues { get; } = new int[4];  
        /// <summary>
        /// Gets the mode to render
        /// </summary>
        public IconMode Mode { get; set; }
        /// <inheritdoc/>
        public override ComponentType Type => ComponentType.Icon;

        /// <inheritdoc/>
        public override IDictionary<string, object?> Attributes 
        {
            get
            {
                var attributes = base.Attributes;               
                if (!string.IsNullOrEmpty(Width))
                {
                    attributes["width"] = Width;
                }
                if (!string.IsNullOrEmpty(Height))
                {
                    attributes["height"] = Height;
                }
                attributes["fill"] = "currentColor";
                attributes["viewBox"] = string.Join(" ", ViewBoxValues);

                return attributes;
            }
        }

        /// <inheritdoc/>
        protected override void OnSerialize(Dictionary<string, object> items)
        {
            if (Paths.Any())
            {
                items[nameof(Paths)] = Paths;
            }
            if (!string.IsNullOrEmpty(Width))
            {
                items[nameof(Width)] = Width;
            }
            if (!string.IsNullOrEmpty(Height))
            {
                items[nameof(Height)] = Height;
            }
            if (ViewBoxValues.Any(value => value != default))
            {
                items[nameof(ViewBoxValues)] = ViewBoxValues;
            }
            items[nameof(Mode)] = Mode;
        }
    }
}
