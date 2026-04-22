using System.Globalization;
using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Playground;

/// <summary>
/// Playground editor for explicit allowed-value selections.
/// </summary>
public partial class OptAEditorSelect
{
    private string CurrentSelectedIndex => GetCurrentSelectedIndex().ToString(CultureInfo.InvariantCulture);

    private int GetCurrentSelectedIndex()
    {
        for (var index = 0; index < Descriptor.AllowedValues.Count; index++)
        {
            if (Equals(Descriptor.AllowedValues[index], Value))
            {
                return index;
            }
        }

        return 0;
    }

    private Task HandleChange(ChangeEventArgs args)
    {
        if (!int.TryParse(args.Value?.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var index)
            || index < 0
            || index >= Descriptor.AllowedValues.Count)
        {
            return ValueChanged.InvokeAsync(null);
        }

        return ValueChanged.InvokeAsync(Descriptor.AllowedValues[index]);
    }
}
