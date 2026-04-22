using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Playground;

/// <summary>
/// Playground editor for free-form text values.
/// </summary>
public partial class OptAEditorText
{
    private Task HandleInput(ChangeEventArgs args)
    {
        return ValueChanged.InvokeAsync(args.Value?.ToString());
    }
}
