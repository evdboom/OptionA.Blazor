using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components.Menu.Struct;

namespace OptionA.Blazor.Components.Menu
{
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

        private string GetClasses()
        {
            return $"{Provider.GetMenuClass()} {AdditionalClasses}".Trim();
        }
    }
}
