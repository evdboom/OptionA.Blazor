using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    public partial class OptATableRow
    {
        /// <summary>
        /// Content for the component
        /// </summary>
        [Parameter]
        public IList<string>? Content { get; set; }
        private IEnumerable<IContent>? _content;

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            if (Content is null)
            {
                return;
            }

            _content = Content.Select(item => new InlineContent { Content = item });

            await InvokeAsync(StateHasChanged);
        }
    }
}