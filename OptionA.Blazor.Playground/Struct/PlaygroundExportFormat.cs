namespace OptionA.Blazor.Playground;

/// <summary>
/// Describes the export formats supported by the playground surface.
/// </summary>
public enum PlaygroundExportFormat
{
    /// <summary>
    /// Exports component code as Razor markup.
    /// </summary>
    Razor,

    /// <summary>
    /// Exports component metadata as JSON.
    /// </summary>
    Json,

    /// <summary>
    /// Exports component documentation as Markdown.
    /// </summary>
    Markdown,

    /// <summary>
    /// Exports rendered output as HTML.
    /// </summary>
    Html
}