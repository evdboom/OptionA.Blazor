using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Group of menu items (dropdown)
    /// </summary>
    public partial class OptAMenuGroup : IDisposable
    {
        private const string GetScrollHeigth = "getScrollHeight";

        private bool _open;
        private bool _isActive;
        private bool _closing;
        private bool _parametersChanged;
        private int _scrollHeight;
        private bool _openFromMouse;

        private Timer? _timer;
        private ElementReference _dropDown;
        private Lazy<Task<IJSObjectReference>>? _moduleTask;

        [Inject]
        private IJSRuntime JsRuntime { get; set; } = null!;
        [Inject]
        private IMenuDataProvider DataProvider { get; set; } = null!;
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;
        /// <summary>
        /// Menu items to display in this group
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
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
        /// Currently set orientation on the menu
        /// </summary>
        [CascadingParameter(Name = "MenuOrientation")]
        public Orientation MenuOrientation { get; set; }

        private event EventHandler? _childClicked;

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
                    await InvokeAsync(StateHasChanged);
                }                
            }
        }

        /// <summary>
        /// Check for active
        /// </summary>
        protected override void OnInitialized()
        {
            _timer = new(Elapsed, null, Timeout.Infinite, Timeout.Infinite);
            _childClicked += OnChildClicked;
            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
            _moduleTask = new(() => JsRuntime.InvokeAsync<IJSObjectReference>(
                "import",
                "./_content/OptionA.Blazor.Components/Menu/OptAMenuGroup.razor.js").AsTask());
        }

        private void OnChildClicked(object? sender, EventArgs e)
        {
            _openFromMouse = false;
        }

        private void NavigationManager_LocationChanged(object? sender, LocationChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ActiveRoute))
            {
                var location = NavigationManager.ToBaseRelativePath(e.Location);
                _isActive = $"/{location}".StartsWith(ActiveRoute, StringComparison.OrdinalIgnoreCase);
                InvokeAsync(StateHasChanged);
            }
        }

        private void MouseEnter()
        {
            if (DataProvider.OpenGroupOnMouseOver)
            {
                _openFromMouse = true;
                _open = true;
                InvokeAsync(StateHasChanged);
            }
        }

        private void MouseLeave()
        {
            if (DataProvider.OpenGroupOnMouseOver)
            {
                if (DataProvider.GroupCloseTime > 0)
                {
                    _closing = true;
                    _timer?.Change(DataProvider.GroupCloseTime, Timeout.Infinite);
                }

                _openFromMouse = false;
                _open = false;
                InvokeAsync(StateHasChanged);
            }
        }

        private void Elapsed(object? state)
        {
            _closing = false;
            InvokeAsync(StateHasChanged);
        }

        private void Toggle()
        {
            if (_open && DataProvider.GroupCloseTime > 0)
            {
                _closing = true;
                _timer?.Change(DataProvider.GroupCloseTime, Timeout.Infinite);
            }

            _open = _openFromMouse || !_open;
            InvokeAsync(StateHasChanged);
        }

        private Dictionary<string, object?> GetGroupAttributes()
        {
            var result = GetAttributes();
            result["opta-menu-group"] = true;
            
            if (TryGetClasses(DataProvider.MenuItemClass, out var classes))
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
            if (!string.IsNullOrEmpty(Description))
            {
                result["title"] = Description;
            }
            if (_open)
            {
                result["open"] = true;
            }
            if (_closing)
            {
                result["closing"] = true;
            }


            return result;
        }
        private Dictionary<string, object?> GetLinkAttributes()
        {
            var result = new Dictionary<string, object?>();

            var classes = DataProvider.GroupClass
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

        private Dictionary<string, object?> GetDropdownAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-dropdown"] = true,
                ["style"] = $"--opta-menu-dropdown-size:{_scrollHeight}px;"
            };
            if (MenuOrientation == Orientation.Vertical)
            {
                result["vertical"] = true;
            }

            if (!string.IsNullOrEmpty(DataProvider.DropdownClass))
            {
                result["class"] = DataProvider.DropdownClass;
            }

            return result;
        }

        private Dictionary<string, object?> GetBackgroundAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-menu-group-background"] = true,                
            };            

            return result;
        }

        private bool _disposed;
        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(!_disposed);
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing) 
            {
                _timer?.Dispose();
            }
        }
    }
}
