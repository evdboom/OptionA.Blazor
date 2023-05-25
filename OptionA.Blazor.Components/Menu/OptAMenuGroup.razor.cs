using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components.Menu.Struct;

namespace OptionA.Blazor.Components.Menu
{
    public partial class OptAMenuGroup
    {
        [Inject]
        private IMenuDataProvider Provider { get; set; } = null!;
        /// <summary>
        /// Menu items to display in this group
        /// </summary>
        [Parameter]
        public RenderFragment? Items { get; set; }
        /// <summary>
        /// Name of the group
        /// </summary>
        [Parameter]
        public string? Name { get; set; }
        /// <summary>
        /// Description of the group
        /// </summary>
        [Parameter]
        public string? Description { get; set; }
        /// <summary>
        /// Additonal classes to add
        /// </summary>
        [Parameter]
        public string? AdditionalClasses { get; set; }

        private bool _open;

        private void Toggle()
        {
            _open = !_open;
        }

        private string GetClasses()
        {
            return $"{Provider.GetGroupClass()} {AdditionalClasses}".Trim();
        }
    }
}
