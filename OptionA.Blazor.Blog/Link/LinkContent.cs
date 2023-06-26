using System.Text.Json;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for the <see cref="Link"/> component, inherits for <see cref="BlockContent"/>
    /// </summary>
    public class LinkContent : BlockContent
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public LinkContent() : base() { }
        /// <summary>
        /// Constructor for use in deserialization
        /// </summary>
        /// <param name="items"></param>
        public LinkContent(Dictionary<string, JsonElement> items) : base(items) 
        {
            if (items.TryGetValue(nameof(Href), out var href))
            {
                Href = JsonSerializer.Deserialize<string>(href) ?? string.Empty;
            }
            if (items.TryGetValue(nameof(Mode), out var mode))
            {
                Mode = JsonSerializer.Deserialize<LinkMode>(mode);
            }
        }

        /// <summary>
        /// Href for the link
        /// </summary>
        public string Href { get; set; } = string.Empty;
        /// <summary>
        /// Linkmode for this link, see <see cref="LinkMode"/> for options
        /// </summary>
        public LinkMode Mode { get; set; }
        /// <inheritdoc/>
        public override ComponentType Type => ComponentType.Link;
        /// <inheritdoc/>
        public override IDictionary<string, object?> Attributes
        {
            get
            {
                var attributes = base.Attributes;
                attributes["href"] = Href;
                switch (Mode)
                {
                    case LinkMode.Self:
                        attributes["target"] =  "_self";
                        break;
                    case LinkMode.NewTab:
                        attributes["target"] = "_blank";
                        break;
                }
                
                return attributes;
            }
        }

        /// <inheritdoc />
        protected override void OnSerialize(Dictionary<string, object> items)
        {
            base.OnSerialize(items);
            items[nameof(Href)] = Href;
            items[nameof(Mode)] = Mode;

        }
    }
}
