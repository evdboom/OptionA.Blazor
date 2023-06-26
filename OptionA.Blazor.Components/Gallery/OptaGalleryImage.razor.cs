using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Image inside the gallery
    /// </summary>
    public partial class OptaGalleryImage
    {
        private bool _registered;

        [Inject]
        private IGallerylDataProvider Provider { get; set; } = null!;

        /// <summary>
        /// <see cref="OptaGallery"/> as parent
        /// </summary>
        [CascadingParameter(Name = "GalleryParent")]
        public OptaGallery? Parent { get; set; }
        /// <summary>
        /// Additional classes to set
        /// </summary>
        [Parameter]
        public string? AdditionalClasses { get; set; }
        /// <summary>
        /// Image
        /// </summary>
        [Parameter]
        public string ImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// Optional thumbnail url to display in thumbnail container, will default to <see cref="ImageUrl"/> if not set
        /// </summary>
        [Parameter]
        public string? ImageThumbnailUrl { get; set; }
        /// <summary>
        /// Text to use for image as alt and title attributes
        /// </summary>
        [Parameter]
        public string? ImageText { get; set; }
        /// <summary>
        /// Image number to order the gallery by
        /// </summary>
        [Parameter]
        public int ImageNumber { get; set; }
        /// <summary>
        /// Gets or sets if this is the previous slide, call <see cref="Update"/> after changes to rerender
        /// </summary>
        public bool IsPrevious { get; set; }
        /// <summary>
        /// Gets or sets if this is the current slide, call <see cref="Update"/> after changes to rerender
        /// </summary>
        public bool IsCurrent { get; set; }
        /// <summary>
        /// Gets or sets if this is the next slide, call <see cref="Update"/> after changes to rerender
        /// </summary>
        public bool IsNext { get; set; }
        /// <summary>
        /// Gets or sets if this was a next slide in the previous iteration, call <see cref="Update"/> after changes to rerender
        /// </summary>
        public bool WasNext { get; set; }
        /// <summary>
        /// Tells the component its state has changes (use after changing the status boolean)
        /// </summary>
        public void Update()
        {
            StateHasChanged();
        }

        /// <summary>
        /// <inheritdoc/>
        /// Used to register with parent
        /// </summary>
        protected override void OnParametersSet()
        {
            if (!_registered && Parent != null)
            {
                _registered = true;
                Parent.RegisterChild(this);
            }
        }
    }
}
