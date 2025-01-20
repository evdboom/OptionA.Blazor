using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using OptionA.Blazor.Blog.Core.Extensions;
using OptionA.Blazor.Blog.Services;
using OptionA.Blazor.Storage;

namespace OptionA.Blazor.Blog.Builder;

/// <summary>
/// Builder component for posts
/// </summary>
public partial class OptAPostBuilder : IDisposable
{
    private const string RegisterHandlerFunction = "registerHandler";
    private const string UnRegisterHandlerFunction = "unRegisterHandler";
    private const string StorageKey = "opta-post-storage";

    /// <summary>
    /// Post to build or edit
    /// </summary>
    [Parameter]
    public Post? Post { get; set; }
    /// <summary>
    /// Called whenever this post being build is updated;
    /// </summary>
    [Parameter]
    public EventCallback<Post?> PostChanged { get; set; }
    /// <summary>
    /// Called whenever the post is saved
    /// </summary>
    [Parameter]
    public EventCallback<Post> PostSaved { get; set; }
    [Inject]
    private IBlogBuilderDataProvider DataProvider { get; set; } = null!;
    [Inject]
    private IStorageService StorageService { get; set; } = null!;
    [Inject]
    private NavigationManager Navigation { get; set; } = null!;
    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject]
    private IBuilderService BuilderService { get; set; } = null!;

    private Post? _post;
    private IJSObjectReference? _module;
    private string? _eventId;

    /// <summary>
    /// Stores current post if not saved
    /// </summary>
    [JSInvokable]
    public async void BuilderUnloaded()
    {
        Navigation.LocationChanged -= LocationChanged;
        await StoreUnfinishedPost();
    }

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        Navigation.LocationChanged += LocationChanged;
    }

    /// <inheritdoc/>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        _module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OptionA.Blazor.Blog.Builder/OptAPostBuilder.razor.js");
        var objRef = DotNetObjectReference.Create(this);
        _eventId = await _module.InvokeAsync<string>(RegisterHandlerFunction, objRef, nameof(BuilderUnloaded));

        if (_post is null)
        {
            if ((await StorageService.GetItemAsync<string>(StorageLocation.Local, StorageKey)) is string postJson)
            {
                var post = BuilderService.CreateFromJson(postJson);
                _post = post;
                await PostChanged.InvokeAsync(_post);
                await InvokeAsync(StateHasChanged);
            }
        }
    }

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        if (Post is not null)
        {
            _post = Post;
        }
    }

    private async void LocationChanged(object? sender, LocationChangedEventArgs e)
    {
        await StoreUnfinishedPost();
    }

    private async Task StoreUnfinishedPost()
    {
        if (_post is not null)
        {
            var json = BuilderService.ToJson(_post);
            await StorageService.SetItemAsync(StorageLocation.Local, StorageKey, json);
        }
    }

    private async Task CreateNewPost()
    {
        _post = new()
        {
            Date = DateTime.Today
        };
        await PostChanged.InvokeAsync(_post);
    }

    private async Task OnPostChanged()
    {
        if (_post is null)
        {
            return;
        }

        await PostChanged.InvokeAsync(_post);
    }


    private async Task OnSavePost()
    {
        if (_post is null)
        {
            return;
        }

        _post.Tags.RemoveAll(string.IsNullOrWhiteSpace);
        _post.Content.RemoveAll(content => content.IsInvalid);
        foreach (var content in _post.Content)
        {
            content.RemovedClasses.RemoveAll(string.IsNullOrWhiteSpace);
            content.AdditionalClasses.RemoveAll(string.IsNullOrWhiteSpace);                
        }

        await PostSaved.InvokeAsync(_post);
        _post = null;
        await PostChanged.InvokeAsync(_post);
        await StorageService.RemoveItemAsync(StorageLocation.Local, StorageKey);
        await InvokeAsync(StateHasChanged);
    }

    private bool _disposed;
    /// <summary>
    /// Disposes the builder
    /// </summary>
    public async void Dispose()
    {
        if (!_disposed)
        {
            await Dispose(true);
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }

    private async Task Dispose(bool disposing)
    {
        if (disposing)
        {
            Navigation.LocationChanged -= LocationChanged;
            if (_module is not null && !string.IsNullOrEmpty(_eventId))
            {
                await _module.InvokeVoidAsync(UnRegisterHandlerFunction, _eventId);
            }                
        }
    }
}
