using System.Globalization;
using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Playground;

/// <summary>
/// Playground editor for numeric values.
/// </summary>
public partial class OptAEditorNumber
{
    private string CurrentNumberValue => Value switch
    {
        IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture),
        _ => CurrentStringValue
    };

    private Task HandleInput(ChangeEventArgs args)
    {
        var rawValue = args.Value?.ToString();
        if (string.IsNullOrWhiteSpace(rawValue))
        {
            return ValueChanged.InvokeAsync(null);
        }

        object parsedValue = EffectiveValueType == typeof(int)
            ? int.Parse(rawValue, CultureInfo.InvariantCulture)
            : EffectiveValueType == typeof(double)
                ? double.Parse(rawValue, CultureInfo.InvariantCulture)
                : Convert.ChangeType(rawValue, EffectiveValueType, CultureInfo.InvariantCulture);

        return ValueChanged.InvokeAsync(parsedValue);
    }
}
