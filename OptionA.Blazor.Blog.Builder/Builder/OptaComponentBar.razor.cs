using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder
{
    public partial class OptaComponentBar
    {
        [Inject]
        private IBlogDataProvider DataProvider { get; set; }
    }
}
