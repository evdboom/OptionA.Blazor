using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Group of menu items (dropdown)
    /// </summary>
    public partial class OptAMenuGroup
    {
        private const string GetScrollHeigth = "getScrollHeight";

        private bool _open;
        private bool _isActive;
        private bool _closing;
        private bool _parametersChanged;
        private int _scrollHeight;
        private bool _openFromMouse;

        private ElementReference _dropDown;
        private Lazy<Task<IJSObjectReference>>? _moduleTask;

        [Inject]
        private IJSRuntime JsRuntime { get; set; } = null!;
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
        /// Currently set orientation on the menu
        /// </summary>
        [CascadingParameter(Name = "MenuOrientation")]
        public Orientation MenuOrientation { get; set; }

        /// <summary>
        /// Check for active
        /// <inheritdoc/>
        /// </summary>
        protected override void OnParametersSet()
        {
            _parametersChanged = true;
            if (!string.IsNullOrEmpty(ActiveRoute))
            {
                var location = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
                _isActive = $"/{location}".StartsWith(ActiveRoute, StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender || _parametersChanged)
            {
                _parametersChanged = false;
                var module = await _moduleTask!.Value;
                var newScrollHeight = await module.InvokeAsync<int>(GetScrollHeigth, _dropDown);

                if (newScrollHeight != _scrollHeight)
                {
                    _scrollHeight = newScrollHeight;
                    StateHasChanged();
                }                
            }
        }

        /// <summary>
        /// Check for active
        /// </summary>
        protected override void OnInitialized()
        {
            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
            _moduleTask = new(() => JsRuntime.InvokeAsync<IJSObjectReference>(
                "import",
                "./_content/OptionA.Blazor.Components/Menu/OptAMenuGroup.razor.js").AsTask());
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

        private void MouseEnter()
        {
            if (Provider.OpenGroupOnMouseOver())
            {
                _openFromMouse = true;
                _open = true;
                StateHasChanged();
            }
        }

        private void MouseLeave()
        {
            if (Provider.OpenGroupOnMouseOver())
            {
                if (Provider.GroupCloseTime() > 0)
                {
                    _closing = true;
                    var timer = new Timer(Elapsed, null, Provider.GroupCloseTime(), Timeout.Infinite);
                }

                _openFromMouse = false;
                _open = false;
                StateHasChanged();
            }
        }

        private void Elapsed(object? state)
        {
            _closing = false;
            StateHasChanged();
        }

        private void Toggle()
        {
            if (_open && Provider.GroupCloseTime() > 0)
            {
                _closing = true;
                var timer = new Timer(Elapsed, null, Provider.GroupCloseTime(), Timeout.Infinite);
            }

            _open = _openFromMouse || !_open;
            StateHasChanged();
        }

        private string GetClasses() => $"{Provider.GetMenuItemClass()} {AdditionalClasses}".Trim();
        private string GetLinkClasses() => $"{Provider.GetGroupClass()} {(_isActive ? Provider.GetActiveClass() : string.Empty)}".Trim();
    }
}
