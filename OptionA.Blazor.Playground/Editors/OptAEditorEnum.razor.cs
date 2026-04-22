using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Playground;

/// <summary>
/// Playground editor for enum values.
/// </summary>
public partial class OptAEditorEnum
{
    private string CurrentEnumValue => Value?.ToString() ?? string.Empty;

    private IEnumerable<string> GetEnumNames()
    {
        return Enum.GetNames(EffectiveValueType);
    }

    private Task HandleChange(ChangeEventArgs args)
    {
        var selectedValue = args.Value?.ToString();
        if (string.IsNullOrWhiteSpace(selectedValue))
        {
            return ValueChanged.InvokeAsync(null);
        }

        return ValueChanged.InvokeAsync(Enum.Parse(EffectiveValueType, selectedValue, ignoreCase: false));
    }
}
