using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components.Gallery
{
    /// <summary>
    /// Wrapper for gallery in modal mode
    /// </summary>
    public partial class OptaGalleryModal
    {
        [Inject]
        private IGallerylDataProvider Provider { get; set; } = null!;
        /// <summary>
        /// Set to true to show the modal
        /// </summary>
        [Parameter]
        public bool Show { get; set; }
        /// <summary>
        /// Content to display in modal, designed for <see cref="OptaGalleryImageContainer"/>
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        /// <summary>
        /// Occurs when close or outside image is clicked
        /// </summary>
        [Parameter]
        public EventCallback OnClose { get; set; }
        /// <summary>
        /// Width for the modal
        /// </summary>
        [Parameter]
        public string? ModalMaxWidth { get; set; }

        private async Task Close()
        {
            if (OnClose.HasDelegate)
            {
                await OnClose.InvokeAsync();
            }
        }

        private string? GetStyle()
        {
            return !string.IsNullOrEmpty(ModalMaxWidth)
                ? $"max-width:{ModalMaxWidth}"
                : null;
        }
    }
}
