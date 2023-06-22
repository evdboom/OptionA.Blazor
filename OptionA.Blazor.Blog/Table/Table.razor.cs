using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Table component
    /// </summary>
    public partial class Table
    {
        /// <summary>
        /// Content for the component
        /// </summary>
        [Parameter]
        public TableContent? Content { get; set; }

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
