namespace OptionA.Blazor.Blog;

/// <summary>
/// Content for italic text
/// </summary>
public class ItalicContent : TextContent
{
    /// <inheritdoc/>
    public override ContentType Type => ContentType.Italic;
}
