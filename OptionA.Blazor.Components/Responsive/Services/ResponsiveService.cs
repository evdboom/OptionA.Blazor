using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace OptionA.Blazor.Components.Services
{
    /// <inheritdoc/>
    public class ResponsiveService : IResponsiveService, IDisposable
    {
        private const string UnregisterHandlerFunction = "unregisterHandler";
        private const string RegisterHandlerFunction = "registerHandler";
        private const string GetDimensionFunction = "getDimension";

        private NamedDimension? _currentDimension;
        private bool _initialized;
        private string? _eventId;

        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        private readonly Dictionary<int, string> _sizes;
        private readonly Dictionary<string, int> _sizesByName;

        /// <inheritdoc/>
        public event EventHandler<NamedDimension>? OnWindowSizeChanged;
        /// <inheritdoc/>
        public event EventHandler<string>? OnDimensionChanged;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="dataProvider"></param>
        public ResponsiveService(IJSRuntime jsRuntime, IResponsiveDataProvider dataProvider)
        {
            _sizes = dataProvider.Sizes;
            _sizesByName = _sizes.ToDictionary(size => size.Value, size => size.Key);

            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
              "import", "./_content/OptionA.Blazor.Components/Responsive/OptAResponsive.razor.js").AsTask());
        }

        /// <inheritdoc/>
        public IEnumerable<(string Name, int Width)> GetAllDimensionBreakPoints()
        {
            return _sizes.Select(size => (Name: size.Value, Width: size.Key));
        }

        /// <inheritdoc/>
        public IEnumerable<string> ValidDimensions()
        {
            return _sizesByName
                .Where(size => CurrentWidthEnoughForDimension(size.Key))
                .Select(size => size.Key);
        }

        /// <inheritdoc/>
        public bool CurrentWidthEnoughForDimension(string targetDimension)
        {
            if (!_initialized)
            {
                throw new InvalidOperationException("Service not initialized yet");
            }
            if (!_sizesByName.ContainsKey(targetDimension))
            {
                throw new InvalidOperationException($"{targetDimension} Is not known");
            }

            return _currentDimension!.Value.Width >= _sizesByName[targetDimension];
        }

        /// <inheritdoc/>
        public NamedDimension GetWindowSize()
        {
            if (!_initialized)
            {
                throw new InvalidOperationException("Service not initialized yet");
            }

            return _currentDimension!.Value;
        }

        /// <inheritdoc/>/>
        public async Task Initialize()
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;
            var objRef = DotNetObjectReference.Create(this);
            var module = await _moduleTask.Value;
            var dimension = await module.InvokeAsync<Dimension>(GetDimensionFunction);
            var dimensionName = GetDimensionName(dimension.Width);
            _currentDimension = new NamedDimension
            {
                Name = dimensionName,
                Height = dimension.Height,
                Width = dimension.Width
            };

            _eventId = await module.InvokeAsync<string>(RegisterHandlerFunction, objRef, nameof(WindowSizeChanged));
        }

        /// <summary>
        /// Invoked from javascript when bound to the onresize event
        /// </summary>
        /// <param name="dimension"></param>
        [JSInvokable]
        public void WindowSizeChanged(Dimension dimension)
        {
            var dimensionName = GetDimensionName(dimension.Width);
            var oldName = _currentDimension!.Value.Name;
            _currentDimension = new NamedDimension
            {
                Name = dimensionName,
                Height = dimension.Height,
                Width = dimension.Width
            };
            if (!(oldName?.Equals(dimensionName) ?? false))
            {
                OnDimensionChanged?.Invoke(this, dimensionName);
            }

            OnWindowSizeChanged?.Invoke(this, _currentDimension.Value);
        }

        private string GetDimensionName(int width)
        {
            return _sizes
                .OrderByDescending(size => size.Key)
                .First(size => size.Key <= width).Value;
        }


        private bool _disposed;
        /// <inheritdoc/>
        public async void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            if (!string.IsNullOrEmpty(_eventId))
            {
                var module = await _moduleTask.Value;
                await module.InvokeVoidAsync(UnregisterHandlerFunction, _eventId);
            }
            GC.SuppressFinalize(this);
        }

    }
}
