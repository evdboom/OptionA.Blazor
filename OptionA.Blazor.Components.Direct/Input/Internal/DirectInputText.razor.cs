namespace OptionA.Blazor.Components.Direct.Input.Internal;

/// <summary>
/// Implementation of <see cref="Microsoft.AspNetCore.Components.Forms.InputText"/> with bind to oninput
/// </summary>
public partial class DirectInputText
{
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
