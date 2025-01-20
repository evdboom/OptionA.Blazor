namespace OptionA.Blazor.Components;

/// <summary>
/// When dragging, determines the way it happens
/// </summary>
public enum DragMode
{
    /// <summary>
    /// Direct, app is updated constantly
    /// </summary>
    Direct,
    /// <summary>
    /// Outline, you drag around an outline of the app, only updates when dropped
    /// </summary>
    Outline
}
