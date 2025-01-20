using System.Diagnostics.CodeAnalysis;

namespace OptionA.Blazor.Blog.Text.Parser;

/// <summary>
/// Definition for markdown markers
/// </summary>
public interface IMarkerDefinition
{
    /// <summary>
    /// Priority if the definition, for instance to first determine bold before italic in markdown
    /// </summary>
    int Priority { get; }
    /// <summary>
    /// First character of the starting sequence, to determine a mark might start
    /// </summary>
    char FirstChar { get; }
    /// <summary>
    /// Starting sequence for this marker
    /// </summary>
    string Starter { get; }
    /// <summary>
    /// Ending sequence for this marker
    /// </summary>
    string Ender { get; }
    /// <summary>
    /// MArker type
    /// </summary>
    MarkerType Type { get; }
    /// <summary>
    /// Method to determine if given string is valid for this marker, will return the insided of the marker if true.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    bool IsValidForMarker(string input, [NotNullWhen(true)] out string? content);
    /// <summary>
    /// Creates linked content for this marker
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    IContent CreateContent(string content);
}
