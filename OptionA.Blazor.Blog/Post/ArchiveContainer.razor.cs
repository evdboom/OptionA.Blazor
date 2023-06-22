using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Archive component
    /// </summary>
    public partial class ArchiveContainer
    {
        [Inject]
        private IPostService PostService { get; set; } = null!;

        private BlockContent? _content;

        /// <summary>
        /// Initialize content
        /// </summary>
        protected override void OnParametersSet()
        {
            var months = PostService.GetMonthsWithPosts();

            var listBuilder = ComponentBuilder
                .CreateBuilder()
                    .CreateBlock()
                        .AddClasses(DefaultClasses.ArchiveContainer)
                        .CreateList()
                            .WithListStyle(ListStyle.DisclosureClosed);
            
            foreach ( var month in months ) 
            {
                listBuilder
                    .CreateRow()
                        .CreateLink()
                            .WithHref($"/archive/{month.Year}/{month.Month}")
                            .AddDate(month, DateDisplayType.YearMonth)
                            .Build()
                        .Build();
            }

            _content = listBuilder
                            .Build()
                        .Build()
                    .BuildOne<BlockContent>();
        }
    }


}
