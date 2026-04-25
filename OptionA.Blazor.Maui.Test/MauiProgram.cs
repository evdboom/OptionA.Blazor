using Microsoft.Extensions.Logging;
using OptionA.Blazor.Blog;
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
        builder.Services
            .AddOptionABootstrapBlog(config =>
            {
                config.PostTitleClass = "text-center opta-header";
                config.HeaderTagContainerClass = "text-center";
                config.PostDateClass = "text-center fst-italic";
                config.PostSubtitleClass = "text-center";
                config.TagClass = "opta-tag px-2 py-1 mx-1";

            });

        return builder.Build();
    }
}
