using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Selector component
    /// </summary>
    public partial class CompoundItem
    {
        /// <summary>
        /// Content of component
        /// </summary>
        [Parameter]
        public IContent? Content { get; set; }
    }
}
