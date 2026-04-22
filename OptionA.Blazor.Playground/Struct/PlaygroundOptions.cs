namespace OptionA.Blazor.Playground;

/// <summary>
/// Configures the default layout and CSS classes used by playground components.
/// </summary>
public class PlaygroundOptions
{
    /// <summary>
    /// Gets or sets the default CSS class for the top-level playground container.
    /// </summary>
    public string? DefaultPlaygroundClass { get; set; }

    /// <summary>
    /// Gets or sets the default CSS class for the preview panel.
    /// </summary>
    public string? DefaultPreviewClass { get; set; }

    /// <summary>
    /// Gets or sets the default CSS class for the editor panel.
    /// </summary>
    public string? DefaultEditorClass { get; set; }

    /// <summary>
    /// Gets or sets the default CSS class for the code panel.
    /// </summary>
    public string? DefaultCodeClass { get; set; }

    /// <summary>
    /// Gets or sets the default CSS class for parameter editor labels.
    /// </summary>
    public string? DefaultEditorLabelClass { get; set; }

    /// <summary>
    /// Gets or sets the default CSS class for parameter editor inputs.
    /// </summary>
    public string? DefaultEditorInputClass { get; set; }

    /// <summary>
    /// Gets or sets the default CSS class for parameter editor groups.
    /// </summary>
    public string? DefaultEditorGroupClass { get; set; }

    /// <summary>
    /// Gets or sets the default layout for the playground.
    /// </summary>
    public PlaygroundLayout DefaultLayout { get; set; } = PlaygroundLayout.SideBySide;
}
