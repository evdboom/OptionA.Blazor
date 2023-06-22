using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Block component
    /// </summary>
    public partial class Block
    {
        /// <summary>
        /// Content for the component
        /// </summary>
        [Parameter]
        public BlockContent? Content { get; set; }
        /// <summary>
        /// Optional child content to render after the content
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        private async Task Click(MouseEventArgs args)
        {
            if (Content?.OnClick is null)
            {
                return;
            }

            await Content.OnClick.Invoke(args);
            StateHasChanged();
        }
    }
}
