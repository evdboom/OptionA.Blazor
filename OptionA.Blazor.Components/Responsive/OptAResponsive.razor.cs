using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Component for passing dimension and dimension name to child components
    /// </summary>
    public partial class OptAResponsive : IDisposable
    {
        /// <summary>
        /// Name of the cascading parameter for the dimension
        /// </summary>
        public const string DimensionParameterName = "Dimension";
        /// <summary>
        /// Name of the cascading parameter for the dimension name
        /// </summary>
        public const string DimensionNameParameterName = "DimensionName";
        /// <summary>
        /// Name of the cascading parameter for all the valid dimensions
        /// </summary>
        public const string ValidDimensionsParameterName = "ValidDimensions";
        /// <summary>
        /// Name of the cascading parameter for all the breakpoints dimensions
        /// </summary>
        public const string AllDimensionBreakPointsParameterName = "AllDimensionBreakPoints";

        [Inject]
        private IResponsiveService ResponsiveService { get; set; } = null!;
        /// <summary>
        /// Content
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        private NamedDimension _dimension;
        private string _dimensionName = string.Empty;
        private IEnumerable<(string Name, int Width)> _breakPoints = Enumerable.Empty<(string, int)>();
        private IEnumerable<string> _validDimensions = Enumerable.Empty<string>();

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            ResponsiveService.WindowSizeChanged += OnWindowSizeChanged;
            ResponsiveService.DimensionChanged += OnDimensionChanged;

            await ResponsiveService.Initialize();

            _dimension = ResponsiveService.GetWindowSize();
            _validDimensions = ResponsiveService
                .ValidDimensions()
                .ToList();
            _breakPoints = ResponsiveService
                .GetAllDimensionBreakPoints()
                .ToList();
        }

        private void OnDimensionChanged(object? sender, string e)
        {
            _dimensionName = e;
            _validDimensions = ResponsiveService
                .ValidDimensions()
                .ToList();
            StateHasChanged();
        }

        private void OnWindowSizeChanged(object? sender, NamedDimension e)
        {
            _dimension = e;
            StateHasChanged();
        }

        private bool _disposed;
        /// <inheritdoc/>
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            ResponsiveService.WindowSizeChanged -= OnWindowSizeChanged;
            ResponsiveService.DimensionChanged -= OnDimensionChanged;

            GC.SuppressFinalize(this);
        }
    }
}
