using System.Diagnostics.CodeAnalysis;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Interface for use in modals to provide the correct classes and icons
    /// </summary>
    public interface IModalDataProvider
    {
        /// <summary>
        /// Class to add to the modaldialog
        /// </summary>
        string DialogClass { get; }
        /// <summary>
        /// Class to add to the content of the modal
        /// </summary>
        string? ContentClass { get; }
        /// <summary>
        /// Class to add to the section inside the dialog
        /// </summary>
        string? SectionClass { get; }
        /// <summary>
        /// Class to add to the header of the modal
        /// </summary>
        string? HeaderClass { get; }
        /// <summary>
        /// Class to add to the title of the modal
        /// </summary>
        string? TitleClass { get; }
        /// <summary>
        /// Class to add to the close button of the modal
        /// </summary>
        string? CloseButtonClass { get; }
        /// <summary>
        /// Content for the close button
        /// </summary>
        string? CloseButtonContent { get; }
        /// <summary>
        /// Class to add to the footer of the modal
        /// </summary>
        string? FooterClass { get; }
        /// <summary>
        /// Class to add to the backdrop of the modal
        /// </summary>
        string? BackdropClass { get; }
        /// <summary>
        /// Default behavior for the Draggable property
        /// </summary>
        bool Draggable { get; }
        /// <summary>
        /// Class to set if scrollable is set to true
        /// </summary>
        string? ScrollableDialogClass { get; }
        /// <summary>
        /// Default modal Z-index (needed to support multiple open modals)
        /// </summary>
        int ModalZIndex { get; }
        /// <summary>
        /// Steps for increasing the z-index when multiple modals are open, defaults to 1
        /// </summary>
        int ModalZIndexSteps { get; }
        /// <summary>
        /// true if the class for the given size it set
        /// </summary>
        /// <param name="size"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        bool TryGetClassForSize(ModalSize size, [NotNullWhen(true)] out string? className);
    }
}
