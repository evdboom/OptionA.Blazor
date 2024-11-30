using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    public partial class OptAListContent
    {
        /// <summary>
        /// Content for the component
        /// </summary>
        [Parameter]
        public ListContent? Content { get; set; }

        private IEnumerable<IContent>? _content;

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            if (Content is null)
            {
                return;
            }

            _content = Content.Items.Count > 0
                ? Content.Items
                    .Select(item => new TextContent { Content = item }) 
                : Content.ItemsFromString
                    .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                    .Select(item => new TextContent { Content = item });

            await InvokeAsync(StateHasChanged);
        }
    }
}