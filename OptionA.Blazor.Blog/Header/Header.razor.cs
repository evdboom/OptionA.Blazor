using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Header component
    /// </summary>
    public partial class Header
    {
        /// <summary>
        /// Content for component
        /// </summary>
        [Parameter]
        public HeaderContent? Content { get; set; }
        /// <summary>
        /// Childcontent to be placed after content
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
