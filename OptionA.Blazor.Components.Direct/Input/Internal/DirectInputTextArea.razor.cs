using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components.Direct.Input.Internal
{
    /// <summary>
    /// Implementation of <see cref="Microsoft.AspNetCore.Components.Forms.InputTextArea"/> with bind to oninput
    /// </summary>
    public partial class DirectInputTextArea
    {
        /// <summary>
        /// Set to true to enable autogrow
        /// </summary>
        [Parameter]
        public bool AutoGrow { get; set; }

        private Dictionary<string, object> Attributes
        {
            get
            {
                var result = AdditionalAttributes?.ToDictionary(d => d.Key, d => d.Value) ?? new();
                return result;
            }
        }

        private Dictionary<string, object?> GetContainerAttributes()
        {
            var result = new Dictionary<string, object?>()
            {
                ["auto-grow"] = true
            };

            return result;
        }
    }
}
