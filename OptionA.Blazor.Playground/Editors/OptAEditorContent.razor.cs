using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Playground;

/// <summary>
/// Playground editor for content values represented as text.
/// </summary>
public partial class OptAEditorContent
{
    private Task HandleInput(ChangeEventArgs args)
    {
        return ValueChanged.InvokeAsync(args.Value?.ToString());
    }
}
