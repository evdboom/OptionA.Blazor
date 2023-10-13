namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for header parts
    /// </summary>
    public class HeaderContent : Content
    {
        /// <inheritdoc/>
        public override ContentType Type => ContentType.Header;
        /// <summary>
        /// Content to display
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// Size of the header
        /// </summary>
        public HeaderSize Size { get; set; }
        /// <inheritdoc/>
        public override bool IsInvalid => string.IsNullOrEmpty(Content);
    }
}
