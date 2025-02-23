using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components.Direct.Input.Internal;

/// <summary>
/// Input for number types with change on input
/// </summary>
public partial class DirectInputInteger
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
            result["type"] = "number";
            result["step"] = "any";
            return result;
        }
    }


}
