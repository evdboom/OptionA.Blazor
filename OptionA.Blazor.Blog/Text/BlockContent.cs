namespace OptionA.Blazor.Blog;

/// <summary>
/// Content for a block of text
/// </summary>
public class BlockContent : TextContent
{
    /// <inheritdoc/>
    public override ContentType Type => ContentType.Block;
}
