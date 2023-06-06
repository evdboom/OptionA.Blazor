using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components.Menu.Struct;
using OptionA.Blazor.Components.Shared.Enum;

namespace OptionA.Blazor.Components.Menu
{
    /// <summary>
    /// Component for placing a menu on the page
    /// </summary>
    public partial class OptAMenu
    {
        [Inject]
        private IMenuDataProvider Provider { get; set; } = null!;
        /// <summary>
        /// Menu items to display
        /// </summary>
        [Parameter]
        public RenderFragment? Items { get; set; }
        /// <summary>
        /// Additonal classes to add
        /// </summary>
        [Parameter]
        public string? AdditionalClasses { get; set; }
        /// <summary>
        /// Additonal classes to add to the usperceding nav container
        /// </summary>
        [Parameter]
        public string? AdditionalContainerClasses { get; set; }
        /// <summary>
        /// Orientation for menu, default is Horizontal
        /// </summary>
        [Parameter]
        public Orientation Orientation { get; set; } = Orientation.Horizontal;
        /// <summary>
        /// Called when a menu item is clicked
        /// </summary>
        [Parameter]
        public EventCallback OnItemSelected { get; set; }

        private string GetClasses() => $"{Provider.GetMenuClass()} {AdditionalClasses} {GetOrientationClass()}".Trim();
        private string GetContainerClasses() => $"{Provider.GetMenuContainerClass()} {AdditionalContainerClasses}".Trim();
        private string GetOrientationClass() => Orientation == Orientation.Horizontal
            ? "opta-menu-horizontal"
            : "opta-menu-vertical";
    }
}
