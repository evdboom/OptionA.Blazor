using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Blog.Builder.HelperComponents;
using OptionA.Blazor.Components;

namespace OptionA.Blazor.Blog.Builder.Parts;

/// <summary>
/// Builder for paragraphs
/// </summary>
public partial class OptAParagraphBuilder
{
    private const string ParagraphId = "opta-paragraph";

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
    public ParagraphContent? Content { get; set; }
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

    private OptAFlexibleTextArea? _input;
    private BindMode _bindMode = BindMode.OnChange;
    private bool _showAutoGrow = false;
    private bool _autoGrow = true;

    /// <inheritdoc/>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (_input is not null)
            {
                await _input.Element.FocusAsync(false);
            }
        }
      
    }

    private Dictionary<string, object?> GetAttributes()
    {
        var defaultAttributes = new Dictionary<string, object?>
        {
            ["placeholder"] = "Text...",
            ["id"] = $"{ParagraphId}-{ContentIndex}"            
        };

        return DataProvider.GetAttributes(BuilderType.TextAreaInput, defaultAttributes);
    }
}
