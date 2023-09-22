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
    }
}
