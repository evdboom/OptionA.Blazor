using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Image inside the gallery
    /// </summary>
    public partial class OptAGalleryImage
    {
        private bool _registered;

        [Inject]
        private IGalleryDataProvider Provider { get; set; } = null!;

        /// <summary>
        /// <see cref="OptAGallery"/> as parent
        /// </summary>
        [CascadingParameter(Name = "GalleryParent")]
        public OptAGallery? Parent { get; set; }
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
        /// Optional content for display below the image
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        /// <summary>
        /// Gets or sets if this is the current slide, call <see cref="Update"/> after changes to rerender
        /// </summary>
        public bool IsCurrent { get; set; }
        /// <summary>
        /// Tells the component its state has changes (use after changing the status boolean)
        /// </summary>
        public void Update()
        {
            InvokeAsync(StateHasChanged);
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

        private Dictionary<string, object?> GetListAttributes()
        {
            var result = GetAttributes();

            if (TryGetClasses(Provider.GetDefaultImageClasses(), out var classes))
            {
                result["class"] = classes;
            }

            if (IsCurrent)
            {
                result["active"] = true;
            }
            return result;
        }

        private Dictionary<string, object?> GetImageAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["src"] = ImageUrl,
                ["opta-gallery-image"] = true
            };

            if (!string.IsNullOrEmpty(ImageText))
            {
                result["alt"] = ImageText;
                result["title"] = ImageText;
            }
            
            return result;
        }
    }
}
