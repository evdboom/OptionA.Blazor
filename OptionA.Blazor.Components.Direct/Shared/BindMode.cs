namespace OptionA.Blazor.Components;

/// <summary>
/// Bind mode for the direct input components, to determine how the value is bound
/// </summary>
public enum BindMode
{
    /// <summary>
    /// Bind the value on input, this will update the value on every key press
    /// </summary>
    OnInput,
    /// <summary>
    /// Bind the value on change, this will update the value when the input loses focus, this is the default for the Microsoft.AspNetCore.Components.Forms input components/>
    /// </summary>
    OnChange
}
