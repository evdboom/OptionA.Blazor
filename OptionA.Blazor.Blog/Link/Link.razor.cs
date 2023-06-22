using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Link component
    /// </summary>
    public partial class Link
    {
        [Inject]
        private NavigationManager Navigation { get; set; } = null!;
        /// <summary>
        /// Content for the component
        /// </summary>
        [Parameter]
        public LinkContent? Content { get; set; }

        private bool PreventDefault()
        {
            if (Content is null)
            {
                return false;
            }

            return Content.Mode == LinkMode.Internal;
        }

        private async Task Click(MouseEventArgs args)
        {
            if (Content is null)
            {
                return;
            }

            if (Content.OnClick is null)
            {
                if (Content.Mode == LinkMode.Internal)
                {
                    Navigation.NavigateTo(Content.Href);
                }
                
                return;
            }

            await Content.OnClick.Invoke(args);
            StateHasChanged();
        }
    }
}
