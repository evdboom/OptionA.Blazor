namespace OptionA.Blazor.Playground;

/// <summary>
/// Exposes read-only playground configuration to components.
/// </summary>
public interface IPlaygroundDataProvider
{
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
}
