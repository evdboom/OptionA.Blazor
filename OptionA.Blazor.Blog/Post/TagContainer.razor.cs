using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Component for displaying most used tags
    /// </summary>
    public partial class TagContainer
    {
        [Inject]
        private IPostService PostService { get; set; } = null!;
        /// <summary>
        /// Sets the maximum amount of tags to display
        /// </summary>
        [Parameter]
        public int? MaxCount { get; set; }        

        private BlockContent? _content;

        /// <summary>
        /// Initialize content
        /// </summary>
        protected override void OnParametersSet()
        {
            var tags = PostService.GetTags();
            if (MaxCount.HasValue)
            {
                tags = tags.Take(MaxCount.Value);
            }
            _content = ComponentBuilder
                .CreateBuilder()
                    .CreateBlock()
                    .AddClasses(DefaultClasses.TagContainer)
                    .AddTags(tags)
                    .Build()
                .BuildOne<BlockContent>();
        }
    }
}
