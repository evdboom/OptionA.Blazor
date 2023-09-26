using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.HelperComponents
{
    /// <summary>
    /// Component to select an enum
    /// </summary>
    public partial class OptaEnumSelect<TEnum> where TEnum : struct, Enum
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

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            _values = Enum.GetValues<TEnum>();
        }
    }
}
