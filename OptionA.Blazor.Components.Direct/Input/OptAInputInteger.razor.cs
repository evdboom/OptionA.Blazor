using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Implementation of <see cref="Microsoft.AspNetCore.Components.Forms.InputNumber{TValue}"/> but bound to oninput instead of onchange
    /// </summary>
    public partial class OptAInputInteger
    {
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
}