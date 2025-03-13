using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components;

namespace OptionA.Blazor.Blog.Builder.Parts;

/// <summary>
/// Build list blog part
/// </summary>
public partial class OptAListBuilder
{
    private readonly List<string> NewLineSplitters = ["\r\n", "\n"];
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
    /// <summary>
    /// Called when the component is moved to a new index
    /// </summary>
    [Parameter]
    public EventCallback<int> MovedToIndex { get; set; }
    [Inject]
    private IBlogBuilderDataProvider DataProvider { get; set; } = null!;

    private int _inputMethod;
    private BindMode? _bindMode = BindMode.OnChange;
    private bool _showAutoGrow = false;
    private bool _autoGrow = true;

    private ListType? InternalListType
    {
        get => Content?.ListType;
        set
        {
            if (Content != null)
            {
                Content.ListType = value ?? ListType.UnorderedList;
            }            
        }
    }

    private string ItemsFromString
    {
        get => string.Join(Environment.NewLine, Content?.Items ?? []) ?? string.Empty;
        set
        {
            if (Content is not null)
            {
                foreach(var splitter in NewLineSplitters)
                {
                    var items = value.Split(splitter);
                    if (items.Length > 1)
                    {
                        Content.Items = items.ToList();
                        return;
                    }
                }
                
                Content.Items = value.Split(Environment.NewLine).ToList();
            }
        }
    }
}