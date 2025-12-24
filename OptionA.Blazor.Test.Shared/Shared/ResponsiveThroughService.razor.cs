using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components;

namespace OptionA.Blazor.Test.Shared.Shared;

public partial class ResponsiveThroughService : ComponentBase, IDisposable
{
    [Inject]
    private IResponsiveService ResponsiveService { get; set; } = null!;
    private string _dimension = string.Empty;
    private NamedDimension? _namedDimension;
    private Dictionary<string, int>? _dimensionBreakPoints;

    protected override async Task OnInitializedAsync()
    {                        
        ResponsiveService.DimensionChanged += OnDimensionChanged;            
        ResponsiveService.WindowSizeChanged += OnWindowSizeChanged;

        await ResponsiveService.Initialize();
        _dimension = ResponsiveService.GetWindowSize().Name;
        _dimensionBreakPoints = ResponsiveService
            .GetAllDimensionBreakPoints()
            .ToDictionary(bp => bp.Name, bp => bp.Width);
    }

    private IEnumerable<string> ValidThroughAll()
    {
        if (_dimensionBreakPoints is null)
        {
            yield break;
        }

        foreach(var bp in _dimensionBreakPoints)
        {
            if (ResponsiveService.CurrentWidthEnoughForDimension(bp.Key))
            {
                yield return bp.Key;
            }
        }
    }

    private void OnDimensionChanged(object? sender, string e)
    {
        if (_disposed)
        {
            return;
        }
        _dimension = e;
        InvokeAsync(StateHasChanged);
    }

    private void OnWindowSizeChanged(object? sender, NamedDimension e) 
    {
        if (_disposed)
        {
            return;
        }
        _namedDimension = e;
        InvokeAsync(StateHasChanged);
    }

    private bool _disposed;
    public void Dispose() 
    {
        if (_disposed)
        {
            return;
        }
        
        _disposed = true;
        ResponsiveService.DimensionChanged -= OnDimensionChanged;
        ResponsiveService.WindowSizeChanged -= OnWindowSizeChanged;
        GC.SuppressFinalize(this);
    }
}
