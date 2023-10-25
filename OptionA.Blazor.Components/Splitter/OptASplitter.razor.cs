using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OptionA.Blazor.Components.Shared;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// splitter component for creating scalabale parts
    /// </summary>
    public partial class OptASplitter
    {
        private const string GetBoundsFunction = "getBoundingRect";
        private const string StartDragFunction = "startDragging";

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
        [Inject]
        private IJSRuntime JsRuntime { get; set; } = null!;

        private int? _valueFirst, _min, _max;
        private bool _dragging;
        private double? _left, _top;
        private ElementReference _splitter;
        private IJSObjectReference? _module;
        private IJSObjectReference? _abortController;

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
            _left = rectangle.Left;
            _top = rectangle.Top;

            _min = Orientation == Orientation.Horizontal
                ? GetMin(rectangle.Width)
                : GetMin(rectangle.Height);
            _max = Orientation == Orientation.Horizontal
                ? GetMax(rectangle.Width)
                : GetMax(rectangle.Height);

            var objRef = DotNetObjectReference.Create(this);
            await _module.InvokeVoidAsync(StartDragFunction, objRef, nameof(Drag), nameof(EndDrag));

        }

        private int GetMin(double bound)
        {

        }

        private int GetMax(double bound)
        {

        }

        /// <summary>
        /// called by js to end dragging
        /// </summary>
        [JSInvokable]
        public void EndDrag()
        {
            _dragging = false;
            StateHasChanged();

        }

        /// <summary>
        /// called by js when dragging
        /// </summary>
        /// <param name="args"></param>
        [JSInvokable]
        public void Drag(MouseEvent args)
        {
            if (!_dragging)
            {
                return;
            }
            var left = args.ClientX - _left;
            var top = args.ClientY - _top;

            var min = MinimumPercentageFirst ?? 0;
            var max = 100 - (MinimumPercentageSecond ?? 0);
            var percentage = Orientation == Orientation.Horizontal
                ? 100 * left / _width
                : 100 * top / _height;

            if (percentage >= min && percentage <= max)
            {
                _percentageFirst = (int?)percentage;
                StateHasChanged();
            }
            else if (percentage < min && (!_percentageFirst.HasValue ||  _percentageFirst.Value != min))
            {
                _percentageFirst = min;
                StateHasChanged();
            }
            else if (percentage > max && (!_percentageFirst.HasValue || _percentageFirst.Value != max))
            {
                _percentageFirst = max;
                StateHasChanged();
            }
        }
    }
}
