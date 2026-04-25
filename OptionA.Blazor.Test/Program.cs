using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OptionA.Blazor.Blog;
using OptionA.Blazor.Components;
using OptionA.Blazor.Playground;
using OptionA.Blazor.Storage;
using OptionA.Blazor.Test.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
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
    })
    .AddOptionABootstrapPlayground();
builder.Services
    .AddOptionABootstrapBlog(config =>
    {
        config.PostTitleClass = "text-center opta-header";
        config.HeaderTagContainerClass = "text-center";
        config.PostDateClass = "text-center fst-italic";
        config.PostSubtitleClass = "text-center";
        config.TagClass = "opta-tag px-2 py-1 mx-1";

    })
    .AddOptionAStorageServices(ServiceLifetime.Singleton);
await builder.Build().RunAsync();
