namespace OptionA.Blazor.Blog;

/// <summary>
/// Parses a Markdown string into a list of <see cref="IContent"/> objects suitable for rendering with existing blog components.
/// </summary>
public interface IMarkdownDocumentParser
{
    /// <summary>
    /// Parses the given Markdown source and returns the corresponding content list.
    /// </summary>
    /// <param name="markdown">Markdown source text.</param>
    /// <returns>Ordered list of content items.</returns>
    IReadOnlyList<IContent> Parse(string? markdown);
}
