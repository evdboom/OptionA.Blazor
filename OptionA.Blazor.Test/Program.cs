using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OptionA.Blazor.Components;
using OptionA.Blazor.Test;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services
    .AddOptionABootstrapComponents(configuration: options =>
    {
        options.CarouselConfiguration = (carousel) =>
        {
            carousel.AutoPlayText = "Autoplay";
        };
        options.MenuConfiguration = (menu) =>
        {
            menu.OpenGroupOnMouseOver = true;
            menu.GroupCloseTime = 250;
        };
    });
await builder.Build().RunAsync();
