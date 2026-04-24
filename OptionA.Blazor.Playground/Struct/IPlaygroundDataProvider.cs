namespace OptionA.Blazor.Playground;

/// <summary>
/// Exposes read-only playground configuration to components.
/// </summary>
public interface IPlaygroundDataProvider
{
    /// <summary>
    /// Gets the default CSS class for the interactive wrapper.
    /// </summary>
    string? DefaultInteractiveClass { get; }

    /// <summary>
    /// Gets the default CSS class for the top-level playground container.
    /// </summary>
    string? DefaultPlaygroundClass { get; }

    /// <summary>
    /// Gets the default CSS class for the preview panel.
    /// </summary>
    string? DefaultPreviewClass { get; }

    /// <summary>
    /// Gets the default CSS class for the editor panel.
    /// </summary>
    string? DefaultEditorClass { get; }

    /// <summary>
    /// Gets the default CSS class for the code panel.
    /// </summary>
    string? DefaultCodeClass { get; }

    /// <summary>
    /// Gets the default CSS class for parameter editor labels.
    /// </summary>
    string? DefaultEditorLabelClass { get; }

    /// <summary>
    /// Gets the default CSS class for parameter editor inputs.
    /// </summary>
    string? DefaultEditorInputClass { get; }

    /// <summary>
    /// Gets the default CSS class for parameter editor groups.
    /// </summary>
    string? DefaultEditorGroupClass { get; }

    /// <summary>
    /// Gets the default playground layout.
    /// </summary>
    PlaygroundLayout DefaultLayout { get; }

    /// <summary>
    /// Gets whether code editing is enabled.
    /// </summary>
    bool CodeEditingEnabled { get; }

    /// <summary>
    /// Gets the preferred editor implementation.
    /// </summary>
    PlaygroundEditorKind PreferredCodeEditor { get; }

    /// <summary>
    /// Gets the default code language label.
    /// </summary>
    string? DefaultCodeLanguage { get; }

    /// <summary>
    /// Gets the enabled export formats.
    /// </summary>
    IReadOnlyList<PlaygroundExportFormat> EnabledExportFormats { get; }
}
