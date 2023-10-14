using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Map content to correct components
    /// </summary>
    public partial class OptAChildBuilder
    {
        /// <summary>
        /// Content to build child parts from
        /// </summary>
        [Parameter]
        public IEnumerable<IContent>? Content { get; set; }
    }
}
