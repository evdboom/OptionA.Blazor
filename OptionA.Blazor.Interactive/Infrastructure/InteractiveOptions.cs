using OptionA.Blazor.Interactive.Editors;
using OptionA.Blazor.Interactive.Exporters;
using OptionA.Blazor.Playground;

namespace OptionA.Blazor.Interactive.Infrastructure;

/// <summary>
/// Configures the interactive surface defaults.
/// </summary>
public class InteractiveOptions : PlaygroundOptions
{
    /// <summary>
    /// Gets or sets the default CSS class for the interactive wrapper.
    /// </summary>
    public string? DefaultInteractiveClass { get; set; }

    /// <summary>
    /// Gets or sets whether code editing is enabled.
    /// </summary>
    public bool CodeEditingEnabled { get; set; } = true;

    /// <summary>
    /// Gets or sets the preferred editor implementation.
    /// </summary>
    public InteractiveEditorKind PreferredCodeEditor { get; set; } = InteractiveEditorKind.TextArea;

    /// <summary>
    /// Gets or sets the default code language label.
    /// </summary>
    public string? DefaultCodeLanguage { get; set; } = "razor";

    /// <summary>
    /// Gets or sets the export formats made available by default.
    /// </summary>
    public List<InteractiveExportFormat> EnabledExportFormats { get; set; } =
        [InteractiveExportFormat.Razor, InteractiveExportFormat.Json, InteractiveExportFormat.Markdown, InteractiveExportFormat.Html];
}
