namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Interface for parsing different kinds of code
    /// </summary>
    public interface ICodeParser
    {
        /// <summary>
        /// Language this parser is made for
        /// </summary>
        CodeLanguage Language { get; }
        /// <summary>
        /// Parse content into a collection of content items to render
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        IEnumerable<IContent> Parse(string? content, string? newLine);
    }
}
