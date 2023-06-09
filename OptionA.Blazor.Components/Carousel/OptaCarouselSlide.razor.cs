using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Slide for the carousel component
    /// </summary>
    public partial class OptaCarouselSlide
    {
        private bool _registered;

        /// <summary>
        /// <see cref="OptaCarousel"/> as parent
        /// </summary>
        [CascadingParameter(Name="Parent")]
        public OptaCarousel? Parent { get; set; }
        /// <summary>
        /// Additional classes to set
        /// </summary>
        [Parameter]
        public string? AdditionalClasses { get; set; }
        /// <summary>
        /// Child content for the slde
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        /// <summary>
        /// Slide number, carousel will order slides by slidenumber
        /// </summary>
        [Parameter]
        public int SlideNumber { get; set; }
        /// <summary>
        /// Optional image to use as background
        /// </summary>
        [Parameter]
        public string? ImageUrl { get; set; }
        /// <summary>
        /// Text to use for image as alt and title attributes
        /// </summary>
        [Parameter]
        public string? ImageText { get; set; }
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
        /// Ùsed to register with parent
        /// </summary>
        protected override void OnParametersSet()
        {
            if (!_registered && Parent != null)
            {
                _registered = true;
                Parent.RegisterChild(this);
            }
        }

        private string GetStatusClass()
        {
            if (IsCurrent)
            {
                return "active";
            }
            else if (IsPrevious)
            {
                return WasNext
                    ? "previous was-next"
                    : "previous";                
            }
            else if (IsNext)
            {
                return "next";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
