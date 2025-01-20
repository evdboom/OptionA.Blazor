namespace OptionA.Blazor.Blog;

/// <summary>
/// Interface for markdownparsing
/// </summary>
public interface IMarkDownParser
{
    /// <summary>
    /// Parse content into a collection of content items to render
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    IEnumerable<IContent> Parse(string? content);
}
