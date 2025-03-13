using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace OptionA.Blazor.Components;

/// <summary>
/// Implementation of  <see cref="Microsoft.AspNetCore.Components.Forms.InputRadioGroup{TValue}"/> where TValue is an <see cref="Enum"/>
/// </summary>
public partial class OptAEnumRadioGroup<TEnum> where TEnum : struct, Enum
{
    /// <summary>
    /// Returns the reference to first radiobutton, null if there are no elements
    /// </summary>
    public ElementReference? Element => _input?.Element;
    /// <summary>
    /// Selected Value
    /// </summary>
    [Parameter]
    public TEnum? Value { get; set; }
    /// <summary>
    /// Occurs when the value is updated
    /// </summary>
    [Parameter]
    public EventCallback<TEnum?> ValueChanged { get; set; }
    /// <summary>
    /// Optional name mappings for display value
    /// </summary>
    [Parameter]
    public Dictionary<TEnum, string>? NameMappings { get; set; }
    /// <summary>
    /// Optional title mappings for title attribute of options
    /// </summary>
    [Parameter]
    public Dictionary<TEnum, string>? TitleMappings { get; set; }
    /// <summary>
    /// Set to change the order of the items in the list
    /// </summary>
    [Parameter]
    public EnumOrder OrderMode { get; set; }
    /// <summary>
    /// True to order descending
    /// </summary>
    [Parameter]
    public bool OrderDescending { get; set; }
    /// <summary>
    ///Orientation of the radio group, default is vertical
    /// </summary>
    [Parameter]
    public Orientation? Orientation { get; set; }

    private TEnum? InternalValue
    {
        get => Value;
        set
        {           
            if (!EqualValue(Value, value))
            {
                Value = value;
                if (ValueChanged.HasDelegate)
                {
                    ValueChanged.InvokeAsync(Value);
                }
            }
        }
    }

    private TEnum[]? _values;
    private IEnumerable<TEnum> OrderedItems
    {
        get
        {
            if (_values is null)
            {
                return [];
            }

            return OrderMode switch
            {
                EnumOrder.Name => OrderDescending
                    ? _values.OrderByDescending(value => $"{value}")
                    : _values.OrderBy(value => $"{value}"),
                EnumOrder.DisplayValue => OrderDescending
                    ? _values.OrderByDescending(GetDisplayName)
                    : _values.OrderBy(GetDisplayName),
                _ => OrderDescending
                    ? _values.OrderDescending()
                    : _values
            };
        }
    }

    private InputRadio<TEnum>? _input;

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        _values = Enum.GetValues<TEnum>();
    }

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        InternalValue = Value;
    }

    private bool EqualValue(TEnum? oldValue, TEnum? newValue)
    {
        if (oldValue.HasValue != newValue.HasValue)
        {
            return false;
        }
        if (!oldValue.HasValue && !newValue.HasValue)
        {
            return true;
        }
        else
        {
            return oldValue!.Value.Equals(newValue!.Value);
        }
    }

    private Dictionary<string, object?> GetAllAttributes()
    {
        var result = GetAttributes();
        result["opta-radio-group"] = true;
        if (TryGetClasses(null, out var classes))
        {
            result["class"] = classes;
        }
        if (Orientation == Components.Orientation.Horizontal)
        {
            result["horizontal"] = true;
        }
        return result;
    }

    private string? GetDisplayName(TEnum value)
    {
        if (NameMappings is not null && NameMappings.TryGetValue(value, out var name))
        {
            return name;
        }

        return $"{value}";
    }

    private Dictionary<string, object?> GetOptionLabelAttributes(TEnum value)
    {
        var result = new Dictionary<string, object?>();

        if (TitleMappings is not null && TitleMappings.TryGetValue(value, out var title))
        {
            result["title"] = title;
        }

        return result;
    }
}