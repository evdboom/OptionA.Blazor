using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Header component
    /// </summary>
    public partial class OptaHeader
    {
        /// <summary>
        /// Content for the header
        /// </summary>
        [Parameter]
        public HeaderContent? Content { get; set; }
        [Inject]
        private IBlogDataProvider DataProvider { get; set; } = null!;
    }
}
