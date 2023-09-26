using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace OptionA.Blazor.Blog.Builder
{
    /// <summary>
    /// Builder component for posts
    /// </summary>
    public partial class OptaPostBuilder
    {
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

        private Post? _post;
        private EditContext? _context;

        private void OnContentAdded(IContent content)
        {
            if (_post is null)
            {
                return;
            }

            _post.Content.Add(content);
        }

        private async Task CreateNewPost()
        {
            _post = new()
            {
                Date = DateTime.Today
            };
            await PostChanged.InvokeAsync(_post);
            _context = new(_post);
            _context.OnFieldChanged += FieldChanged;
            StateHasChanged();
        }

        private async void FieldChanged(object? sender, FieldChangedEventArgs e)
        {
            await PostChanged.InvokeAsync(_post);
        }

        private async Task SavePost()
        {
            if (_post is null || _context is null)
            {
                return;
            }

            _post.Tags.RemoveAll(string.IsNullOrWhiteSpace);

            await PostSaved.InvokeAsync(_post);
            _context.OnFieldChanged -= FieldChanged;
            _context = null;
            _post = null;
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
                    foreach(var attribute in  DataProvider.CreatePostButton.AdditionalAttributes)
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

        private Dictionary<string, object?> GetSavePostAttributes()
        {
            var result = new Dictionary<string, object?>()
            {
                ["title"] = "Save the post"
            };
            if (DataProvider.SavePostButton is not null)
            {
                if (!string.IsNullOrEmpty(DataProvider.SavePostButton.Class))
                {
                    result["class"] = DataProvider.SavePostButton.Class;
                }
                if (DataProvider.SavePostButton.AdditionalAttributes is not null)
                {
                    foreach (var attribute in DataProvider.SavePostButton.AdditionalAttributes)
                    {
                        result[attribute.Key] = attribute.Value;
                    }
                }
            }
            return result;
        }

        private InlineContent? GetSavePostContent()
        {
            return DataProvider.SavePostButton?.Content is not null
                ? new InlineContent
                {
                    Content = DataProvider.SavePostButton.Content,
                }
                : new InlineContent
                {
                    Content = "Save",
                };
        }

        private Dictionary<string, object?> GetSavePostContainerAttributes()
        {
            var result = new Dictionary<string, object?>();
            if (DataProvider.SavePostButton is not null)
            {
                if (!string.IsNullOrEmpty(DataProvider.SavePostButton.ContainerClass))
                {
                    result["class"] = DataProvider.SavePostButton.ContainerClass;
                }
            }
            return result;
        }

        private Dictionary<string, object?> GetFormAttributes()
        {
            var result = new Dictionary<string, object?>();
            if (!string.IsNullOrEmpty(DataProvider.FormClass))
            {
                result["class"] = DataProvider.FormClass;
            }
            return result;
        }

        private void OnRemove(IContent content)
        {
            if (_post is null || _context is null)
            {
                return;
            }

            _post.Content.Remove(content);
            var id = _context.Field(nameof(_post.Content));
            _context.NotifyFieldChanged(id);
        }

        private void OnChange(string property)
        {
            if (_context is null)
            {
                return;
            }

            var id = _context.Field(property);
            _context.NotifyFieldChanged(id);
        }

        private void OnMoveUp(IContent content)
        {
            if (_post is null || _context is null)
            {
                return;
            }

            var index = _post.Content.IndexOf(content);
            if (index > 0)
            {
                _post.Content.RemoveAt(index);
                index--;
                _post.Content.Insert(index, content);

                var id = _context.Field(nameof(_post.Content));
                _context.NotifyFieldChanged(id);
            }
        }

        private void OnMoveDown(IContent content)
        {
            if (_post is null || _context is null)
            {
                return;
            }

            var index = _post.Content.IndexOf(content);
            if (index >= 0 && index < _post.Content.Count - 1)
            {
                _post.Content.RemoveAt(index);
                index++;
                _post.Content.Insert(index, content);

                var id = _context.Field(nameof(_post.Content));
                _context.NotifyFieldChanged(id);
            }
        }
    }
}
