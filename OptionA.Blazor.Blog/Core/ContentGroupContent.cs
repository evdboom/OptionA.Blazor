namespace OptionA.Blazor.Blog;

/// <summary>
/// Content for Content group
/// </summary>
public class ContentGroupContent : Content
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override ContentType Type => ContentType.ContentGroup;

    /// <summary>
    /// Invalid if no content is supplied
    /// </summary>
    public override bool IsInvalid => !Content.Any();

    /// <summary>
    /// Content to display in the group
    /// </summary>
    public IList<IContent> Content { get; set; } = [];
}
