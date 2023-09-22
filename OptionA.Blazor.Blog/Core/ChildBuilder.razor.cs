using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    public partial class ChildBuilder
    {
        /// <summary>
        /// Content to build chid parts from
        /// </summary>
        [Parameter]
        public IEnumerable<IContent>? Content { get; set; }
    }
}
