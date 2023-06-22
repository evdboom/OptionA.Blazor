namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for the <see cref="Header.Header"/> component
    /// </summary>
    public class HeaderContent : BlockContent
    {
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
    }
}
