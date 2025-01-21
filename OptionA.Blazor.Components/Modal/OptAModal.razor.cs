using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OptionA.Blazor.Components.Shared;
using System.Text;

namespace OptionA.Blazor.Components;

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
    /// Way to drag, direct or drag outline and move on mouseup
    /// </summary>
    [Parameter]
    public DragMode? DragMode { get; set; }
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
    private double _startMouseX, _startMouseY;
    private BoundingRectangle? _bounds;
    private int? _dragOffsetX, _dragOffsetY, _offsetX, _offsetY;
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
        await InvokeAsync(StateHasChanged);
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

    private DragMode GetDragMode()
    {
        return DragMode ?? DataProvider.DragMode;
    }

    private async Task TitleMouseDown(MouseEventArgs args)
    {
        if (_module is null || !GetIsDraggable())
        {
            return;
        }

        _dragging = true;
        _bounds = await _module.InvokeAsync<BoundingRectangle>(GetBoundsFunction, _dialog);
        _startMouseX = args.ClientX;
        _startMouseY = args.ClientY;

        if (GetDragMode() == Components.DragMode.Outline)
        {
            _dragOffsetX = (int)_bounds.Left;
            _dragOffsetY = (int)_bounds.Top;
        }
    }

    private void Drag(MouseEventArgs args)
    {
        if (!_dragging || _bounds is null)
        {
            return;
        }
        
        var divX = args.ClientX - _startMouseX;
        var divY = args.ClientY - _startMouseY;

        if (GetDragMode() == Components.DragMode.Direct)
        {
            _offsetX = (int)(_bounds.Left + divX);
            _offsetY = (int)(_bounds.Top + divY);
        }
        else
        {
            _dragOffsetX = (int)(_bounds.Left + divX);
            _dragOffsetY = (int)(_bounds.Top + divY);
        }
        
    }

    private void EndDrag(MouseEventArgs args)
    {
        _dragging = false;
        if (_dragOffsetX.HasValue && _dragOffsetY.HasValue)
        {
            _offsetX = _dragOffsetX.Value;
            _offsetY = _dragOffsetY.Value;
            _dragOffsetX = null;
            _dragOffsetY = null;
        }
    }

    private Dictionary<string, object?> GetOutlineAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["opta-modal-outline"] = true,
        };

        if (!string.IsNullOrEmpty(DataProvider.OutlineClass))
        {
            result["class"] = DataProvider.OutlineClass;
        }

        var sb = new StringBuilder();
        sb.Append($"--opta-outline-width: {_bounds?.Width}px;");
        sb.Append($"--opta-outline-height: {_bounds?.Height}px;");
        sb.Append($"--opta-outline-top: {_dragOffsetY}px;");
        sb.Append($"--opta-outline-left: {_dragOffsetX}px;");

        result["style"] = sb.ToString();

        return result;
    }

    private Dictionary<string, object?> GetModalAttributes()
    {
        var result = GetAttributes();
        result["opta-modal-dialog"] = true;            

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
            result["style"] = $"margin-top:{_offsetY.Value}px;margin-left:{_offsetX.Value}px;";
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
        if (_dragging)
        {
            result["dragging"] = true;
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
