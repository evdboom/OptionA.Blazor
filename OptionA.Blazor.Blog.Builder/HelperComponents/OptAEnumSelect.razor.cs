using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.HelperComponents
{
    /// <summary>
    /// Component to select an enum
    /// </summary>
    public partial class OptAEnumSelect<TEnum> where TEnum : struct, Enum
    {
        /// <summary>
        /// Selected Value
        /// </summary>
        [Parameter]
        public TEnum Value { get; set; }        
        /// <summary>
        /// Occurs when the value is updated
        /// </summary>
        [Parameter]
        public EventCallback<TEnum> ValueChanged { get; set; }
        /// <summary>
        /// Additional attributes to add to the class
        /// </summary>
        [Parameter]
        public Dictionary<string, object?>? AdditionalAttributes { get; set; }
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

        private TEnum InternalValue
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

        private TEnum[]? _values;
        private IEnumerable<TEnum> OrderedItems
        {
            get
            {
                if (_values is null)
                {
                    return Enumerable.Empty<TEnum>();
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

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            _values = Enum.GetValues<TEnum>();
        }

        private string? GetDisplayName(TEnum value) 
        {
            if (NameMappings is not null && NameMappings.TryGetValue(value, out var name))
            {
                return name;
            }

            return $"{value}";
        }

        private Dictionary<string, object?> GetOptionAttributes(TEnum value) 
        {
            var result = new Dictionary<string, object?>
            {
                ["value"] = value
            };

            if (TitleMappings is not null && TitleMappings.TryGetValue(value, out var title))
            {
                result["title"] = title;
            }

            return result;
        }
    }
}
