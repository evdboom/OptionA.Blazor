using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Component for most text based parts
    /// </summary>
    public partial class OptAText
    {
        /// <summary>
        /// Content for this text block
        /// </summary>
        [Parameter]
        public TextContent? Content { get; set; }
        [Inject]
        private IMarkDownParser Parser { get; set; } = null!;
        [Inject]
        private IBlogDataProvider DataProvider { get; set; } = null!;

        private IEnumerable<IContent>? _content;

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            _content = Parser.Parse(Content?.Content);
            StateHasChanged();
        }
    }
}
