using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components;

namespace OptionA.Blazor.Test.Pages
{
    public partial class ResponsiveService
    {
        [Inject]
        private IResponsiveService ResponsiveServiceService { get; set; } = null!;
        private string _dimension = string.Empty;


        protected override async Task OnInitializedAsync()
        {
            await ResponsiveServiceService.Initialize();
            ResponsiveServiceService.OnDimensionChanged += ResponsiveService_OnDimensionChanged;
            _dimension = ResponsiveServiceService.GetWindowSize().Name;
        }

        private void ResponsiveService_OnDimensionChanged(object? sender, string e)
        {
            _dimension = e;
            StateHasChanged();
        }
    }
}
