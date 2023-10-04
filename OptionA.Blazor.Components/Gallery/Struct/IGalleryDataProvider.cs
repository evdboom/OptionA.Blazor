namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Interface for use in galleries to provide the correct classes and icons
    /// </summary>
    public interface IGalleryDataProvider
    {
        /// <summary>
        /// Default classes to apply to next button
        /// </summary>
        /// <returns></returns>
        string DefaultNextClasses();
        /// <summary>
        /// Default classes to apply to next button icon
        /// </summary>
        /// <returns></returns>
        string DefaultNextIconClasses();
        /// <summary>
        /// Default classes to apply to previous button
        /// </summary>
        /// <returns></returns>
        string DefaultPreviousClasses();
        /// <summary>
        /// Default classes to apply to previous button icon 
        /// </summary>
        /// <returns></returns>
        string DefaultPreviousIconClasses();
        /// <summary>
        /// Default classes for thumbnail container for given mode
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        string GetThumbnailContainerClasses(GalleryMode mode);
        /// <summary>
        /// Default classes for image container for given mode
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        string GetImageContainerClasses(GalleryMode mode);
        /// <summary>
        /// Default classes for image
        /// </summary>
        /// <returns></returns>
        string GetDefaultImageClasses();
        /// <summary>
        /// Default classes for thumbnail
        /// </summary>
        /// <returns></returns>
        string GetDefaultThumbnailClasses();
        /// <summary>
        /// Default classes for main gallery for given mode
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        string GetGalleryClasses(GalleryMode mode);
        /// <summary>
        /// Classes for the close button in modal mode
        /// </summary>
        /// <returns></returns>
        string ModalCloseButtonClasses();
        /// <summary>
        /// Classes for the background in modal mode
        /// </summary>
        /// <returns></returns>
        string ModalBackgroundClasses();
        /// <summary>
        /// Classes for the modal
        /// </summary>
        /// <returns></returns>
        string ModalClasses();
        /// <summary>
        /// Text for the modal close button
        /// </summary>
        /// <returns></returns>
        string ModalCloseButtonText();
        /// <summary>
        /// Default classes for the modal wrapper
        /// </summary>
        /// <returns></returns>
        string ModalWrapperClasses();
    }
}
