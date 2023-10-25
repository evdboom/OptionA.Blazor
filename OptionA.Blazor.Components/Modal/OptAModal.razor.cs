using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OptionA.Blazor.Components.Shared;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Modal implementation
    /// </summary>
    public partial class OptAModal
    {
        private const string CloseDialogFunction = "closeDialog";
        private const string ShowDialogFunction = "showDialog";
        private const string ListCloseFunction = "listenClose";
        private const string GetBoundsFunction = "getBoundingRect";

        /// <summary>
        /// Id for the modal
        /// </summary>
        [Parameter]
        public string? ModalId { get; set; }
        /// <summary>
        /// Fragment to show as title
        /// </summary>
        [Parameter]
        public RenderFragment? Header { get; set; }
        /// <summary>
        /// Fragment to show as body
        /// </summary>
        [Parameter]
        public RenderFragment? Content { get; set; }
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
        /// Size for the modal
        /// </summary>
        [Parameter]
        public ModalSize Size { get; set; }

        [Inject]
        private IModalDataProvider DataProvider { get; set; } = null!;
        [Inject]
        private IJSRuntime JsRuntime { get; set; } = null!;

        private bool _showDialog;
        private bool _awaitShow, _awaitClose;
        private bool _dragging;
        private double? _startX, _startY, _startMouseX, _startMouseY, _offsetX, _offsetY;
        private IJSObjectReference? _module;

        private ElementReference _dialog;

        /// <summary>
        /// Show the modal
        /// </summary>
        /// <returns></returns>
        public void Show()
        {
            if (_showDialog)
            {
                return;
            }
            _showDialog = true;
            _awaitShow = true;                        
        }

        /// <summary>
        /// Closes the modal
        /// </summary>
        /// <returns></returns>
        public void Close()
        {
            if (!_showDialog)
            {
                return;
            }
            _awaitClose = true;            
        }

        /// <summary>
        /// Invoked by JS when the dialog is closed
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public async Task OnDialogClose()
        {
            _showDialog = false;
            _offsetX = null;
            _offsetY = null;
            await OnClose.InvokeAsync();
            StateHasChanged();
        }

        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OptionA.Blazor.Components/Modal/OptAModal.razor.js");                
            }
            else if (_awaitShow && _module is not null)
            {
                _awaitShow = false;
                var objRef = DotNetObjectReference.Create(this);
                await _module.InvokeVoidAsync(ShowDialogFunction, _dialog, objRef, nameof(OnDialogClose));
                await OnShow.InvokeAsync();
            }      
            else if (_awaitClose && _module is not null)
            {
                _awaitClose = false;
                await _module.InvokeVoidAsync(CloseDialogFunction, _dialog);
                await OnDialogClose();
            }
        }

        private bool GetIsDraggable()
        {
            return Draggable ?? DataProvider.Draggable;
        }

        private async Task TitleMouseDown(MouseEventArgs args)
        {
            if (_module is null || !GetIsDraggable())
            {
                return;
            }

            _dragging = true;
            var rectangle = await _module.InvokeAsync<BoundingRectangle>(GetBoundsFunction, _dialog);
            _startMouseX = args.ClientX;
            _startMouseY = args.ClientY;
            _startX = rectangle.Left;
            _startY = rectangle.Top;
        }

        private void OnDrag(MouseEventArgs args)
        {
            if (!_dragging)
            {
                return;
            }
            
            var divX = args.ClientX - _startMouseX;
            var divY = args.ClientY - _startMouseY;

            _offsetX = _startX + divX;
            _offsetY = _startY + divY;
        }

        private void EndDrag(MouseEventArgs args)
        {
            _dragging = false;
        }

        private Dictionary<string, object?> GetModalAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-modal-dialog"] = true
            };

            if (!string.IsNullOrEmpty(ModalId))
            {
                result["id"] = ModalId;
            }
            var baseClass = DataProvider.DialogClass;
            if (DataProvider.TryGetClassForSize(Size, out var sizeClass)) 
            {
                result["opta-size"] = $"{Size}".ToLowerInvariant();
                baseClass += $" {sizeClass}";
            }
            if (TryGetClasses(baseClass, out var classes))
            {
                result["class"] = classes;
            }
            if (_offsetX.HasValue && _offsetY.HasValue)
            {
                result["style"] = $"margin-top:{(int)_offsetY.Value}px;margin-left:{(int)_offsetX.Value}px;";
            }

            return result;
        }
        private Dictionary<string, object?> GetHeaderAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-modal-header"] = true
            };
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

        private Dictionary<string, object?> GetFooterAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-modal-footer"] = true
            };
            if (!string.IsNullOrEmpty(DataProvider.FooterClass))
            {
                result["class"] = DataProvider.FooterClass;
            }


            return result;
        }

        private Dictionary<string, object?> GetContentAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-modal-content"] = true
            };
            if (!string.IsNullOrEmpty(DataProvider.ContentClass))
            {
                result["class"] = DataProvider.ContentClass;
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

        private Dictionary<string, object?> GetSectionAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-modal-section"] = true
            };

            if (!string.IsNullOrEmpty(DataProvider.SectionClass))
            {
                result["class"] = DataProvider.SectionClass;
            }

            return result;
        }
    }
}
