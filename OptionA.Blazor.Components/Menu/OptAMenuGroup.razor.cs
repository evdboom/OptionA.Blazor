using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using OptionA.Blazor.Components.Menu.Struct;

namespace OptionA.Blazor.Components.Menu
{
    public partial class OptAMenuGroup
    {
        private bool _open;
        private bool _isActive;

        [Inject]
        private IMenuDataProvider Provider { get; set; } = null!;
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;
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
        /// Route to validate against wether or not this group is considered active
        /// </summary>
        [Parameter]
        public string? ActiveRoute { get; set; }
        /// <summary>
        /// Additonal classes to add
        /// </summary>
        [Parameter]
        public string? AdditionalClasses { get; set; }

        /// <summary>
        /// Check for active
        /// </summary>
        protected override void OnParametersSet()
        {            
            if (!string.IsNullOrEmpty(ActiveRoute))
            {
                var location = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
                _isActive = $"/{location}".StartsWith(ActiveRoute, StringComparison.OrdinalIgnoreCase);
            }                
        }

        /// <summary>
        /// Check for active
        /// </summary>
        protected override void OnInitialized()
        {
            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
        }

        private void NavigationManager_LocationChanged(object? sender, LocationChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ActiveRoute))
            {
                var location = NavigationManager.ToBaseRelativePath(e.Location);
                _isActive = $"/{location}".StartsWith(ActiveRoute, StringComparison.OrdinalIgnoreCase);
                StateHasChanged();
            }            
        }

        private void Toggle()
        {
            _open = !_open;
        }

        private string GetClasses()
        {
            return $"{Provider.GetMenuItemClass()} {AdditionalClasses}".Trim();
        }

        private string GetLinkClasses()
        {
            return $"{Provider.GetLinkClass()} {(_isActive ? Provider.GetActiveClass() : string.Empty)}".Trim();
        }
    }
}
