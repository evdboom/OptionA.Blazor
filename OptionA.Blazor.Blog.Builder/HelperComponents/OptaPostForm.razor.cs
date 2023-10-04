using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace OptionA.Blazor.Blog.Builder.HelperComponents
{
    public partial class OptaPostForm
    {
        /// <summary>
        /// _post for form
        /// </summary>
        [Parameter]
        public Post? Post { get; set; }
        /// <summary>
        /// Called whenever a _post has changes
        /// </summary>
        [Parameter]
        public EventCallback PostChanged { get; set; }
        /// <summary>
        /// Called when the save _post button is clicked
        /// </summary>
        [Parameter]
        public EventCallback PostSubmitted { get; set; }
        [Inject]
        private IBlogBuilderDataProvider DataProvider { get; set; } = null!;

        private EditContext? _context;
        private Post? _post;

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            if (_post is null && Post is null)
            {
                return;
            }
            else if (_post is not null && Post is not null)
            {
                return;
            }

            _post = Post;
            if (_post is not null)
            {
                _context = new EditContext(_post);
                _context.OnFieldChanged += FieldChanged;
            }
            else if (_context is not null)
            {
                _context.OnFieldChanged -= FieldChanged;
                _context = null;
            }
        }

        private void OnContentAdded(IContent content)
        {
            if (_post is null)
            {
                return;
            }

            _post.Content.Add(content);
        }

        private async void FieldChanged(object? sender, FieldChangedEventArgs e)
        {
            await PostChanged.InvokeAsync(_post);
        }

        private async Task SavePost()
        {
            await PostSubmitted.InvokeAsync();
        }

        private Dictionary<string, object?> GetSavePostAttributes()
        {
            var result = new Dictionary<string, object?>()
            {
                ["title"] = "Save the Post"
            };

            return DataProvider.GetAttributes(BuilderType.SavePostButton, result);            
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
