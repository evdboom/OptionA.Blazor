using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OptionA.Blazor.Components.Shared;
using System.Text;

namespace OptionA.Blazor.Components;

/// <summary>
/// splitter component for creating scalabale parts
/// </summary>
public partial class OptASplitter
{
    private const string GetBoundsFunction = "getBoundingRect";

    /// <summary>
    /// First child fragment, left or top (depending on orientation)
    /// </summary>
    [Parameter]
    public RenderFragment? First { get; set; }
    /// <summary>
    /// First child fragment, right or bottom (depending on orientation)
    /// </summary>
    [Parameter]
    public RenderFragment? Second { get; set; }
    /// <summary>
    /// Orientation of splitter
    /// </summary>
    [Parameter]
    public Orientation Orientation { get; set; }
    /// <summary>
    /// Initial percentage of width/height of first fragment
    /// </summary>
    [Parameter]
    public int? StartPercentageFirst { get; set; }
    /// <summary>
    /// Minimum percentage of width/height
    /// </summary>
    [Parameter]
    public int? MinimumPercentageFirst { get; set; }
    /// <summary>
    /// Minimum percentage of width/height
    /// </summary>
    [Parameter]
    public int? MinimumPercentageSecond { get; set; }
    /// <summary>
    /// Way to drag, constantly updating, or just an outline
    /// </summary>
    [Parameter]
    public DragMode DragMode { get; set; }

    [Inject]
    private ISplitterDataProvider DataProvider { get; set; } = null!;
    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;

    private int? _valueFirst, _dragValue, _min, _max;
    private bool _dragging;
    private double _left, _top, _barWidth, _barHeight, _onBarX, _onBarY;
    private ElementReference _splitter;
    private ElementReference _bar;
    private IJSObjectReference? _module;

    /// <inheritdoc/>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OptionA.Blazor.Components/Splitter/OptASplitter.razor.js");
        }
    }

    private Dictionary<string, object?> GetFirstContainerAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["opta-splitter-first"] = true
        };

        if (_valueFirst.HasValue)
        {
            result["style"] = $"--opta-first-width: {_valueFirst.Value}px;";
        }
        else if (StartPercentageFirst.HasValue)
        {
            result["style"] = $"--opta-first-width: {StartPercentageFirst.Value}%;";
        }


        return result;
    }

    private Dictionary<string, object?> GetSecondContainerAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["opta-splitter-second"] = true
        };

        return result;
    }

    private Dictionary<string, object?> GetSplitterAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["opta-splitter-bar"] = true
        };

        if (!string.IsNullOrEmpty(DataProvider.DragBarClass))
        {
            result["class"] = DataProvider.DragBarClass;
        }

        return result;
    }

    private Dictionary<string, object?> GetMainAttributes()
    {

        var result = GetAttributes();

        result["opta-splitter"] = true;            

        if (Orientation == Orientation.Vertical)
        {
            result["vertical"] = true;
        }

        if (TryGetClasses(null, out var classes))
        {
            result["class"] = classes;
        }


        return result;
    }

    private Dictionary<string, object?> GetOutlineAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["opta-splitter-outline"] = true,
        };
        
        if (!string.IsNullOrEmpty(DataProvider.OutlineClass))
        {
            result["class"] = DataProvider.OutlineClass;
        }

        var sb = new StringBuilder();
        sb.Append($"--opta-outline-width: {_barWidth}px;");
        sb.Append($"--opta-outline-height: {_barHeight}px;");
        sb.Append($"--opta-outline-position: {_dragValue}px;");

        result["style"] = sb.ToString();

        return result;
    }

    private async Task StartDrag(MouseEventArgs args)
    {
        if (_module is null)
        {
            return;
        }
        _dragging = true;
        var rectangle = await _module.InvokeAsync<BoundingRectangle>(GetBoundsFunction, _splitter);
        var barRectangle = await _module.InvokeAsync<BoundingRectangle>(GetBoundsFunction, _bar);
        _left = rectangle.Left;
        _top = rectangle.Top;
        _barWidth = barRectangle.Width;
        _barHeight = barRectangle.Height;

        _onBarX = args.ClientX - barRectangle.Left;
        _onBarY = args.ClientY - barRectangle.Top;

        _min = Orientation == Orientation.Horizontal
            ? GetMin(rectangle.Width - _barWidth)
            : GetMin(rectangle.Height - _barHeight);
        _max = Orientation == Orientation.Horizontal
            ? GetMax(rectangle.Width - _barWidth)
            : GetMax(rectangle.Height - _barHeight);

        if (DragMode == DragMode.Outline)
        {
            _dragValue = Orientation == Orientation.Horizontal
                ? (int)(barRectangle.Left - _left)
                : (int)(barRectangle.Top - _top);
        }
    }

    private int GetMin(double bound)
    {
        if (!MinimumPercentageFirst.HasValue)
        {
            return 0;
        }

        var percentage = (double)MinimumPercentageFirst.Value / 100;
        return (int)(bound * percentage);
    }

    private int GetMax(double bound)
    {
        if (!MinimumPercentageSecond.HasValue)
        {
            return (int)bound;
        }

        var percentage = (double)(100 - MinimumPercentageSecond.Value) / 100;
        return (int)(bound * percentage);
    }

    private void Drag(MouseEventArgs args)
    {
        if (!_dragging)
        {
            return;
        }

        var posX = args.ClientX - _left - _onBarX;
        var posY = args.ClientY - _top - _onBarY;

        var value = Orientation == Orientation.Horizontal
            ? (int)posX
            : (int)posY;

        if (value >= _min && value <= _max)
        {
            if (DragMode == DragMode.Direct)
            {
                _valueFirst = value;
            }
            else
            {
                _dragValue = value;
            }
        }
    }

    private void EndDrag()
    {
        _dragging = false;
        if (_dragValue.HasValue)
        {
            _valueFirst = _dragValue.Value;
            _dragValue = null;
        }
    }
}
