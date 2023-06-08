namespace OptionA.Blazor.Components.Carousel.Struct
{
    /// <summary>
    /// Options for Carousel data provider
    /// </summary>
    public class CarouselOptions
    {
        /// <summary>
        /// Text for autoplay
        /// </summary>
        public string? AutoPlayText { get; set; }
        /// <summary>
        /// Classes for autoplay
        /// </summary>
        public string? AutoPlayClasses { get; set; }
        /// <summary>
        /// Classes for item select group div
        /// </summary>
        public string? ItemSelectClasses { get; set; }
        /// <summary>
        /// Additional attributes to add to item select options
        /// </summary>
        public Dictionary<string, object?>? ItemSelectAttributes { get; set; }
        /// <summary>
        /// Classes for active item select
        /// </summary>
        public string? ActiveItemSelectClasses { get; set; }
        /// <summary>
        /// Classes for inactive item select
        /// </summary>
        public string? InactiveItemSelectClasses { get; set; }
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
    }
}
