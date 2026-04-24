namespace OptionA.Blazor.Playground;

/// <summary>
/// Describes the code editor implementation to use.
/// </summary>
public enum PlaygroundEditorKind
{
    /// <summary>
    /// Uses a standard textarea for editing code.
    /// </summary>
    TextArea,

    /// <summary>
    /// Uses the Monaco editor for editing code.
    /// </summary>
    Monaco
}