using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components;

namespace OptionA.Blazor.Blog.Builder.Parts;

/// <summary>
/// Build image blog part
/// </summary>
public partial class OptAImageBuilder
{
    private const string SourceId = "opta-image-source";
    private const string TitleId = "opta-image-title";
    private const string AlternativeId = "opta-image-alt";

    /// <summary>
    /// Index of the current content in the collection
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
    public ImageContent? Content { get; set; }
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

    private Dictionary<string, object?> GetAttributes(string id, string placeholder)
    {
        var defaultAttributes = new Dictionary<string, object?>
        {
            ["placeholder"] = placeholder,
            ["id"] = $"{id}-{ContentIndex}"
        };

        return DataProvider.GetAttributes(BuilderType.TextInput, defaultAttributes);
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
