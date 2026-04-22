using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Playground;

/// <summary>
/// Playground editor for color values.
/// </summary>
public partial class OptAEditorColor
{
    private Task HandleInput(ChangeEventArgs args)
    {
        return ValueChanged.InvokeAsync(args.Value?.ToString());
    }
}
