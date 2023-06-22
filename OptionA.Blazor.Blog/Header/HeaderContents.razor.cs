using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Contents for header components
    /// </summary>
    public partial class HeaderContents
    {
        /// <summary>
        /// Actual contents
        /// </summary>
        [Parameter]
        public HeaderContent? Content { get; set; }
    }
}
