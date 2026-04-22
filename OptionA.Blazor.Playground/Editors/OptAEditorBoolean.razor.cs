using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Playground;

/// <summary>
/// Playground editor for boolean values.
/// </summary>
public partial class OptAEditorBoolean
{
    private Task HandleChange(ChangeEventArgs args)
    {
        return ValueChanged.InvokeAsync(args.Value is bool boolValue && boolValue);
    }
}
