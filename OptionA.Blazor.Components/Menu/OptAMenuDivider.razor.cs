using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Divider to use inside a menu group
    /// </summary>
    public partial class OptAMenuDivider
    {
        [Inject]
        private IMenuDataProvider Provider { get; set; } = null!;
        /// <summary>
        /// Additonal classes to add
        /// </summary>
        [Parameter]
        public string? AdditionalClasses { get; set; }

        private string GetClasses()
        {
            return $"{Provider.GetDividerClass()} {AdditionalClasses}".Trim();
        }
    }
}
