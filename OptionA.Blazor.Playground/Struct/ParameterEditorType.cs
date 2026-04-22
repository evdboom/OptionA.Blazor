namespace OptionA.Blazor.Playground;

/// <summary>
/// Defines the supported editor experiences for playground parameters.
/// </summary>
public enum ParameterEditorType
{
    /// <summary>
    /// A free-form text editor.
    /// </summary>
    Text,
    /// <summary>
    /// A numeric editor.
    /// </summary>
    Number,
    /// <summary>
    /// A boolean editor.
    /// </summary>
    Boolean,
    /// <summary>
    /// An enum editor.
    /// </summary>
    Enum,
    /// <summary>
    /// A selection editor backed by allowed values.
    /// </summary>
    Select,
    /// <summary>
    /// A color editor.
    /// </summary>
    Color,
    /// <summary>
    /// A content editor for richer markup or fragments.
    /// </summary>
    Content
}
