namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for a quote
    /// </summary>
    public class QuoteContent : TextContent
    {
        /// <inheritdoc/>
        public override ContentType Type => ContentType.Quote;        
    }
}
