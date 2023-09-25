using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.Parts
{
    /// <summary>
    /// Header builder component
    /// </summary>
    public partial class OptaHeaderBuilder
    {
        /// <summary>
        /// Content to create
        /// </summary>
        [Parameter]
        public HeaderContent? Content { get; set; }
        /// <summary>
        /// Called when something changes
        /// </summary>
        [Parameter]
        public EventCallback OnChange { get; set; }
        /// <summary>
        /// Called when content should be removed
        /// </summary>
        [Parameter]
        public EventCallback<IContent> OnRemove { get; set; }
        [Inject]
        private IBlogBuilderDataProvider DataProvider { get; set; } = null!;

        private HeaderSize InternalSize
        {
            get => Content?.Size ?? default;
            set
            {
                if (Content is null)
                {
                    return;
                }
                if (!value.Equals(Content.Size))
                {
                    Content.Size = value;
                    if (OnChange.HasDelegate)
                    {
                        OnChange.InvokeAsync();
                    }
                }
            }
        }
    }
}
