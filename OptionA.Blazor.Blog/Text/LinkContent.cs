namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for a link
    /// </summary>
    public class LinkContent : TextContent
    {
        /// <inheritdoc/>
        public override ContentType Type => ContentType.Link;
        /// <summary>
        /// Location to go to
        /// </summary>
        public string? Href { get; set; }
        /// <summary>
        /// Target for link
        /// </summary>
        public string? Target { get; set; }

        /// <inheritdoc/>
        public override Dictionary<string, object?> Attributes
        {
            get
            {
                var result = new Dictionary<string, object?>
                {
                    ["href"] = Href
                };

                if (!string.IsNullOrEmpty(Target))
                {
                    result["target"] = Target;
                }

                foreach (var attribute in base.Attributes)
                {
                    result[attribute.Key] = attribute.Value;
                }
                return result;
            }
        }
    }
}
