using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OptionA.Blazor.Components.Modal.Services;
using OptionA.Blazor.Components.Modal.Struct;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Modal implementation
    /// </summary>
    public partial class OptaModal : IDisposable
    {
        private const string RegisterHandlerFunction = "registerHandler";
        private const string UnregisterHandlerFunction = "unregisterHandler";
        private const string GetScrollPositionFunction = "getScrollPosition";

        /// <summary>
        /// Id for the modal
        /// </summary>
        [Parameter]
        public string? ModalId { get; set; }
        /// <summary>
        /// Fragment to show as title
        /// </summary>
        [Parameter]
        public RenderFragment? Title { get; set; }
        /// <summary>
        /// Fragment to show as body
        /// </summary>
        [Parameter]
        public RenderFragment? Body { get; set; }
        /// <summary>
        /// Fragment to show as footer
        /// </summary>
        [Parameter]
        public RenderFragment? Footer { get; set; }
        /// <summary>
        /// Called when the modal is closed (not close silent)
        /// </summary>
        [Parameter]
        public EventCallback OnClose { get; set; }
        /// <summary>
        /// Called when the modal is shown
        /// </summary>
        [Parameter]
        public EventCallback OnShow { get; set; }
        /// <summary>
        /// Set to true to enable dragging (like a window) of the modal, this will disable closing by clicking on the background
        /// Defaults to the value set in the <see cref="ModalOptions"/>
        /// </summary>
        [Parameter]
        public bool? Draggable { get; set; }
        /// <summary>
        /// Set to true if there are dropdowns in the modal (overflow will be hidden otherwise)
        /// </summary>
        [Parameter]
        public bool SupportDropdowns { get; set; }
        /// <summary>
        /// Set to false to hide the Close button
        /// </summary>
        [Parameter]
        public bool UserCanClose { get; set; } = true;
        /// <summary>
        /// Set to true to enable scrollbars in the modal
        /// </summary>
        [Parameter]
        public bool ScrollableDialog { get; set; } = true;
        /// <summary>
        /// Size for the modal
        /// </summary>
        [Parameter]
        public ModalSize Size { get; set; }

        [Inject]
        private IModalDataProvider DataProvider { get; set; } = null!;
        [Inject]
        private IModalService ModalService { get; set; } = null!;
        [Inject]
        private IJSRuntime JsRuntime { get; set; } = null!;

        private int _zindex;
        private bool _showDialog;
        private bool _dragging;
        private bool _backgroundDown;
        private bool _showOnMouse;
        private double _startX, _startY, _offsetX, _offsetY;
        private IJSObjectReference? _module;
        private string? _eventId;

        /// <summary>
        /// Called whenever scroll changes
        /// </summary>
        /// <param name="scrollPosition"></param>
        [JSInvokable]
        public void ScrollChanged(ScrollPosition scrollPosition)
        {
            var difX = (int)(scrollPosition.ScrollX - _startX);
            var difY = (int)(scrollPosition.ScrollY - _startY);

            if (difX != 0 || difY != 0) 
            {
                _offsetX -= difX;
                _offsetY -= difY;
                StateHasChanged();
            }

            _startX = scrollPosition.ScrollX;
            _startY = scrollPosition.ScrollY;
        }

        /// <summary>
        /// Show the modal
        /// </summary>
        /// <returns></returns>
        public async Task Show()
        {
            if (_showDialog)
            {
                return;
            }

            _zindex = ModalService.RegisterOpen(this);
            _showOnMouse = false;
            _offsetX = 0;
            _offsetY = 0;
            _showDialog = true;
            await OnShow.InvokeAsync();
            StateHasChanged();
        }

        /// <summary>
        /// Shows the modal at the position of the mouse
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task ShowOnMouse(MouseEventArgs args)
        {
            await Show();
            _showOnMouse = true;
            _offsetX = args.ClientX;
            _offsetY = args.ClientY;
            if (_module is null)
            {
                return;
            }
            var currentPosition = await _module.InvokeAsync<ScrollPosition>(GetScrollPositionFunction);
            _startX = currentPosition?.ScrollX ?? 0;
            _startY = currentPosition?.ScrollY ?? 0;
            var objRef = DotNetObjectReference.Create(this);
            _eventId = await _module.InvokeAsync<string>(RegisterHandlerFunction, objRef, nameof(ScrollChanged));
            
            StateHasChanged();
        }

        /// <summary>
        /// Closes the modal
        /// </summary>
        /// <returns></returns>
        public async Task Close()
        {
            if (!_showDialog)
            {
                return;
            }

            ModalService.RegisterClosed(this);
            await OnClose.InvokeAsync();
            _showDialog = false;
            if (!string.IsNullOrEmpty(_eventId) && _module is not null)
            {
                await _module.InvokeVoidAsync(UnregisterHandlerFunction, _eventId);
            }
            StateHasChanged();
        }

        /// <summary>
        /// Closes the modal without invoking onclose
        /// </summary>
        public void CloseSilent()
        {
            if (!_showDialog)
            {
                return;
            }

            ModalService.RegisterClosed(this); 
            _showDialog = false;
            if (!string.IsNullOrEmpty(_eventId) && _module is not null)
            {
                _module.InvokeVoidAsync(UnregisterHandlerFunction, _eventId);
            }            
        }

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            ModalService.ModalClicked += OnModalClicked;
            ModalService.ModalClosed += OnModalClosed;
        }

        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            _module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OptionA.Blazor.Components/Modal/OptaModal.razor.js");
        }

        private void OnModalClosed(object? sender, OptaModal e)
        {
            if (e != this && _showDialog)
            {
                var newIndex = ModalService.RegisterOpen(this);
                if (newIndex != _zindex)
                {
                    _zindex = newIndex;
                    StateHasChanged();
                }
            }
        }

        private void OnModalClicked(object? sender, OptaModal e)
        {
            if (e != this && _showDialog)
            {
                var newIndex = ModalService.RegisterOpen(this);
                if (newIndex != _zindex)
                {
                    _zindex = newIndex;
                    StateHasChanged();
                }
            }
        }

        private bool GetIsDraggable()
        {
            return Draggable ?? DataProvider.Draggable;
        }

        private void TitleMouseDown(MouseEventArgs args)
        {
            if (!GetIsDraggable())
            {
                return;
            }

            _startX = args.ClientX - _offsetX;
            _startY = args.ClientY - _offsetY;

            _dragging = true;
        }

        private void SelectModal(MouseEventArgs args)
        {
            _zindex = ModalService.OnModalClicked(this);
            StateHasChanged();
        }

        private async Task BackgroundUp(MouseEventArgs args)
        {
            _dragging = false;
            if (_backgroundDown && UserCanClose)
            {
                await Close();
            }
            _backgroundDown = false;
        }

        private void OnDrag(MouseEventArgs args)
        {
            if (!_dragging)
            {
                return;
            }

            _offsetX = args.ClientX - _startX;
            _offsetY = args.ClientY - _startY;

            StateHasChanged();
        }

        private void BackgroundDown(MouseEventArgs args)
        {
            _backgroundDown = !GetIsDraggable();
        }

        private bool GetShowBackDrop()
        {
            return !GetIsDraggable() && !_showOnMouse;
        }

        private Dictionary<string, object?> GetModalAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["tabindex"] = -1,
                ["role"] = "dialog",
                ["style"] = $"z-index:{_zindex};"
            };
            if (!string.IsNullOrEmpty(ModalId))
            {
                result["id"] = ModalId;
            }
            if (!string.IsNullOrEmpty(DataProvider.ModalClass))
            {
                result["class"] = DataProvider.ModalClass;
            }
            if (GetIsDraggable())
            {
                result["draggable-modal"] = true;
            }

            return result;
        }

        private Dictionary<string, object?> GetDialogAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["role"] = "document",
            };
            var classes = (DataProvider.DialogClass ?? string.Empty)
                .Split(' ')
                .ToList();

            if (ScrollableDialog && !string.IsNullOrEmpty(DataProvider.ScrollableDialogClass))
            {
                classes.Add(DataProvider.ScrollableDialogClass);
            }

            if (DataProvider.TryGetClassForSize(Size, out var sizeClass))
            {
                classes.Add(sizeClass);
            }

            if (classes.Any())
            {
                result["class"] = string.Join(' ', classes.Distinct());
            }

            if (GetIsDraggable())
            {
                result["draggable-dialog"] = true;
            }

            if (_showOnMouse)
            {
                result["on-mouse"] = true;
            }

            return result;
        }

        private Dictionary<string, object?> GetContentAttributes()
        {
            var result = new Dictionary<string, object?>();
            if (TryGetClasses(DataProvider.ContentClass ?? string.Empty, out string classes))
            {
                result["class"] = classes;
            }
            if (Attributes is not null)
            {
                foreach (var attribute in Attributes)
                {
                    result[attribute.Key] = attribute.Value;
                }
            }
            if ((int)_offsetX != 0 || (int)_offsetY != 0)
            {
                result["style"] = $"top:{(int)_offsetY}px;left:{(int)_offsetX}px;";
            }
            if (ScrollableDialog && string.IsNullOrEmpty(DataProvider.ScrollableDialogClass))
            {
                result["scrollable"] = true;
            }
            if (SupportDropdowns)
            {
                result["dropdown"] = true;
            }

            return result;
        }

        private Dictionary<string, object?> GetHeaderAttributes()
        {
            var result = new Dictionary<string, object?>();
            if (GetIsDraggable())
            {
                result["draggable"] = true;
            }
            if (!string.IsNullOrEmpty(DataProvider.HeaderClass))
            {
                result["class"] = DataProvider.HeaderClass;
            }

            return result;
        }

        private Dictionary<string, object?> GetTitleAttributes()
        {
            var result = new Dictionary<string, object?>();
            if (!string.IsNullOrEmpty(DataProvider.TitleClass))
            {
                result["class"] = DataProvider.TitleClass;
            }

            return result;
        }

        private Dictionary<string, object?> GetCloseButtonAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["type"] = "button"
            };
            if (!string.IsNullOrEmpty(DataProvider.CloseButtonClass))
            {
                result["class"] = DataProvider.CloseButtonClass;
            }

            return result;
        }

        private Dictionary<string, object?> GetBodyAttributes()
        {
            var result = new Dictionary<string, object?>();
            if (!string.IsNullOrEmpty(DataProvider.BodyClass))
            {
                result["class"] = DataProvider.BodyClass;
            }
            if (ScrollableDialog && string.IsNullOrEmpty(DataProvider.ScrollableDialogClass))
            {
                result["scrollable-body"] = true;
            }

            return result;
        }

        private Dictionary<string, object?> GetFooterAttributes()
        {
            var result = new Dictionary<string, object?>();
            if (!string.IsNullOrEmpty(DataProvider.FooterClass))
            {
                result["class"] = DataProvider.FooterClass;
            }

            return result;
        }

        private Dictionary<string, object?> GetBackdropAttributes()
        {
            var result = new Dictionary<string, object?>();
            if (!string.IsNullOrEmpty(DataProvider.BackdropClass))
            {
                result["class"] = DataProvider.BackdropClass;
            }

            return result;
        }

        private bool _disposed;
        /// <inheritdoc/>
        public void Dispose()
        {
            if (!_disposed)
            {
                ModalService.ModalClicked -= OnModalClicked;
                ModalService.ModalClosed -= OnModalClosed;
                _disposed = true;
                GC.SuppressFinalize(this);

                if (!string.IsNullOrEmpty(_eventId) && _module is not null)
                {
                    _module.InvokeVoidAsync(UnregisterHandlerFunction, _eventId);
                }
            }
        }
    }
}
