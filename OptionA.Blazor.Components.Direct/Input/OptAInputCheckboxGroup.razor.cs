using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components.Direct.Input.Internal;

namespace OptionA.Blazor.Components;

/// <summary>
/// Implementation of a group of <see cref="Microsoft.AspNetCore.Components.Forms.InputCheckbox"/> effectively a creating a multiselect <see cref="Microsoft.AspNetCore.Components.Forms.InputRadioGroup{TValue}"/>
/// </summary>
/// <typeparam name="TValue"></typeparam>
public partial class OptAInputCheckboxGroup<TValue>
{
    /// <summary>
    /// Returns the reference to first checkbox, null if there are no elements
    /// </summary>
    public ElementReference? Element => _input?.Element;
    /// <summary>
    /// Selected Values
    /// </summary>
    [Parameter]
    public IEnumerable<TValue>? Value { get; set; }
    /// <summary>
    /// Occurs when the value is updated
    /// </summary>
    [Parameter]
    public EventCallback<IEnumerable<TValue>> ValueChanged { get; set; }
    /// <summary>
    /// Optional name mappings for display value
    /// </summary>
    [Parameter]
    public Func<TValue, string?>? DisplayValue { get; set; }
    /// <summary>
    /// Optional title mappings for title attribute of options
    /// </summary>
    [Parameter]
    public Func<TValue, string?>? TitleValue { get; set; }
    /// <summary>
    /// True to order descending
    /// </summary>
    [Parameter]
    public bool OrderDescending { get; set; }
    /// <summary>
    /// Optional comparer to order the items
    /// </summary>
    [Parameter]
    public IComparer<TValue>? OrderComparer { get; set; }
    /// <summary>
    /// Values to display
    /// </summary>
    [Parameter]
    public IEnumerable<TValue>? Items { get; set; }
    /// <summary>
    ///Orientation of the input group, default is vertical
    /// </summary>
    [Parameter]
    public Orientation? Orientation { get; set; }

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        _items = Items?
            .Select((item, index) => (item, index))
            .ToDictionary(i => i.index, i => i.item);

        if (_items is not null)
        {
            _selectedItems = Value?
            .Select(value => _items
                .FirstOrDefault(i => i.Value?.Equals(value) ?? false)
                .Key)
            .ToHashSet();
        }                     
    }

    private HashSet<int>? _selectedItems;
    private Dictionary<int, TValue>? _items;
    private OptAInputCheckbox? _input;

    private IEnumerable<(int Index, TValue Value)> OrderedItems
    {
        get
        {
            if (_items is null)
            {
                return [];
            }

            if (OrderComparer is not null)
            {
                return OrderDescending
                    ? _items
                        .OrderByDescending(item => item.Value, OrderComparer)
                        .Select(item => (item.Key, item.Value))
                    : _items
                        .OrderBy(item => item.Value, OrderComparer)
                        .Select(item => (item.Key, item.Value));
            }
            else
            {
                return OrderDescending
                    ? _items
                        .OrderByDescending(item => item.Key)
                        .Select(item => (item.Key, item.Value))
                    : _items
                        .OrderBy(item => item.Key)
                        .Select(item => (item.Key, item.Value));
            }
        }
    }

    private void OnSelectedChanged(int index, bool selected)
    {
        if (_items is null)
        {
            return;
        }

        _selectedItems ??= [];

        if (selected)
        {
            _selectedItems.Add(index);
        }
        else
        {
            _selectedItems.Remove(index);
        }

        Value = _selectedItems
            .Order()
            .Select(index => _items[index])
            .ToList();

        if (ValueChanged.HasDelegate)
        {
            ValueChanged.InvokeAsync(Value);
        }
    }

    private Dictionary<string, object?> GetAllAttributes()
    {
        var result = GetAttributes();
        result["opta-checkbox-group"] = true;
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

    private string? GetDisplayName(TValue value)
    {
        if (DisplayValue is not null)
        {
            return DisplayValue(value);
        }

        return $"{value}";
    }

    private Dictionary<string, object?> GetCheckboxAttributes(TValue value)
    {
        var result = new Dictionary<string, object?>();

        if (TitleValue is not null)
        {
            result["title"] = TitleValue(value);
        }

        return result;
    }

    private Dictionary<string, object?> GetSetAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["opta-field-set"] = true
        };
        return result;
    }
}
