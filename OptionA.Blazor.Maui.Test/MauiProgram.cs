using Microsoft.Extensions.Logging;
using OptionA.Blazor.Components;

namespace OptionA.Blazor.Maui.Test;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        builder.Services
            .AddOptionABootstrapComponents(darkMode: true, configuration: options =>
            {
                options.CarouselConfiguration = (carousel) =>
                {
                    carousel.AutoPlayText = "Autoplay";
                };
                options.MenuConfiguration = (menu) =>
                {
                    menu.OpenGroupOnMouseOver = true;
                    menu.GroupCloseTime = 250;
                    menu.DefaultMenuContainerClass += " opta-bg ps-2 sticky-top";
                    menu.DefaultDropdownClass = "opta-bg opta-dropdown";
                    menu.DefaultMenuItemClass += " opta-menu-item";
                };
            });
        return builder.Build();
    }
}
