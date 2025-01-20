using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components;

/// <summary>
/// Gallery Component
/// </summary>
public partial class OptAGallery
{
    private List<(int Index, OptAGalleryImage Child)> _children = new();
    private int? _selectedIndex;
    private GalleryMode _oldMode;

    [Inject]
    private IGalleryDataProvider Provider { get; set; } = null!;

    /// <summary>
    /// Slides to show should be of type <see cref="OptAGalleryImage"/>
    /// </summary>
    [Parameter]
    public RenderFragment? Images { get; set; }
    /// <summary>
    /// Determines if next/previous are shown
    /// </summary>
    [Parameter]
    public bool ShowNextPrevious { get; set; } = true;
    /// <summary>
    /// Classes to add to next
    /// </summary>
    [Parameter]
    public string? NextClasses { get; set; }
    /// <summary>
    /// Classes to add to next
    /// </summary>
    [Parameter]
    public string? NextIconClasses { get; set; }
    /// <summary>
    /// Classes to add to previous
    /// </summary>
    [Parameter]
    public string? PreviousClasses { get; set; }
    /// <summary>
    /// Classes to add to previous
    /// </summary>
    [Parameter]
    public string? PreviousIconClasses { get; set; }
    /// <summary>
    /// true if next/previous should be rendered as i tags
    /// </summary>
    [Parameter]
    public bool NextPreviousIsIcon { get; set; } = true;
    /// <summary>
    /// Additional classes to add to thumbnail container
    /// </summary>
    [Parameter]
    public string? AdditionalThumbnailContainerClasses { get; set; }
    /// <summary>
    /// Additional classes to add to image container
    /// </summary>
    [Parameter]
    public string? AdditionalImageContainerClasses { get; set; }
    /// <summary>
    /// Content to load for next element, should be set if not rendered as icon,
    /// </summary>
    [Parameter]
    public RenderFragment? NextContent { get; set; }
    /// <summary>
    /// Content to load for previous element, should be set if not rendered as icon,
    /// </summary>
    [Parameter]
    public RenderFragment? PreviousContent { get; set; }
    /// <summary>
    /// <see cref="GalleryMode"/> for the gallery, side by side (larger devices) or modal (smaller devices)
    /// </summary>
    [Parameter]
    public GalleryMode Mode { get; set; }
    /// <summary>
    /// Css value for max-height to be set on thumbnail container
    /// </summary>
    [Parameter]
    public string? MaxThumbnailContainerHeight { get; set; }
    /// <summary>
    /// Set to true to also show the title of an image in the thumbnail container
    /// </summary>
    [Parameter]
    public bool ShowTitleOnThumbnail { get; set; }
    /// <summary>
    /// Percentage of width the thumbnail container takes, default = 25
    /// </summary>
    [Parameter]
    public int ThumbnailContainerPercentage { get; set; } = 25;
    /// <summary>
    /// Thumbnails per row in modal mode
    /// </summary>
    [Parameter]
    public int? ThumbnailsPerRow { get; set; }
    /// <summary>
    /// When setting the thumbnails per row, fill in the estimated gap/margin percentage here
    /// </summary>
    [Parameter]
    public int? ThumbnailsPerRowMargin { get; set; }
    /// <summary>
    /// Sets the maximum width of the images in modal mode
    /// </summary>
    [Parameter]
    public string? MaxModalWidth { get; set; }

    /// <summary>
    /// Register a child to include in slides
    /// </summary>
    /// <param name="newChild"></param>
    public void RegisterChild(OptAGalleryImage newChild)
    {
        var current = _children
            .Select(child => child.Child)
            .ToList();
        current.Add(newChild);
        _children = current
            .OrderBy(child => child.ImageNumber)
            .Select((child, index) => (index, child))
            .ToList();
        InvokeAsync(StateHasChanged);
    }

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        if (Mode != _oldMode)
        {
            _children.Clear();
            _oldMode = Mode;
            Deselect();
        }
    }

    /// <inheritdoc />
    private void Deselect()
    {
        _selectedIndex = null;
        var current = _children
            .FirstOrDefault(child => child.Child.IsCurrent);

        if (current.Child != null)
        {
            current.Child.IsCurrent = false;
            current.Child.Update();
        }

        InvokeAsync(StateHasChanged);
    }

    private void SelectIndex(int index)
    {
        if (!_children.Any())
        {
            return;
        }

        var current = _children
            .FirstOrDefault(child => child.Child.IsCurrent);

        if (current.Index == index && current.Child != null)
        {
            return;
        }

        if (current.Child != null)
        {
            current.Child.IsCurrent = false;
        }

        var newCurrent = _children[index].Child;
        newCurrent.IsCurrent = true;
        current.Child?.Update();
        newCurrent.Update();

        _selectedIndex = index;
        InvokeAsync(StateHasChanged);
    }

    private void SelectNext()
    {
        if (!_children.Any(child => child.Child.IsCurrent))
        {
            return;
        }

        var current = _children
            .First(child => child.Child.IsCurrent);
        var newIndex = current.Index < _children.Count - 1
            ? current.Index + 1
            : 0;
        SelectIndex(newIndex);
    }

    private void SelectPrevious()
    {
        if (!_children.Any(child => child.Child.IsCurrent))
        {
            return;
        }

        var current = _children
            .First(child => child.Child.IsCurrent);
        var newIndex = current.Index > 0
            ? current.Index - 1
            : _children.Max(child => child.Index);

        SelectIndex(newIndex);
    }

    private Dictionary<string, object?> GetGalleryAttributes()
    {
        var result = GetAttributes();
        result["opta-gallery"] = true;
        
        if (TryGetClasses(Provider.GetGalleryClasses(Mode), out string classes))
        {
            result["class"] = classes;
        }

        return result;
    }
}
