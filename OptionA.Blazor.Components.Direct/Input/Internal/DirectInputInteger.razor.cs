namespace OptionA.Blazor.Components.Direct.Input.Internal;

/// <summary>
/// Input for number types with change on input
/// </summary>
public partial class DirectInputInteger
{
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
