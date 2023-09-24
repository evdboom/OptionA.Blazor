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
        private const string TagId = "post-builder-tag";

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
        private IBlogDataProvider DataProvider { get; set; } = null!;

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
            if (!string.IsNullOrEmpty(DataProvider.BuilderFormClass))
            {
                result["class"] = DataProvider.BuilderFormClass;
            }
            return result;
        }

        private Dictionary<string, object?> GetTagListAttributes()
        {
            var result = new Dictionary<string, object?>();
            if (!string.IsNullOrEmpty(DataProvider.BuilderTagListClass))
            {
                result["class"] = DataProvider.BuilderTagListClass;
            }
            return result;
        }

        private Dictionary<string, object?> GetInputAttributes(string id, BuilderType type)
        {
            var result = new Dictionary<string, object?>();
            if (!string.IsNullOrEmpty(DataProvider.BuilderFormControlClass))
            {
                result["class"] = DataProvider.BuilderFormControlClass;
            }
            result["id"] = id;
            if (DataProvider.PropertiesForBuilderType(type) is BuilderTypeProperties properties)
            {
                result["placeholder"] = !string.IsNullOrEmpty(properties.Placeholder)
                    ? properties.Placeholder
                    : $"{type}..";
                if (!string.IsNullOrEmpty(properties.Class))
                {
                    if (result.ContainsKey("class"))
                    {
                        result["class"] += $" {properties.Class}"; 
                    }
                    else
                    {
                        result["class"] = $"{properties.Class}";
                    }
                }
            }

            return result;
        }

        private string? GetLabelForType(BuilderType type)
        {
            return DataProvider.PropertiesForBuilderType(type) is BuilderTypeProperties properties && !string.IsNullOrEmpty(properties.Label)
                ? properties.Label
                : $"{type}";
        }

        private InlineContent? GetContentForButton(BuilderType type, string defaultValue)
        {
            return new InlineContent
            {
                Content = DataProvider.PropertiesForBuilderType(type) is BuilderTypeProperties properties && !string.IsNullOrEmpty(properties.Content)
                ? properties.Content
                : defaultValue
            };
        }

        private Dictionary<string, object?> GetTagContainerAttributes()
        {
            var result = new Dictionary<string, object?>();
            if (DataProvider.PropertiesForBuilderType(BuilderType.TagContainer) is BuilderTypeProperties properties)
            {
                if (!string.IsNullOrEmpty(properties.Class))
                {
                    result["class"] = properties.Class;
                }
            }

            return result;
        }
        

        private Dictionary<string, object?> GetButtonAttributes(BuilderType type)
        {
            var result = new Dictionary<string, object?>();
            if (DataProvider.PropertiesForBuilderType(type) is BuilderTypeProperties properties)
            {
                if (!string.IsNullOrEmpty(properties.Class)) 
                {
                    result["class"] = properties.Class;
                }
                if (!string.IsNullOrEmpty(properties.Label))
                {
                    result["title"] = properties.Label;
                }                
            }            

            return result;
        }

        private Dictionary<string, object?> GetContainerAttributes(BuilderType type)
        {
            var result = new Dictionary<string, object?>();
            if (DataProvider.PropertiesForBuilderType(type) is BuilderTypeProperties properties)
            {
                if (!string.IsNullOrEmpty(properties.ContainerClass))
                {
                    result["class"] = properties.ContainerClass;
                }
            }

            return result;
        }

        private Dictionary<string, object?> GetInputLabelAttributes(string? inputId)
        {
            var result = new Dictionary<string, object?>();
            if (!string.IsNullOrEmpty(DataProvider.BuilderFormLabelClass))
            {
                result["class"] = DataProvider.BuilderFormLabelClass;
            }
            if (!string.IsNullOrEmpty(inputId))
            {
                result["for"] = inputId;
            }
            
            return result;
        }

        private void UpdateTag(string tag, int index)
        {
            if (_post is null || _post.Tags.Count <= index || _context is null)
            {
                return;
            }
            
            _post.Tags[index] = tag;

            var id = _context.Field(nameof(_post.Tags));
            _context.NotifyFieldChanged(id);
            StateHasChanged();
        }

        private void AddTag()
        {
            if (_post is null)
            {
                return;
            }
            _post.Tags.Add("");
            StateHasChanged();
        }

        private void RemoveTag(int index)
        {
            if (_post is null || _post.Tags.Count <= index || _context is null)
            {
                return;
            }

            _post.Tags.RemoveAt(index);

            var id = _context.Field(nameof(_post.Tags));
            _context.NotifyFieldChanged(id);
            StateHasChanged();
        }

    }
}
