using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using OptionA.Blazor.Components.Menu.Struct;

namespace OptionA.Blazor.Components.Menu
{
    /// <summary>
    /// Menu item
    /// </summary>
    public partial class OptAMenuItem
    {
        [Inject]
        private IMenuDataProvider Provider { get; set; } = null!;
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;

        private bool _isActive;

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
        /// <summary>
        /// Optional child content
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// Override to map locationchanged
        /// </summary>
        protected override void OnInitialized()
        {
            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
        }

        /// <summary>
        /// Override to map active
        /// </summary>
        protected override void OnParametersSet()
        {
            if (!string.IsNullOrEmpty(Href))
            {
                var location = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
                _isActive = $"/{location}".Equals(Href, StringComparison.OrdinalIgnoreCase);
            }
        }

        private void NavigationManager_LocationChanged(object? sender, LocationChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Href))
            {
                var location = NavigationManager.ToBaseRelativePath(e.Location);
                _isActive = $"/{location}".Equals(Href, StringComparison.OrdinalIgnoreCase);
                StateHasChanged();
            }
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
