namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Options for Modal data provider
    /// </summary>
    public class ModalOptions
    {
        /// <summary>
        /// Class to add to the modal
        /// </summary>
        public string? ModalClass { get; set; }
        /// <summary>
        /// Class to add to the dialog of the modal
        /// </summary>
        public string? DialogClass { get; set; }
        /// <summary>
        /// Class to add to the content of the modal
        /// </summary>
        public string? ContentClass { get; set; }
        /// <summary>
        /// Class to add to the header of the modal
        /// </summary>
        public string? HeaderClass { get; set; }
        /// <summary>
        /// Class to add to the title of the modal
        /// </summary>
        public string? TitleClass { get; set; }
        /// <summary>
        /// Class to add to the close button of the modal
        /// </summary>
        public string? CloseButtonClass { get; set; }
        /// <summary>
        /// Content for the close button
        /// </summary>
        public string? CloseButtonContent { get; set; }
        /// <summary>
        /// Class to add to the body of the modal
        /// </summary>
        public string? BodyClass { get; set; }
        /// <summary>
        /// Class to add to the footer of the modal
        /// </summary>
        public string? FooterClass { get; set; }
        /// <summary>
        /// Class to add to the backdrop of the modal
        /// </summary>
        public string? BackdropClass { get; set; }
        /// <summary>
        /// Default behavior for the Draggable property
        /// </summary>
        public bool Draggable { get; set; }
        /// <summary>
        /// Class to set if scrollable is set to true
        /// </summary>
        public string? ScrollableDialogClass { get; set; }
        /// <summary>
        /// Add to set size classes for the modal
        /// </summary>
        public Dictionary<ModalSize, string>? SizeClasses { get; set; }
        /// <summary>
        /// Default modal Z-index defaults to 1000
        /// </summary>
        public int? ModalZIndex { get; set; }
        /// <summary>
        /// Steps for increasing the z-index when multiple modals are open, defaults to 1
        /// </summary>
        public int? ModalZIndexSteps { get; set; }
    }
}
