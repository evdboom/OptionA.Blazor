using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components.Menu.Struct;

namespace OptionA.Blazor.Components.Menu
{
    public partial class OptAMenuItem
    {
        [Inject]
        private IMenuDataProvider Provider { get; set; } = null!;
        /// <summary>
        /// Name of the menu item to display
        /// </summary>
        [Parameter]
        public string? Name { get; set; }
        /// <summary>
        /// Longer description to show as tooltop
        /// </summary>
        [Parameter]
        public string? Description { get; set; }
        /// <summary>
        /// Location for the MenuItem
        /// </summary>
        [Parameter]
        public string? Href { get; set; }
        /// <summary>
        /// Additonal classes to add
        /// </summary>
        [Parameter]
        public string? AdditionalClasses { get; set; }

        private string GetClasses()
        {
            return $"{Provider.GetMenuItemClass()} {AdditionalClasses}".Trim();
        }
    }
}
