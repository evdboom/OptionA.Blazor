using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Component for inside <see cref="Block"/> component
    /// </summary>
    public partial class BlockContents
    {
        /// <summary>
        /// Content of component
        /// </summary>
        [Parameter]
        public BlockContent? Content { get; set; }
    }
}
