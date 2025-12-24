using OptionA.Blazor.Blog;
using OptionA.Blazor.Blog.Builder;
using OptionA.Blazor.Components;
using OptionA.Blazor.Storage;
using OptionA.Blazor.Test.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();

// Note: Some OptionA components use JSRuntime which is scoped in Blazor Server.
// The library registers these as Singleton, which causes DI validation errors.
// For this test project, we'll register the core components that work with Server.
builder.Services
    .AddOptionABootstrapButtons()
    .AddOptionABootstrapMenu(darkMode: true)
    .AddOptionABootstrapCarousel()
    .AddOptionABootstrapGallery()
    .AddOptionABootstrapModal()
    .AddOptionABootstrapSplitter()
    .AddOptionABootstrapMessageBox()
    .AddOptionABootstrapTabs();

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
            [ContentType.Frame] = IconButton("bi bi-window"),
            [ContentType.List] = IconButton("bi bi-list-ul"),
            [ContentType.Table] = IconButton("bi bi-table"),
        };
        if (config.PostBuilderOptions is not null && config.PostBuilderOptions.TryGetValue(BuilderType.ComponentBar, out var componentBar))
        {
            componentBar.Class += " top-60";
        }
    });

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
