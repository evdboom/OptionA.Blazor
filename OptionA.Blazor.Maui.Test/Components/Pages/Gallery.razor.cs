using OptionA.Blazor.Components;

namespace OptionA.Blazor.Maui.Test.Components.Pages;

public partial class Gallery
{
    private bool _show;
    private bool _showNextPrevious = true;
    private GalleryMode _mode;
    private string? _thumbnailContainerHeight = "50vh";
    private bool _showTitleOnThumbnail = true;
    private int _containerPercentage = 25;
    private int? _thumbsPerRow = 3;
    private int? _thumbsPerRowMargin = 4;
    private string? _modalWidth = "400px";

    private void ChangeGalleryMode()
    {
        _mode = _mode == GalleryMode.SideBySide
            ? GalleryMode.Modal
            : GalleryMode.SideBySide;
    }
}
