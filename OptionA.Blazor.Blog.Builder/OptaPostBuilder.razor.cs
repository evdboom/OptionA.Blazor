using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using OptionA.Blazor.Storage;

namespace OptionA.Blazor.Blog.Builder
{
    /// <summary>
    /// Builder component for posts
    /// </summary>
    public partial class OptaPostBuilder : IDisposable
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

        private Post? _post;
        private  IJSObjectReference? _module;

        /// <summary>
        /// Stores current post if not saved
        /// </summary>
        [JSInvokable]
        public async void BuilderUnloaded()
        {
            Navigation.LocationChanged -= LocationChanged;
            if (_post is not null)
            {
                await StorageService.SetItemAsync(StorageLocation.Local, StorageKey, _post);
            }
        }

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            Navigation.LocationChanged += LocationChanged;
        }

        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender || _post is not null)
            {
                return;
            }

            _module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/OptionA.Blazor.Blog.Builder/OptaPostBuilder.razor.js");
            var objRef = DotNetObjectReference.Create(this);
            await _module.InvokeVoidAsync(RegisterHandlerFunction, objRef, nameof(BuilderUnloaded));

            if (_post is null)
            {
                if ((await StorageService.GetItemAsync<Post>(StorageLocation.Local, StorageKey)) is Post post)
                {
                    _post = post;
                    await PostChanged.InvokeAsync(_post);
                    StateHasChanged();
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
            if (_post is not null)
            {
                await StorageService.SetItemAsync(StorageLocation.Local, StorageKey, _post);
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

            await PostSaved.InvokeAsync(_post);
            _post = null;
            await PostChanged.InvokeAsync(_post);
            await StorageService.RemoveItemAsync(StorageLocation.Local, StorageKey);
            StateHasChanged();
        }

        private Dictionary<string, object?> GetCreatePostAttributes()
        {
            var result = new Dictionary<string, object?>()
            {
                ["title"] = "Create a new post"
            };
            if (DataProvider.CreatePostButton is not null)
            {
                if (!string.IsNullOrEmpty(DataProvider.CreatePostButton.Class))
                {
                    result["class"] = DataProvider.CreatePostButton.Class;
                }
                if (DataProvider.CreatePostButton.AdditionalAttributes is not null)
                {
                    foreach (var attribute in DataProvider.CreatePostButton.AdditionalAttributes)
                    {
                        result[attribute.Key] = attribute.Value;
                    }
                }
            }
            return result;
        }

        private InlineContent? GetCreatePostContent()
        {
            return DataProvider.CreatePostButton?.Content is not null
                ? new InlineContent
                {
                    Content = DataProvider.CreatePostButton.Content,
                }
                : new InlineContent
                {
                    Content = "Create new post",
                };
        }

        private Dictionary<string, object?> GetCreatePostContainerAttributes()
        {
            var result = new Dictionary<string, object?>();
            if (DataProvider.CreatePostButton is not null)
            {
                if (!string.IsNullOrEmpty(DataProvider.CreatePostButton.ContainerClass))
                {
                    result["class"] = DataProvider.CreatePostButton.ContainerClass;
                }
            }
            return result;
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
                if (_module is not null)
                {
                    await _module.InvokeVoidAsync(UnRegisterHandlerFunction);
                }                
            }
        }
    }
}
