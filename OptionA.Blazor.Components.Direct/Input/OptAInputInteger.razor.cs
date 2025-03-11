using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components.Direct.Input.Internal;

namespace OptionA.Blazor.Components;

/// <summary>
/// Implementation of <see cref="Microsoft.AspNetCore.Components.Forms.InputNumber{TValue}"/> but bound to oninput instead of onchange
/// </summary>
public partial class OptAInputInteger
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
    public int Value { get; set; }
    /// <summary>
    /// Occurs when value changes
    /// </summary>
    [Parameter]
    public EventCallback<int> ValueChanged { get; set; }

    private int InternalValue
    {
        get => Value;
        set
        {
            if (!Value.Equals(value))
            {
                Value = value;
                if (ValueChanged.HasDelegate)
                {
                    ValueChanged.InvokeAsync(Value);
                }
            }
        }
    }

    private DirectInputInteger? _input;

    private Dictionary<string, object?> GetAllAttributes()
    {
        var result = GetAttributes();
        result["opta-input-integer"] = true;
        if (TryGetClasses(null, out var classes))
        {
            result["class"] = classes;
        }
        return result;
    }
}