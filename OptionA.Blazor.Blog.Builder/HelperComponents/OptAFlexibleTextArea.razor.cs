using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components;

namespace OptionA.Blazor.Blog.Builder.HelperComponents
{
    /// <summary>
    /// A text area with build in autogrow checkbox
    /// </summary>
    public partial class OptAFlexibleTextArea
    {
        /// <summary>
        /// Set required bindmode for the input
        /// </summary>
        [Parameter]
        public BindMode? Mode { get; set; }
        /// <summary>
        /// Set to false to hide the autogrow option (default is true)
        /// </summary>
        [Parameter]
        public bool? DisplayAutoGrow { get; set; }
        /// <summary>
        /// Set to true to enable autogrow initially
        /// </summary>
        [Parameter]
        public bool? InitialAutoGrow { get; set; }
        /// <summary>
        /// Text of the text area
        /// </summary>
        [Parameter]
        public string? Value { get; set; }
        /// <summary>
        /// Occurs when value changes
        /// </summary>
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }
        /// <summary>
        /// Attributes to set
        /// </summary>
        [Parameter]
        public IDictionary<string, object>? Attributes { get; set; }
        [Inject]
        private IBlogBuilderDataProvider DataProvider { get; set; } = null!;

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

        private bool _showAutoGrow = true;
        private bool _autoGrow;
        private string _id = string.Empty;

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            _id = $"OptATA-{Guid.NewGuid()}";
        }

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            if (InitialAutoGrow.HasValue)
            {
                _autoGrow = InitialAutoGrow.Value;
            }
            if (DisplayAutoGrow.HasValue)
            {
                _showAutoGrow = DisplayAutoGrow.Value;
            }
        }

        private Dictionary<string, object?> GetCheckBoxAttributes(string id)
        {
            var defaultAttributes = new Dictionary<string, object?>
            {
                ["id"] = id
            };

            return DataProvider.GetAttributes(BuilderType.CheckboxInput, defaultAttributes);
        }

        private Dictionary<string, object?> GetLabelAttributes(string id)
        {
            var defaultAttributes = new Dictionary<string, object?>
            {
                ["for"] = id
            };

            return DataProvider.GetAttributes(BuilderType.Label, defaultAttributes);
        }

        private Dictionary<string, object?> GetCheckboxContainerAttributes()
        {
            var defaultAttributes = new Dictionary<string, object?>
            {
                ["opta-checkbox-container"] = true,
            };
            return DataProvider.GetAttributes(BuilderType.TextAreaAutoGrowContainer, defaultAttributes);
        }

        private Dictionary<string, object?> GetFleixbleTextAreaAttributes()
        {
            var defaultAttributes = new Dictionary<string, object?>
            {
                ["opta-flexible-text-area"] = true,
            };
            return DataProvider.GetAttributes(BuilderType.FlexibleTextArea, defaultAttributes);
        }
        

    }
}