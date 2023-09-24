using OptionA.Blazor.Blog;

namespace OptionA.Blazor.Test.Pages
{
    public partial class BlogBuilder
    {
        private Post? _post;

        private void PostChanged(Post? post) 
        {
            _post = post;
            StateHasChanged();
        }
    }
}
