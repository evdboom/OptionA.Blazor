using OptionA.Blazor.Components;

namespace OptionA.Blazor.Test.Pages
{
    public partial class GallerySwitch
    {
        private string GetUrl(int i) => $"https://mctsummiteu.blob.core.windows.net/sitecontent/location/Thumbnails/Breakout_{i}.jpg";
        private GalleryMode _mode;

        private void Switch()
        {
            _mode = _mode == GalleryMode.SideBySide
                ? GalleryMode.Modal
                : GalleryMode.SideBySide;
            StateHasChanged();
        }
    }
}
