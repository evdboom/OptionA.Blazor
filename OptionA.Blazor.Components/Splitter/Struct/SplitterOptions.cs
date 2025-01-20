namespace OptionA.Blazor.Components;

/// <summary>
///  Options for the splitter component
/// </summary>
public class SplitterOptions
{
    /// <summary>
    /// Default dragmode for splitters
    /// </summary>
    public DragMode DragMode { get; set; }
    /// <summary>
    /// Content to set inside dragbar
    /// </summary>
    public string? DragBarContent { get; set; }
    /// <summary>
    /// Class to add to the dragbar
    /// </summary>
    public string? DragBarClass { get; set; }
    /// <summary>
    /// Class to add to outline in case of dragmode outline
    /// </summary>
    public string? OutlineClass { get; set; }
}
