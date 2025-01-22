using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace OptionA.Blazor.Blog.Builder.HelperComponents;

/// <summary>
/// Generic setup for blog components
/// </summary>
public partial class OptABlogComponent
{
    private const string CloseDialogFunction = "closeDialog";
    private const string ShowDialogFunction = "showDialog";
    private const string ListCloseFunction = "listenClose";
    private const string GetBoundsFunction = "getBoundingRect";

    /// <summary>
    /// Set to true to not render the remove and up down buttons
    /// </summary>
    [Parameter]
    public bool HideButtons { get; set; }
    /// <summary>
    /// Index of the current content in the collection
    /// </summary>
    [Parameter]
    public int ContentIndex { get; set; }
    /// <summary>
    /// Total number of content (for disabling move up, move down)
    /// </summary>
    [Parameter]
    public int TotalContentCount { get; set; }
    /// <summary>
    /// Content to display
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    /// <summary>
    /// Additional properties to display
    /// </summary>
    [Parameter]
    public RenderFragment? AdditionalProperties { get; set; }
    /// <summary>
    /// Name of the component
    /// </summary>
    [Parameter]
    public string? Name { get; set; }
    /// <summary>
    /// Content for this component
    /// </summary>
    [Parameter]
    public IContent? Content { get; set; }
    /// <summary>
    /// Called whenever the content is changed
    /// </summary>
    [Parameter]
    public EventCallback ContentChanged { get; set; }
    /// <summary>
    /// Called whenever the content should be removed
    /// </summary>
    [Parameter]
    public EventCallback ContentRemoved { get; set; }
    /// <summary>
    /// Occurs when move up is clicked
    /// </summary>
    [Parameter]
    public EventCallback MovedUp { get; set; }
    /// <summary>
    /// Occurs when move down is clicked
    /// </summary>
    [Parameter]
    public EventCallback MovedDown { get; set; }
    /// <summary>
    /// Indicates if the component can be dragged and can be dropped on
    /// </summary>
    [Parameter]
    public bool CanDrag { get; set; }
    /// <summary>
    /// Called when the drag operation is started
    /// </summary>
    [Parameter]
    public EventCallback<DragEvent> DragStarted { get; set; }
    /// <summary>
    /// Called when the drag operation is ended
    /// </summary>
    [Parameter]
    public EventCallback<DragEvent> DragEnded { get; set; }

    [Inject]
    private IBlogBuilderDataProvider DataProvider { get; set; } = null!;
    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;

    private List<string>? _additionalAttributes;

