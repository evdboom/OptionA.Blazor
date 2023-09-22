using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Map content to correct components
    /// </summary>
    public partial class OptaChildBuilder
    {
        /// <summary>
        /// Content to build chid parts from
        /// </summary>
        [Parameter]
        public IEnumerable<IContent>? Content { get; set; }
    }
}
