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

        [Inject]
        private IResponsiveService ResponsiveService { get; set; } = null!;
        /// <summary>
        /// Content
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        private NamedDimension _dimension;
        private string _dimensionName = string.Empty;

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await ResponsiveService.Initialize();
            _dimension = ResponsiveService.GetWindowSize();
            ResponsiveService.OnWindowSizeChanged += WindowSizeChanged;
            ResponsiveService.OnDimensionChanged += DimensionChanged;
        }

        private void DimensionChanged(object? sender, string e)
        {
            _dimensionName = e;
            StateHasChanged();
        }

        private void WindowSizeChanged(object? sender, NamedDimension e)
        {
            _dimension = e;
            StateHasChanged();
        }
    }
}
