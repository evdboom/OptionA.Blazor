using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// List of post content
    /// </summary>
    public partial class PostList
    {
        /// <summary>
        /// Posts to display
        /// </summary>
        [Parameter]
        public IEnumerable<IPost>? Content { get; set; }
    }
}
