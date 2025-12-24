using OptionA.Blazor.Blog;
using OptionA.Blazor.Blog.Builder;
using OptionA.Blazor.Components;
using OptionA.Blazor.Storage;
using OptionA.Blazor.Test.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();

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
    }, lifetime: ServiceLifetime.Scoped);
builder.Services
    .AddOptionABootstrapBlog(config =>
    {
        config.PostTitleClass = "text-center opta-header";
        config.HeaderTagContainerClass = "text-center";
        config.PostDateClass = "text-center fst-italic";
        config.PostSubtitleClass = "text-center";
        config.TagClass = "opta-tag px-2 py-1 mx-1";

    }, lifetime: ServiceLifetime.Scoped)
    .AddOptionABootstrapBlogBuilder(config =>
    {
        config.ComponentButtonOptions = new()
        {
            [ContentType.Paragraph] = IconButton("bi bi-paragraph"),
            [ContentType.Header] = IconButton("bi bi-type-h2"),
            [ContentType.Code] = IconButton("bi bi-code-slash"),
            [ContentType.Quote] = IconButton("bi bi-chat-left-quote"),
            [ContentType.Image] = IconButton("bi bi-image"),
            [ContentType.Frame] = IconButton("bi bi-window"),
            [ContentType.List] = IconButton("bi bi-list-ul"),
            [ContentType.Table] = IconButton("bi bi-table"),
        };
        if (config.PostBuilderOptions is not null && config.PostBuilderOptions.TryGetValue(BuilderType.ComponentBar, out var componentBar))
        {
            componentBar.Class += " top-60";
        }
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
app.MapRazorComponents<App>();

app.Run();

static BuilderTypeProperties IconButton(string icon)
{
    return new BuilderTypeProperties
    {
        ContentType = ContentType.Icon,
        Content = icon
    };
}
