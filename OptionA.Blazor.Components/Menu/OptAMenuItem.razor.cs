using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Menu item
    /// </summary>
    public partial class OptAMenuItem
    {
        [Inject]
        private IMenuDataProvider DataProvider { get; set; } = null!;
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
        /// Optional child content
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        /// <summary>
        /// Triggers when item is clicked
        /// </summary>
        [CascadingParameter(Name = "OnItemSelected")]
        public EventCallback OnItemSelected { get; set; }
        /// <summary>
        /// Triggers when item is clicked
        /// </summary>
        [CascadingParameter(Name = "OnGroupItemSelected")]
        public EventHandler? OnGroupItemSelected { get; set; }

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

        private async Task ItemSelected(MouseEventArgs args)
        {
            if (OnItemSelected.HasDelegate)
            {
                await OnItemSelected.InvokeAsync();
            }
            if (OnGroupItemSelected is not null)
            {
                OnGroupItemSelected.Invoke(this, EventArgs.Empty);
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

        private Dictionary<string, object?> GetItemAttributes()
        {
            var result = GetAttributes();
            if (!string.IsNullOrEmpty(Description))
            {
                result["title"] = Description;
            }
            if (TryGetClasses(DataProvider.MenuItemClass, out string classes))
            {
                result["class"] = classes;
            }
            if (Attributes is not null)
            {
                foreach(var attribute in Attributes) 
                {
                    result[attribute.Key] = attribute.Value;
                }
            }

            return result;
        }

        private Dictionary<string, object?> GetLinkAttributes()
        {
            var result = new Dictionary<string, object?>();
            if (!string.IsNullOrEmpty(Href))
            {
                result["href"] = Href;
            }

            var classes = DataProvider.LinkClass
                .Split(' ')
                .ToList();
            
            if (_isActive)
            {
                classes.AddRange(DataProvider.ActiveClass
                    .Split(' '));
            }

            var resultClasses = classes
                .Distinct()
                .Where(c => !string.IsNullOrEmpty(c))
                .ToList();

            if (resultClasses.Any())
            {
                result["class"] = string.Join(' ', resultClasses);
            }

            return result;
        }
    }
}
