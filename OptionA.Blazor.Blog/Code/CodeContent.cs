namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for displaying code
    /// </summary>
    public class CodeContent : Content
    {
        /// <inheritdoc/>
        public override ContentType Type => ContentType.Code;
        /// <summary>
        /// Code to display
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Language of the code
        /// </summary>
        public CodeLanguage Language { get; set; }
    }
}
