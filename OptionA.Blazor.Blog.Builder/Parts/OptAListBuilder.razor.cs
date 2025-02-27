using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.Parts;

/// <summary>
/// Build list blog part
/// </summary>
public partial class OptAListBuilder
{

    /// <summary>
    /// Index of content in post (for id uniqueness)
    /// </summary>
    [Parameter]
    public int ContentIndex { get; set; }
    /// <summary>
    /// Total number of content (for disabling move up, move down)
    /// </summary>
    [Parameter]
    public int TotalContentCount { get; set; }
    /// <summary>
    /// Content to create
    /// </summary>
    [Parameter]
    public ListContent? Content { get; set; }
    /// <summary>
    /// Called when something changes
    /// </summary>
    [Parameter]
    public EventCallback ContentChanged { get; set; }
    /// <summary>
    /// Called when content should be removed
    /// </summary>
    [Parameter]
    public EventCallback ContentRemoved { get; set; }
    /// <summary>
    /// Occurs when move up is clicked
    /// </summary>
    [Parameter]
    public EventCallback MovedUp { get; set; }
    /// <summary>
    /// Occurs when move down is clicked
    /// </summary>
    [Parameter]
    public EventCallback MovedDown { get; set; }
    /// <summary>
    /// Called when the drag operation is started
    /// </summary>
    [Parameter]
    public EventCallback<DragEvent> DragStarted { get; set; }
    /// <summary>
    /// Called when the drag operation is ended
    /// </summary>
    [Parameter]
    public EventCallback<DragEvent> DragEnded { get; set; }
    [Inject]
    private IBlogBuilderDataProvider DataProvider { get; set; } = null!;

    private int _inputMethod;

    private string ItemsFromString
    {
        get => string.Join(Environment.NewLine, Content?.Items ?? []) ?? string.Empty;
        set
        {
            if (Content is not null)
            {
                Content.Items = value.Split(Environment.NewLine).ToList();
            }
        }
    }
}