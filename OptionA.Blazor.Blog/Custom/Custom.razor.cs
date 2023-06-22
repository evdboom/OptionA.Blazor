using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Wrapper for custom content
    /// </summary>
    public partial class Custom
    {
        /// <summary>
        /// Content for the component
        /// </summary>
        [Parameter]
        public CustomContent? Content { get; set; }
    }
}
