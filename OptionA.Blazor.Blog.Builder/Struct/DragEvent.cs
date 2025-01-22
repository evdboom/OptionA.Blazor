namespace OptionA.Blazor.Blog.Builder;

/// <summary>
/// Drag event for moving content
/// </summary>
/// <param name="Content"></param>
/// <param name="Above"></param>
public record DragEvent(IContent Content, bool Above);    
