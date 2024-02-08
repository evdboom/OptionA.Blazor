using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Implementation of <see cref="Microsoft.AspNetCore.Components.Forms.InputTextArea"/> but bound to oninput instead of onchange
    /// </summary>
    public partial class OptAInputTextArea
    {
        /// <summary>
        /// Value to bind to
        /// </summary>
        [Parameter]
        public string? Value { get; set; }
        /// <summary>
        /// Occurs when the value changes
        /// </summary>
        [Parameter]
        public EventCallback<string?> ValueChanged { get; set; }
        /// <summary>
        /// Set to true to enable autogrow
        /// </summary>
        [Parameter]
        public bool AutoGrow { get; set; }

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
            result["opta-input-textarea"] = true;
            if (TryGetClasses(null, out var classes))
            {
                result["class"] = classes;
            }
            return result;
        }
    }
}