using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components;

namespace OptionA.Blazor.Blog.Builder.Parts;

/// <summary>
/// Header builder component
/// </summary>
public partial class OptAHeaderBuilder
{
    private const string SizeId = "opta-header-size";
    private const string HeaderId = "opta-header";
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
    public HeaderContent? Content { get; set; }
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
    
    private BindMode? _bindMode = BindMode.OnChange;

    private HeaderSize? InternalSize
    {
        get => Content?.Size ?? default;
        set
        {
            if (Content is null || !value.HasValue)
            {
                return;
            }
            if (!value.Value.Equals(Content.Size))
            {
                Content.Size = value.Value;
                if (ContentChanged.HasDelegate)
                {
                    ContentChanged.InvokeAsync();
                }
            }
        }
    }

    private Dictionary<string, object?> GetHeaderAttributes()
    {
        var defaultAttributes = new Dictionary<string, object?>
        {
            ["placeholder"] = "Header...",
            ["id"] = $"{HeaderId}-{ContentIndex}"
        };

        return DataProvider.GetAttributes(BuilderType.TextInput, defaultAttributes);
    }

    private Dictionary<string, object?> GetSizeAttributes()
    {
        var defaultAttributes = new Dictionary<string, object?>
        {
            ["id"] = $"{SizeId}-{ContentIndex}"
        };

        return DataProvider.GetAttributes(BuilderType.SelectInput, defaultAttributes);
    }

    private Dictionary<string, object?> GetLabelAttributes(string id)
    {
        var defaultAttributes = new Dictionary<string, object?>
        {
            ["for"] = $"{id}-{ContentIndex}"
        };

        return DataProvider.GetAttributes(BuilderType.Label, defaultAttributes);
    }
}
