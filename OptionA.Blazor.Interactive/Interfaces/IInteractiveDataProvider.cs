using OptionA.Blazor.Interactive.Editors;
using OptionA.Blazor.Interactive.Exporters;
using OptionA.Blazor.Playground;

namespace OptionA.Blazor.Interactive.Interfaces;

/// <summary>
/// Exposes interactive documentation defaults to the component surface.
/// </summary>
public interface IInteractiveDataProvider : IPlaygroundDataProvider
{
    /// <summary>
    /// Gets the default CSS class for the interactive wrapper.
    /// </summary>
    string? DefaultInteractiveClass { get; }

    /// <summary>
    /// Gets whether code editing is enabled.
    /// </summary>
    bool CodeEditingEnabled { get; }

    /// <summary>
    /// Gets the preferred editor implementation.
    /// </summary>
    InteractiveEditorKind PreferredCodeEditor { get; }

    /// <summary>
    /// Gets the default code language label.
    /// </summary>
    string? DefaultCodeLanguage { get; }

    /// <summary>
    /// Gets the enabled export formats.
    /// </summary>
    IReadOnlyList<InteractiveExportFormat> EnabledExportFormats { get; }
}
