using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Component for passing dimension and dimension name to child components
    /// </summary>
    public partial class OptAResponsive
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
        /// Name of the cascading parameters for all the valid dimensions
        /// </summary>
        public const string ValidDimensionsParamterName = "ValidDimensions";

        [Inject]
        private IResponsiveService ResponsiveService { get; set; } = null!;
        /// <summary>
        /// Content
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        private NamedDimension _dimension;
        private string _dimensionName = string.Empty;
        private IEnumerable<string> _validDimensions = Enumerable.Empty<string>();

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await ResponsiveService.Initialize();
            _dimension = ResponsiveService.GetWindowSize();
            _validDimensions = ResponsiveService
                .ValidDimensions()
                .ToList();
            ResponsiveService.OnWindowSizeChanged += WindowSizeChanged;
            ResponsiveService.OnDimensionChanged += DimensionChanged;
        }

        private void DimensionChanged(object? sender, string e)
        {
            _dimensionName = e;
            _validDimensions = ResponsiveService
                .ValidDimensions()
                .ToList();
            StateHasChanged();
        }

        private void WindowSizeChanged(object? sender, NamedDimension e)
        {
            _dimension = e;
            StateHasChanged();
        }
    }
}
