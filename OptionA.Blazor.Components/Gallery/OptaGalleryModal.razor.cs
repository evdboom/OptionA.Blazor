using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components.Gallery
{
    /// <summary>
    /// Wrapper for gallery in modal mode
    /// </summary>
    public partial class OptAGalleryModal
    {
        [Inject]
        private IGalleryDataProvider Provider { get; set; } = null!;
        /// <summary>
        /// Set to true to show the modal
        /// </summary>
        [Parameter]
        public bool Show { get; set; }
        /// <summary>
        /// Content to display in modal, designed for <see cref="OptAGalleryImageContainer"/>
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

        private Dictionary<string, object?> GetWrapperAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-modal-wrapper"] = true
            };

            if (!string.IsNullOrEmpty(Provider.ModalWrapperClasses()))
            {
                result["class"] = Provider.ModalWrapperClasses();
            }

            if (Show)
            {
                result["show"] = true;
            }

            return result;
        }

        private Dictionary<string, object?> GetModalAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-gallery-modal"] = true
            };

            if (TryGetClasses(Provider.ModalClasses(), out var classes))
            {
                result["class"] = classes;
            }

            if (!string.IsNullOrEmpty(ModalMaxWidth))
            {
                result["style"] = $"max-width:{ModalMaxWidth}";
            }

            return result;
        }

        private Dictionary<string, object?> GetCloseButtonAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-gallery-close"] = true,
                ["type"] = "button"
            };

            if (!string.IsNullOrEmpty(Provider.ModalCloseButtonClasses()))
            {
                result["class"] = Provider.ModalCloseButtonClasses();
            }

            return result;
        }

        private Dictionary<string, object?> GetBackgroundAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-gallery-modal-background"] = true,
            };

            if (!string.IsNullOrEmpty(Provider.ModalBackgroundClasses()))
            {
                result["class"] = Provider.ModalBackgroundClasses();
            }

            return result;
        }
    }
}
