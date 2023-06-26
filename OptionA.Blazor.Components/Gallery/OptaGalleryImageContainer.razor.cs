using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components.Gallery
{ 
    /// <summary>
    /// Container for showing large image in OptaGallery component
    /// </summary>
    public partial class OptaGalleryImageContainer
    {
        [Inject]
        private IGallerylDataProvider Provider { get; set; } = null!;
        /// <summary>
        /// <see cref="OptaGallery"/> as parent
        /// </summary>
        [CascadingParameter(Name = "GalleryParent")]
        public OptaGallery? Parent { get; set; }
        /// <summary>
        /// Occurs when next button is clicked
        /// </summary>
        [Parameter]
        public EventCallback OnSelectNext { get; set; }
        /// <summary>
        /// Occurs when previous button is clicked
        /// </summary>
        [Parameter]
        public EventCallback OnSelectPrevious { get; set; }
        /// <summary>
        /// Additional classes to add to the container
        /// </summary>
        [Parameter]
        public string? AdditionalClasses { get; set; }

        private async Task SelectNext()
        {
            if (OnSelectNext.HasDelegate)
            {
                await OnSelectNext.InvokeAsync();
            }
        }

        private async Task SelectPrevious()
        {
            if (OnSelectPrevious.HasDelegate)
            {
                await OnSelectPrevious.InvokeAsync();
            }
        }

    }
}
