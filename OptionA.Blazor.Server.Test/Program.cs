using OptionA.Blazor.Blog;
using OptionA.Blazor.Components;
using OptionA.Blazor.Playground;
using OptionA.Blazor.Storage;
using OptionA.Blazor.Test.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

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
    }, lifetime: ServiceLifetime.Scoped)
    .AddOptionABootstrapPlayground();
builder.Services
    .AddOptionABootstrapBlog(config =>
    {
        config.PostTitleClass = "text-center opta-header";
        config.HeaderTagContainerClass = "text-center";
        config.PostDateClass = "text-center fst-italic";
        config.PostSubtitleClass = "text-center";
        config.TagClass = "opta-tag px-2 py-1 mx-1";

    }, lifetime: ServiceLifetime.Scoped)
    .AddOptionAStorageServices(ServiceLifetime.Scoped);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
