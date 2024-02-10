using Microsoft.AspNetCore.Components;
using System.Linq;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Component to select an enum
    /// </summary>
    public partial class OptASelect<TValue>
    {
        /// <summary>
        /// Selected Value
        /// </summary>
        [Parameter]
        public TValue? Value { get; set; }
        /// <summary>
        /// Occurs when the value is updated
        /// </summary>
        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }
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

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            _items = Items?
                .Select((item, index) => (item, index))
                .ToDictionary(i => i.index, i => i.item);

            if (Value is not null)
            {
                InternalValue = _items
                    ?.FirstOrDefault(i => i.Value?.Equals(Value) ?? false)
                    .Key;
            }
            else
            {
                InternalValue = 0;
            }
        }

        private Dictionary<int,  TValue>? _items;

        private int? _internalValue;
        private int? InternalValue
        {
            get => _internalValue;
            set
            {
                if (_items is null)
                {
                    return;
                }

                if (_internalValue != value)
                {
                    _internalValue = value;
                    if (value.HasValue)
                    {
                        Value = _items.TryGetValue(value.Value, out var item)
                            ? item
                            : default;                       
                    }
                    else
                    {
                        Value = default;
                    }
                    ValueChanged.InvokeAsync(Value);
                }
            }
        }

        private IEnumerable<(int Index, TValue Value)> OrderedItems
        {
            get
            {
                if (_items is null)
                {
                    return Enumerable.Empty<(int Index, TValue Value)>();
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

        private Dictionary<string, object?> GetAllAttributes()
        {
            var result = GetAttributes();
            result["opta-select"] = true;
            if (TryGetClasses(null, out var classes))
            {
                result["class"] = classes;
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

        private Dictionary<string, object?> GetOptionAttributes(int index, TValue value)
        {
            var result = new Dictionary<string, object?>
            {
                ["value"] = index
            };

            if (TitleValue is not null)
            {
                result["title"] = TitleValue(value);
            }

            return result;
        }
    }
}
