namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for an inline block of text
    /// </summary>
    public class InlineContent : TextContent
    {
        /// <inheritdoc/>
        public override ContentType Type => ContentType.Inline;
    }
}
