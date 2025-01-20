using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;

namespace OptionA.Blazor.Components.Gallery;


/// <summary>
/// Container for use with the OptaGallery
/// </summary>
public partial class OptAGalleryThumbnailContainer
{
    private const string ScrollIntoViewFunction = "scrollActiveIntoView";        
    private Lazy<Task<IJSObjectReference>>? _moduleTask;
    private int _oldActive;

    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject]
    private IGalleryDataProvider Provider { get; set; } = null!;
    /// <summary>
    /// Mode for the gallery, in <see cref="GalleryMode.SideBySide"/> flex direction will be column, otherwise row
    /// </summary>
    [Parameter]
    public GalleryMode Mode { get; set; }
    /// <summary>
    /// Images to display thumbnails for
    /// </summary>
    [Parameter]
    public List<(int Index, OptAGalleryImage Image)>? Images { get; set; }
    /// <summary>
    /// Maxheight to set for the thumbnail container
    /// </summary>
    [Parameter]
    public string? MaxHeight { get; set; }
    /// <summary>
    /// True to display the <see cref="OptAGalleryImage.ImageText"/> as title, default = true
    /// </summary>
    [Parameter]
    public bool ShowTitleOnThumbnail { get; set; } = true;
    /// <summary>
    /// Callback whenever a thumbnail gets selected
    /// </summary>
    [Parameter]
    public EventCallback<int> OnImageSelected { get; set; }
    /// <summary>
    /// Currently active index
    /// </summary>
    [Parameter]
    public int? ActiveIndex { get; set; }
    /// <summary>
    /// Percentage width of total galley container may take in side by side mode.
    /// </summary>
    [Parameter]
    public int ThumbnailContainerPercentage { get; set; }
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

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        _moduleTask = new(() => JsRuntime.InvokeAsync<IJSObjectReference>(
            "import",
            "./_content/OptionA.Blazor.Components/Gallery/OptAGalleryThumbnailContainer.razor.js").AsTask());
    }

    /// <inheritdoc/>
    protected override async Task OnParametersSetAsync()
    {
        if (ActiveIndex.HasValue && ActiveIndex.Value != _oldActive)
        {
            _oldActive = ActiveIndex.Value;
            var module = await _moduleTask!.Value;
            await module.InvokeVoidAsync(ScrollIntoViewFunction, ActiveIndex);
        }
    }

    private Dictionary<string, object?> GetContainerAttributes()
    {
        var result = GetAttributes();
        result["opta-gallery-thumbnail-container"] = true;            

        if (TryGetClasses(Provider.GetThumbnailContainerClasses(Mode), out var classes))
        {
            result["class"] = classes;
        }

        switch (Mode)
        {
            case GalleryMode.SideBySide:
                result["gallery-mode"] = "side-by-side";
                break;
            case GalleryMode.Modal:
                result["gallery-mode"] = "modal";
                break;
        }

        var style = GetStyle();
        if (!string.IsNullOrEmpty(style))
        {
            result["style"] = style;
        }

        return result;
    }

    private Dictionary<string, object?> GetThumbnailAttributes(int index)
    {
        var result = new Dictionary<string, object?>
        {
            ["opta-thumbnail-image"] = true,
            ["opta-index"] = $"{index}"
        };

        if (ActiveIndex == index)
        {
            result["active"] = true;
        }
        var style = GetThumbnailStyle();

        if (!string.IsNullOrEmpty(style))
        {
            result["style"] = style;
        }

        if (!string.IsNullOrEmpty(Provider.GetDefaultThumbnailClasses()))
        {
            result["class"] = Provider.GetDefaultThumbnailClasses();
        }

        return result;
    }

    private Dictionary<string, object?> GetImageAttributes(OptAGalleryImage image)
    {
        var result = new Dictionary<string, object?>()
        {
            ["src"] = !string.IsNullOrEmpty(image.ImageThumbnailUrl)
            ? image.ImageThumbnailUrl
            : image.ImageUrl
        };

        if (!string.IsNullOrEmpty(image.ImageText))
        {
            result["alt"] = image.ImageText;
            result["title"] = image.ImageText;
        }

        return result;
    }

    private string? GetStyle()
    {
        var builder = new StringBuilder();
        if (Mode == GalleryMode.SideBySide)
        {
            builder
                .Append($"flex: 0 0 {ThumbnailContainerPercentage}%;");            
        }
        if (!string.IsNullOrEmpty(MaxHeight))
        {
            builder
                .Append($"max-height:{MaxHeight};")
                .Append("overflow:auto;");
        }
        return builder.Length > 0
            ? builder.ToString()
            : null;
    }

    private string? GetThumbnailStyle()
    {
        if (Mode == GalleryMode.SideBySide || !ThumbnailsPerRow.HasValue)
        {
            return null;
        }

        var full = 100 - (ThumbnailsPerRowMargin ?? 0);

        return $"flex: 0 0 {full / ThumbnailsPerRow}%";

    }

    private async Task SelectImage(int index)
    {
        if (OnImageSelected.HasDelegate)
        {
            await OnImageSelected.InvokeAsync(index);
        }
    }
}
