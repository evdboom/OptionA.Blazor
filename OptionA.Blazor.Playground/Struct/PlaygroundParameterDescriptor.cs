namespace OptionA.Blazor.Playground;

/// <summary>
/// Describes a configurable parameter that can be edited in a playground surface.
/// </summary>
public class PlaygroundParameterDescriptor
{
    /// <summary>
    /// Gets or sets the parameter name on the target component.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the display name shown in the editor UI.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the descriptive text for the parameter.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the editor type used to modify the parameter.
    /// </summary>
    public ParameterEditorType EditorType { get; set; } = ParameterEditorType.Text;

    /// <summary>
    /// Gets or sets the CLR type of the parameter value.
    /// </summary>
    public Type ValueType { get; set; } = typeof(string);

    /// <summary>
    /// Gets or sets the default value for the parameter.
    /// </summary>
    public object? DefaultValue { get; set; }

    /// <summary>
    /// Gets or sets the allowed values for select-like editors.
    /// </summary>
    public IList<object?> AllowedValues { get; set; } = [];

    /// <summary>
    /// Gets or sets the optional display format used to render values.
    /// </summary>
    public string? DisplayFormat { get; set; }

    /// <summary>
    /// Gets or sets the logical group name used to organize editors.
    /// </summary>
    public string? Group { get; set; }

    /// <summary>
    /// Gets or sets the sort order within a group.
    /// </summary>
    public int Order { get; set; }
}