    private bool _dragging;
    private bool _dragTargetLower;
    private bool _dragTargetUpper;
    private bool _showDialog;
    private bool _awaitShow, _awaitClose;
    private bool _collapsed;
    private BoundingRectangle? _bounds;
    private ElementReference _dialog;
    private ElementReference _component;
    private IJSObjectReference? _module;

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        if (Content is not null)
        {
            _additionalAttributes ??= Content.Attributes.Select(x => $"{x.Key}={x.Value}").ToList();
        }
        else
        {
            _additionalAttributes = null;
        } 
    }

    private async Task DragStart(DragEventArgs args)
    {
        _dragging = true;
        if (DragStarted.HasDelegate && Content is not null)
        {
            await DragStarted.InvokeAsync(new DragEvent(Content, false));
        }
    }

    private void DragOver(DragEventArgs args)
    {
        if (_dragging || _bounds is null)
        {
            return;
        }

        var mouseY = args.ClientY;
        var elementMidY = _bounds.Top + (_bounds.Height / 2);
        _dragTargetLower = mouseY >= elementMidY;
        _dragTargetUpper = !_dragTargetLower;       
    }

    private async Task DragEnter(DragEventArgs args)
    {
        if (_module is null || _dragging)
        {
            return;
        }

        if (_bounds is null)
        {
            _bounds = await _module.InvokeAsync<BoundingRectangle>(GetBoundsFunction, _component);
        }

        var mouseY = args.ClientY;
        var elementMidY = _bounds.Top + (_bounds.Height / 2);

        _dragTargetLower = mouseY >= elementMidY;
        _dragTargetUpper = !_dragTargetLower;
    }

    private void DragEnd(DragEventArgs args)
    {
        _dragging = false;
        _dragTargetUpper = false;
        _dragTargetLower = false;
    }

    private void DragLeave(DragEventArgs args)
    {
        _dragTargetUpper = false;
        _dragTargetLower = false;
    }

    private async Task OnDrop()
    {
        if (_dragging)
        {
            return;
        }

        if (DragEnded.HasDelegate && Content is not null)
        {
            await DragEnded.InvokeAsync(new DragEvent(Content, _dragTargetUpper));
        }

        _dragging = false;
        _dragTargetUpper = false;
        _dragTargetLower = false;
    }

    private void EditProperties()
    {
        if (_showDialog)
        {
            return;
        }
        _showDialog = true;
        _awaitShow = true;
    }

    private void Close()
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
    public void OnDialogClose()
    {
        _showDialog = false;
        InvokeAsync(StateHasChanged);
    }

    /// <inheritdoc/>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OptionA.Blazor.Blog.Builder/HelperComponents/OptABlogComponent.razor.js");
        }
        else if (_awaitShow && _module is not null)
        {
            _awaitShow = false;
            var objRef = DotNetObjectReference.Create(this);
            await _module.InvokeVoidAsync(ShowDialogFunction, _dialog, objRef, nameof(OnDialogClose));
        }
        else if (_awaitClose && _module is not null)
        {
            _awaitClose = false;
            await _module.InvokeVoidAsync(CloseDialogFunction, _dialog);
            OnDialogClose();
        }
    }

    private async Task AttributesChanged()
    {
        if (_additionalAttributes is null || Content is null)
        {
            return;
        }

        Content.Attributes.Clear();
        foreach (var attr in _additionalAttributes)
        {
            var split = attr.Split('=');
            if (split.Length != 2)
            {
                continue;
            }
            Content.Attributes.Add(split[0], split[1]);
        }

        await ContentChanged.InvokeAsync();
    }

    private Dictionary<string, object?> GetMoveUpAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["title"] = "Move up in list",
            ["type"] = "button",
            ["onclick"] = EventCallback.Factory.Create<MouseEventArgs>(this, async () => await MovedUp.InvokeAsync())
        };

        if (ContentIndex <= 0)
        {
            result["disabled"] = true;
        }

        return DataProvider.GetAttributes(BuilderType.MoveUpButton, result);
    }

    private Dictionary<string, object?> GetMoveDownAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["title"] = "Move down in list",
            ["type"] = "button",
            ["onclick"] = EventCallback.Factory.Create<MouseEventArgs>(this, async () => await MovedDown.InvokeAsync())
        };

        if (ContentIndex >= TotalContentCount - 1)
        {
            result["disabled"] = true;
        }

        return DataProvider.GetAttributes(BuilderType.MoveDownButton, result);
    }

    private Dictionary<string, object?> GetCollapseAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["title"] = "Expand/Collapse",
            ["type"] = "button",
            ["onclick"] = EventCallback.Factory.Create<MouseEventArgs>(this, () => _collapsed = !_collapsed)
        };

        var type = _collapsed 
            ? BuilderType.ExpandButton 
            : BuilderType.CollapseButton;

        return DataProvider.GetAttributes(type, result);
    }
    

    private Dictionary<string, object?> GetRemoveAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["title"] = "Remove the item",
            ["type"] = "button",
            ["onclick"] = EventCallback.Factory.Create<MouseEventArgs>(this, async () => await ContentRemoved.InvokeAsync())
        };

        return DataProvider.GetAttributes(BuilderType.RemoveButton, result);
    }

    private Dictionary<string, object?> GetPropertiesAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["title"] = "Change the properties",
            ["type"] = "button",
            ["onclick"] = EventCallback.Factory.Create(this, EditProperties)
        };

        return DataProvider.GetAttributes(BuilderType.PropertiesButton, result);
    }

    private Dictionary<string, object?> GetPropertiesModalAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["opta-modal-dialog"] = true,
        };

        return DataProvider.GetAttributes(BuilderType.PropertiesModal, result);
    }
    private Dictionary<string, object?> GetHeaderAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["opta-modal-header"] = true
        };
        
        return DataProvider.GetAttributes(BuilderType.PropertiesModalHeader, result);
    }

    private Dictionary<string, object?> GetContentAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["opta-modal-content"] = true
        };

        return DataProvider.GetAttributes(BuilderType.PropertiesModalContent, result);
    }

    private Dictionary<string, object?> GetComponentContentAttributes()
    {        
        var result = new Dictionary<string, object?>
        {
            ["draggable"] = "true",
        };
        if (_dragging)
        {
            result["dragging"] = true;
        }
        if (CanDrag)
        {
            result["ondragstart"] = EventCallback.Factory.Create<DragEventArgs>(this, DragStart);
            result["ondragend"] = EventCallback.Factory.Create<DragEventArgs>(this, DragEnd);
            result["ondragenter"] = EventCallback.Factory.Create<DragEventArgs>(this, DragEnter);
        }

        return DataProvider.GetAttributes(BuilderType.ComponentContent, result);
    }

    private Dictionary<string, object?> GetDragTargetAttributes(bool lower)
    {
        var result = new Dictionary<string, object?>
        {
            ["lower"] = lower,
            ["drag-target"] = true,
            ["ondragover"] = EventCallback.Factory.Create<DragEventArgs>(this, DragOver),
            ["ondragleave"] = EventCallback.Factory.Create<DragEventArgs>(this, DragLeave),
            ["ondrop"] = EventCallback.Factory.Create<DragEventArgs>(this, OnDrop),
        };

        return result;
    }

    private Dictionary<string, object?> GetCloseButtonAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["type"] = "button",
            ["onclick"] = EventCallback.Factory.Create<MouseEventArgs>(this, Close)
        };

        return DataProvider.GetAttributes(BuilderType.PropertiesModalCloseButton, result);
    }

    private Dictionary<string, object?> GetSectionAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["opta-modal-section"] = true
        };

        return DataProvider.GetAttributes(BuilderType.PropertiesModalSection, result);
    }

    private IContent? GetCollapseContent()
    {
        return _collapsed 
            ? DataProvider.GetContent(BuilderType.ExpandButton, "Expand")
            : DataProvider.GetContent(BuilderType.CollapseButton, "Collapse");
    }
}
