using OptionA.Blazor.Blog;

namespace OptionA.Blazor.Test.Pages
{
    public partial class BlogBuilder
    {
        private string _value = string.Empty;
        private string Value
        {
            get => _value;
            set
            {
                _value = value;
                _paragraphContent = new ParagraphContent
                {
                    Content = _value
                };
                StateHasChanged();
            }
        }
        private ParagraphContent? _paragraphContent;
    }
}
