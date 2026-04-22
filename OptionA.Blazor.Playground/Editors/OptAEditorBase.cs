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

    [Inject]
    protected IPlaygroundDataProvider DataProvider { get; set; } = null!;

    protected Type EffectiveValueType => Nullable.GetUnderlyingType(Descriptor.ValueType) ?? Descriptor.ValueType;

    protected bool CurrentBooleanValue => Value is bool boolValue && boolValue;

    protected string CurrentStringValue => Value?.ToString() ?? string.Empty;

    protected Dictionary<string, object?> GetInputAttributes()
    {
        var result = GetAttributes();

        if (TryGetClasses(DataProvider.DefaultEditorInputClass, out var classes))
        {
            result["class"] = classes;
        }

        return result;
    }

    protected string FormatDisplayValue(object? value)
    {
        if (string.IsNullOrWhiteSpace(Descriptor.DisplayFormat))
        {
            return Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty;
        }

        return string.Format(CultureInfo.InvariantCulture, Descriptor.DisplayFormat, value);
    }
}
