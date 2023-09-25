using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace OptionA.Blazor.Blog.Builder
{
    /// <summary>
    /// Builder component for posts
    /// </summary>
    public partial class OptaPostBuilder
    {
        private const string TitleId = "post-builder-title";
        private const string SubtitleId = "post-builder-subtitle";
        private const string DateId = "post-builder-date";

        /// <summary>
        /// Called whenever this post being build is updated;
        /// </summary>
        [Parameter]
        public EventCallback<Post?> OnPostChanged { get; set; }
        /// <summary>
        /// Called whenever the post is saved
        /// </summary>
        [Parameter]
        public EventCallback<Post> OnPostSaved { get; set; }
        [Inject]
        private IBlogBuilderDataProvider DataProvider { get; set; } = null!;

        private Post? _post;
        private EditContext? _context;

        private async Task CreateNewPost()
        {
            _post = new()
            {
                Date = DateTime.Today
            };
            await OnPostChanged.InvokeAsync(_post);
            _context = new(_post);
            _context.OnFieldChanged += FieldChanged;
            StateHasChanged();
        }

        private async void FieldChanged(object? sender, FieldChangedEventArgs e)
        {
            await OnPostChanged.InvokeAsync(_post);
        }

        private async Task SavePost()
        {
            if (_post is null || _context is null)
            {
                return;
            }

            _post.Tags.RemoveAll(string.IsNullOrWhiteSpace);

            await OnPostSaved.InvokeAsync(_post);
            _context.OnFieldChanged -= FieldChanged;
            _context = null;
            _post = null;
            StateHasChanged();
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

        private void AddContent(IContent content)
        {
            if (_post is null)
            {
                return;
            }

            _post.Content.Add(content);
        }
    }
}
