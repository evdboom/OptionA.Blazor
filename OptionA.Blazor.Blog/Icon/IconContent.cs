namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for icons
    /// </summary>
    public class IconContent : Content
    {
        /// <inheritdoc/>
        public override ContentType Type => ContentType.Icon;

        /// <summary>
        /// Paths to render
        /// </summary>
        public IList<string> Paths { get; set; } = [];
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
        public int[] ViewBoxValues { get; set; } = new int[4];
        /// <summary>
        /// Gets the mode to render
        /// </summary>
        public IconMode Mode { get; set; }
        /// <inheritdoc/>
        public override Dictionary<string, object?> Attributes
        {
            get
            {
                var result = new Dictionary<string, object?>();
                if (Mode == IconMode.Path)
                {
                    if (!string.IsNullOrEmpty(Width))
                    {
                        result["width"] = Width;
                    }
                    if (!string.IsNullOrEmpty(Height))
                    {
                        result["height"] = Height;
                    }
                    result["fill"] = "currentColor";
                    result["viewBox"] = string.Join(" ", ViewBoxValues);
                }
                foreach (var attribute in base.Attributes)
                {
                    result[attribute.Key] = attribute.Value;
                }
                return result;
            }
        }
        /// <inheritdoc/>
        public override bool IsInvalid
        {
            get
            {
                return Mode == IconMode.Class
                    ? AdditionalClasses.Count == 0
                    : Paths.Count == 0;
            }
        }
    }
}
