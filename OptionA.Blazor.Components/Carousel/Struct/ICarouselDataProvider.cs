namespace OptionA.Blazor.Components.Carousel.Struct
{
    /// <summary>
    /// Interface for use in carousels to provide the correct classes and icons
    /// </summary>
    public interface ICarouselDataProvider
    {
        /// <summary>
        /// Text to show next to the autoplay checkbox
        /// </summary>
        /// <returns></returns>
        string GetAutoPlayText();
        /// <summary>
        /// Classes to apply to div containing autoplay and autoplaytext
        /// </summary>
        /// <returns></returns>
        string DefaultAutoPlayClasses();
        /// <summary>
        /// Default classes to apply to itemselect indicator list
        /// </summary>
        /// <returns></returns>
        string DefaultItemSelectClasses();
        /// <summary>
        /// Default classes to apply to selected indicator
        /// </summary>
        /// <returns></returns>
        string DefaultActiveItemSelectClasses();
        /// <summary>
        /// Default classes to apply to inactive indicators
        /// </summary>
        /// <returns></returns>
        string DefaultInactiveItemSelectClasses();
        /// <summary>
        /// Default classes to apply to next button
        /// </summary>
        /// <returns></returns>
        string DefaultNextClasses();
        /// <summary>
        /// Default classes to apply to next button
        /// </summary>
        /// <returns></returns>
        string DefaultNextIconClasses();
        /// <summary>
        /// Default classes to apply to previous button
        /// </summary>
        /// <returns></returns>
        string DefaultPreviousClasses();
        /// <summary>
        /// Default classes to apply to previous button
        /// </summary>
        /// <returns></returns>
        string DefaultPreviousIconClasses();
        /// <summary>
        /// Additional attributes to add to item select options
        /// </summary>
        /// <returns></returns>
        IDictionary<string, object?> AdditionalAttributesItemSelect();

    }
}
