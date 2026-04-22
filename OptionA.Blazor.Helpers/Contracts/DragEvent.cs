using OptionA.Blazor.Blog;

namespace OptionA.Blazor.Helpers.Contracts;

/// <summary>
/// Drag event for moving content.
/// </summary>
/// <param name="Content">Dragged content.</param>
/// <param name="Above">True when dropped above the target.</param>
public record DragEvent(IContent Content, bool Above);
