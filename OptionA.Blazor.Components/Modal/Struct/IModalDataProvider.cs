using System.Diagnostics.CodeAnalysis;

namespace OptionA.Blazor.Components;

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
    /// Default behavior for the Draggable property
    /// </summary>
    bool Draggable { get; }
    /// <summary>
    /// Default behavior for the DragMode property
    /// </summary>
    DragMode DragMode { get; }
    /// <summary>
    /// Class to add to outline incase of dragmode outline
    /// </summary>
    public string? OutlineClass { get; }
    /// <summary>
    /// true if the class for the given size it set
    /// </summary>
    /// <param name="size"></param>
    /// <param name="className"></param>
    /// <returns></returns>
    bool TryGetClassForSize(ModalSize size, [NotNullWhen(true)] out string? className);
}
