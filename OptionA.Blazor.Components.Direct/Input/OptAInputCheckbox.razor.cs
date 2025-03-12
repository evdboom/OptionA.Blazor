using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace OptionA.Blazor.Components;

/// <summary>
/// Implementation of  <see cref="Microsoft.AspNetCore.Components.Forms.InputCheckbox"/>
/// </summary>
public partial class OptAInputCheckbox
{
    /// <summary>
    /// Gets or sets the <see cref="ElementReference"/> for the input element
    /// </summary>
    public ElementReference Element => _input?.Element ?? throw new InvalidOperationException("Element is not available until after the component has been rendered");
    /// <summary>
    /// Display value    
    /// </summary>
    [Parameter]
    public string? Description { get; set; }
    /// <summary>
    /// Value for the input, bound to the selected of the checkbox
    /// </summary>
    [Parameter]
    public bool Value { get; set; }
    /// <summary>
    /// Occurs when the value is updated
    /// </summary>
    [Parameter]
    public EventCallback<bool> ValueChanged { get; set; }

    private bool InternalValue
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

    private InputCheckbox? _input;

    private Dictionary<string, object?> GetAllAttributes()
    {            
        var result = GetAttributes();
        result["opta-input-checkbox"] = true;
        if (TryGetClasses(null, out var classes))
        {
            result["class"] = classes;
        }
        return result;
    }

    private Dictionary<string, object?> GetLabelAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["opta-checkbox-label"] = true
        };

        var baseAttributes = GetAttributes();
        if (baseAttributes.TryGetValue("title", out var title))
        {
            result["title"] = title;
        }

        return result;
    }
}