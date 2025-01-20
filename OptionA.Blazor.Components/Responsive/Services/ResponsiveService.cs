using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace OptionA.Blazor.Components.Services;

/// <inheritdoc/>
public class ResponsiveService : IResponsiveService, IDisposable
{
    private const string RegisterHandlerFunction = "registerHandler";
    private const string GetDimensionFunction = "getDimension";
    private const string AbortFunction = "abort";
    private const string UnnamedSize = "Unnamed";

    private NamedDimension? _currentDimension;
    private bool _initialized;
    private IJSObjectReference? _abortController;

    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    private readonly Dictionary<int, string> _sizes;
    private readonly Dictionary<string, int> _sizesByName;

    /// <inheritdoc/>
    public event EventHandler<NamedDimension>? WindowSizeChanged;
    /// <inheritdoc/>
    public event EventHandler<string>? DimensionChanged;

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
            return false;
        }
        if (!_sizesByName.ContainsKey(targetDimension))
        {
            return false;
        }

        return _currentDimension!.Value.Width >= _sizesByName[targetDimension];
    }

    /// <inheritdoc/>
    public NamedDimension GetWindowSize()
    {
        if (!_initialized)
        {
            return new NamedDimension
            {
                Height = 0,
                Width = 0,
                Name = UnnamedSize
            };
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
                  
        var module = await _moduleTask.Value;
        var dimension = await module.InvokeAsync<Dimension>(GetDimensionFunction);
        var dimensionName = GetDimensionName(dimension.Width);
        _currentDimension = new NamedDimension
        {
            Name = dimensionName,
            Height = dimension.Height,
            Width = dimension.Width
        };

        var objRef = DotNetObjectReference.Create(this);
        _abortController = await module.InvokeAsync<IJSObjectReference>(RegisterHandlerFunction, objRef, nameof(OnWindowSizeChanged));
        _initialized = true;
    }

    /// <summary>
    /// Invoked from javascript when bound to the onresize event
    /// </summary>
    /// <param name="dimension"></param>
    [JSInvokable]
    public void OnWindowSizeChanged(Dimension dimension)
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
            DimensionChanged?.Invoke(this, dimensionName);
        }

        WindowSizeChanged?.Invoke(this, _currentDimension.Value);
    }

    private string GetDimensionName(int width)
    {
        return _sizes
            .OrderByDescending(size => size.Key)
            .FirstOrDefault(size => size.Key <= width).Value ?? UnnamedSize;
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
        if (_abortController is not null)
        {
            await _abortController.InvokeVoidAsync(AbortFunction);
        }
        GC.SuppressFinalize(this);
    }

}
