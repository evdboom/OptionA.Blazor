using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// A list to display
    /// </summary>
    public partial class OptAList
    {
        /// <summary>
        /// Content for the component
        /// </summary>
        [Parameter]
        public ListContent? Content { get; set; }
        [Inject]
        private IBlogDataProvider DataProvider { get; set; } = null!;
    }
}