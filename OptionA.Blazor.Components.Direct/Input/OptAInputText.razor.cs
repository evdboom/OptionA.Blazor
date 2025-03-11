using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components.Direct.Input.Internal;

namespace OptionA.Blazor.Components;

/// <summary>
/// Implementation of <see cref="Microsoft.AspNetCore.Components.Forms.InputText"/> but bound to oninput instead of onchange
/// </summary>
public partial class OptAInputText
{
    /// <summary>
    /// Gets the <see cref="ElementReference"/> for the input element
    /// </summary>
    public ElementReference Element => _input?.Element ?? throw new InvalidOperationException("Element is not available until after the component has been rendered");
    /// <summary>
    /// Bindmode for the input, default is <see cref="BindMode.OnInput"/>
    /// </summary>
    [Parameter]
    public BindMode? Mode { get; set; }
    /// <summary>
    /// Value to bind to
    /// </summary>
    [Parameter]
    public string? Value { get; set; }
    /// <summary>
    /// Occurs when value changes
    /// </summary>
    [Parameter]
    public EventCallback<string?> ValueChanged { get; set; }

    private string? InternalValue
    {
        get => Value;
        set
        {
            if (!string.Equals(Value, value))
            {
                Value = value;
                if (ValueChanged.HasDelegate)
                {
                    ValueChanged.InvokeAsync(Value);
                }
            }
        }
    }

    private DirectInputText? _input;

    private Dictionary<string, object?> GetAllAttributes()
    {
        var result = GetAttributes();
        result["opta-input-text"] = true;
        if (TryGetClasses(null, out var classes))
        {
            result["class"] = classes;
        }
        return result;
    }
}