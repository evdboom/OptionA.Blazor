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
        /// Class to add to the section inside the dialog
        /// </summary>
        public string? SectionClass { get; set; }
        /// <summary>
        /// Class to add to the content of the modal
        /// </summary>
        public string? ContentClass { get; set; }
        /// <summary>
        /// Class to add to the header of the modal
        /// </summary>
        public string? HeaderClass { get; set; }
        /// <summary>
        /// Class to add to the close button of the modal
        /// </summary>
        public string? CloseButtonClass { get; set; }
        /// <summary>
        /// Content for the close button
        /// </summary>
        public string? CloseButtonContent { get; set; }
        /// <summary>
        /// Class to add to the footer of the modal
        /// </summary>
        public string? FooterClass { get; set; }
        /// <summary>
        /// Default behavior for the Draggable property
        /// </summary>
        public bool DefaultDraggable { get; set; }
        /// <summary>
        /// Default behavior for when dragging, direct or outline
        /// </summary>
        public DragMode DefaultDragMode { get; set; }
        /// <summary>
        /// Class to add to outline incase of dragmode outline
        /// </summary>
        public string? OutlineClass { get; set; }
        /// <summary>
        /// Add to set size classes for the modal
        /// </summary>
        public Dictionary<ModalSize, string>? SizeClasses { get; set; }
    }
}
