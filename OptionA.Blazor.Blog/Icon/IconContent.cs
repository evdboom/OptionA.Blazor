namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for Icon component
    /// </summary>
    public class IconContent : Content
    {
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
    }
}
