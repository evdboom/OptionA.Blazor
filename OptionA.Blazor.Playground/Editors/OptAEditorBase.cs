using System.Globalization;
using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components;

namespace OptionA.Blazor.Playground;

/// <summary>
/// Shared functionality for playground parameter editors.
/// </summary>
public abstract class OptAEditorBase : OptAComponent
{
    /// <summary>
    /// Gets or sets the parameter descriptor that defines the editor behavior.
    /// </summary>
    [Parameter]
    public PlaygroundParameterDescriptor Descriptor { get; set; } = new();

    /// <summary>
    /// Gets or sets the current parameter value.
    /// </summary>
    [Parameter]
    public object? Value { get; set; }

    /// <summary>
    /// Gets or sets the callback that reports editor value changes.
    /// </summary>
    [Parameter]
    public EventCallback<object?> ValueChanged { get; set; }

    /// <summary>
    /// Gets the playground data provider used for editor styling and defaults.
    /// </summary>
    [Inject]
    protected IPlaygroundDataProvider DataProvider { get; set; } = null!;

    /// <summary>
    /// Gets the effective value type, unwrapping nullable types when needed.
    /// </summary>
    protected Type EffectiveValueType => Nullable.GetUnderlyingType(Descriptor.ValueType) ?? Descriptor.ValueType;

    /// <summary>
    /// Gets the current value as a boolean.
    /// </summary>
    protected bool CurrentBooleanValue => Value is bool boolValue && boolValue;

    /// <summary>
    /// Gets the current value as a string.
    /// </summary>
    protected string CurrentStringValue => Value?.ToString() ?? string.Empty;

    /// <summary>
    /// Builds the input attributes for an editor instance.
    /// </summary>
    protected Dictionary<string, object?> GetInputAttributes()
    {
        var result = GetAttributes();

        if (TryGetClasses(DataProvider.DefaultEditorInputClass, out var classes))
        {
            result["class"] = classes;
        }

        return result;
    }

    /// <summary>
    /// Formats a value for display based on the descriptor display format.
    /// </summary>
    protected string FormatDisplayValue(object? value)
    {
        if (string.IsNullOrWhiteSpace(Descriptor.DisplayFormat))
        {
            return Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty;
        }

        return string.Format(CultureInfo.InvariantCulture, Descriptor.DisplayFormat, value);
    }
}
