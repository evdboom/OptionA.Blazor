using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components.Direct.Input.Internal;

/// <summary>
/// Implementation of <see cref="Microsoft.AspNetCore.Components.Forms.InputText"/> with bind to oninput
/// </summary>
public partial class DirectInputText
{
    /// <summary>
    /// Bindmode for the input, default is <see cref="BindMode.OnInput"/>
    /// </summary>
    [Parameter]
    public BindMode? Mode { get; set; }

    private BindMode InternalMode => Mode ?? BindMode.OnInput;

    private Dictionary<string, object> Attributes
    {
        get
        {
            var result = AdditionalAttributes?.ToDictionary(d => d.Key, d => d.Value) ?? new();
            result["type"] = "text";
            return result;
        }
    }
}
