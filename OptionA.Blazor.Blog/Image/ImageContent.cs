namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for the <see cref="Image.Image"/> component
    /// </summary>
    public class ImageContent : Content
    {
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
                    attributes["title"] = GetSource();
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

                attributes["src"] = GetSource();

                return attributes;
            }
        }
        /// <inheritdoc/>
        public override ComponentType Type => ComponentType.Image;

        private string GetSource()
        {
            return Mode switch
            {
                ImageMode.LocalPost => $"/images/{Post!.DateId}/{Source}",
                ImageMode.Local => $"/images/{Source}",
                ImageMode.External => Source,
                _ => throw new InvalidOperationException("Unknown ImageMode")
            };
        }
    }
}
