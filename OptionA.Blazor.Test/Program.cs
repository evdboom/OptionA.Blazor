using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OptionA.Blazor.Components.Buttons.Struct;
using OptionA.Blazor.Components.Carousel.Struct;
using OptionA.Blazor.Components.Menu.Struct;
using OptionA.Blazor.Test;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services
    .AddBootstrapButtons()
    .AddBootstrapMenu()
    .AddBootstrapCarousel(config =>
    {
        config.AutoPlayText = "Autoplay";
    });
await builder.Build().RunAsync();
