using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// List component
    /// </summary>
    public partial class List
    {
        /// <summary>
        /// Content for the component
        /// </summary>
        [Parameter]
        public ListContent? Content { get; set; }
        /// <summary>
        /// Child items to be added after the rendered part
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
