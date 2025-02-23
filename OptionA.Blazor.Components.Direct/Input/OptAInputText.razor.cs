using Microsoft.AspNetCore.Components;
using System;

namespace OptionA.Blazor.Components;

/// <summary>
/// Implementation of <see cref="Microsoft.AspNetCore.Components.Forms.InputText"/> but bound to oninput instead of onchange
/// </summary>
public partial class OptAInputText
{
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