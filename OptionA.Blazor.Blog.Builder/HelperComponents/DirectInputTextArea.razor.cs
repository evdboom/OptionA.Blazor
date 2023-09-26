using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.HelperComponents
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
        [Inject]
        private IBlogBuilderDataProvider DataProvider { get; set; } = null!;

        private Dictionary<string, object> Attributes
        {
            get
            {
                var result = AdditionalAttributes?.ToDictionary(d => d.Key, d => d.Value) ?? new();
                if (!string.IsNullOrEmpty(CssClass))
                {
                    result["class"] = CssClass;
                }
                return result;
            }
        }

        private Dictionary<string, object?> GetContainerAttributes()
        {
            var result = new Dictionary<string, object?>()
            {
                ["auto-grow"] = true
            };

            if (DataProvider.TryGetProperties(BuilderType.TextAreaInput, out var properties))
            {
                if (properties.ContainerClass is not null)
                {
                    result["class"] = properties.ContainerClass;
                }
            }

            return result;
        }
    }
}
