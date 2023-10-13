namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Options for Gallery data provider
    /// </summary>
    public class GalleryOptions
    {        
        /// <summary>
        /// Classes for next button
        /// </summary>
        public string? NextClasses { get; set; }
        /// <summary>
        /// Classes for previous button
        /// </summary>
        public string? PreviousClasses { get; set; }
        /// <summary>
        /// Classes for next button icon
        /// </summary>
        public string? NextIconClasses { get; set; }
        /// <summary>
        /// Classes for previous button icon
        /// </summary>
        public string? PreviousIconClasses { get; set; }
        /// <summary>
        /// Classes for the thumbnail container in side by side mode
        /// </summary>
        public string? SideBySideThumbnailContainerClasses { get; set; }
        /// <summary>
        /// Classes for the thumbnail container in modal mode
        /// </summary>
        public string? ModalThumbnailContainerClasses { get; set; }
        /// <summary>
        /// Classes for the image container in side by side mode
        /// </summary>
        public string? SideBySideImageContainerClasses { get; set; }
        /// <summary>
        /// Classes for the image container in modal mode
        /// </summary>
        public string? ModalImageContainerClasses { get; set; }
        /// <summary>
        /// Classes for the main gallery in side by side mode
        /// </summary>
        public string? SideBySideGalleryClasses { get; set; }
        /// <summary>
        /// Classes for the main gallery in modal mode
        /// </summary>
        public string? ModalGalleryClasses { get; set; }
        /// <summary>
        /// Default classes for main image
        /// </summary>
        public string? ImageClasses { get; set; }
        /// <summary>
        /// Default classes for thumbnails
        /// </summary>
        public string? ThumbnailClasses { get; set; }
        /// <summary>
        /// Classes for the gallery close button in modal mode
        /// </summary>
        public string? ModalCloseButtonClasses { get; set; }
        /// <summary>
        /// Classes for the modal background of the gallery in modal mode
        /// </summary>
        public string? ModalBackgroundClasses { get; set; }
        /// <summary>
        /// Classes for the gallery in modal mode 
        /// </summary>
        public string? ModalClasses { get; set; }
        /// <summary>
        /// Text to display in modal close button
        /// </summary>
        public string? ModalCloseButtonText { get; set; }
        /// <summary>
        /// Default classes for the modal wrapper in modal mode
        /// </summary>
        public string? ModalWrapperClasses { get; set; }
    }
}
