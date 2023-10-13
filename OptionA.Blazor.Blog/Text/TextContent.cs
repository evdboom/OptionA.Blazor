namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for basic text, text is parsed directly without formatting of its own
    /// </summary>
    public class TextContent : Content
    {
        /// <inheritdoc/>
        public override ContentType Type => ContentType.Text;
        /// <summary>
        /// Text to display
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// Set to true to skip markdown detection
        /// </summary>
        public bool NotMarkdown { get; set; }
        /// <inheritdoc/>
        public override bool IsInvalid => !string.IsNullOrEmpty(Content);
    }
}
