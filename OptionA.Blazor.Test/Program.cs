using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OptionA.Blazor.Blog;
using OptionA.Blazor.Blog.Builder;
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
builder.Services
    .AddOptionABootstrapBlog(config =>
    {
        config.PostTitleClass = "text-center opta-header";
        config.HeaderTagContainerClass = "text-center";
        config.PostDateClass = "text-center fst-italic";
        config.PostSubtitleClass = "text-center";
        config.TagClass = "opta-tag px-2 py-1 mx-1";
        
    })
    .AddOptionABootstrapBlogBuilder(config =>
    {
        config.ComponentButtonOptions = new()
        {
            [ContentType.Paragraph] = IconButton("bi bi-paragraph"),            
            [ContentType.Header] = IconButton("bi bi-type-h2"),
            [ContentType.Code] = IconButton("bi bi-code-slash"),
            [ContentType.Quote] = IconButton("bi bi-chat-left-quote"),
            [ContentType.Image] = IconButton("bi bi-image"),
            [ContentType.Frame] = IconButton("bi bi-window")
        };
    });
await builder.Build().RunAsync();

BuilderTypeProperties IconButton(string icon)
{
    return new BuilderTypeProperties
    {
        ContentType = ContentType.Icon,
        Content = icon
    };
}