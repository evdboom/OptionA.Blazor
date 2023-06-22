using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// ComponentList selector component
    /// </summary>
    public partial class Compound
    {
        /// <summary>
        /// Content in component
        /// </summary>
        [Parameter]
        public IList<IContent>? Content { get; set; }
    }
}
