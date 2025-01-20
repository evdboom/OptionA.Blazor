using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Code component
    /// </summary>
    public partial class OptACode
    {
        /// <summary>
        /// Code to display
        /// </summary>
        [Parameter]
        public CodeContent? Content { get; set; }
        [Inject]
        private IBlogDataProvider DataProvider { get; set; } = null!;
        [Inject]
        private IEnumerable<ICodeParser> Parsers { get; set; } = null!;
        private BlockContent? Header
        {
            get
            {
                if (Content is null)
                {
                    return null;
                }
                var language = Content.Language == CodeLanguage.Other && !string.IsNullOrEmpty(Content.OtherLanguage) 
                    ? Content.OtherLanguage 
                    : Content.Language.ToDisplayLanguage();
                var result = new BlockContent
                {
                    Content = language
                };
                result.Attributes["opta-code"] = "header";
                return result;
            }
        }

        private IEnumerable<IContent>? _content;

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            if (Content != null)
            {
                Content.Attributes["opta-code"] = "block";
                var parser = Parsers.FirstOrDefault(p => p.Language == Content.Language);
                if (parser != null) 
                {
                    _content = parser.Parse(Content.Code, DataProvider.NewLine);
                }
                else
                {
                    _content = new List<IContent>
                    {
                        new TextContent { Content = Content.Code ?? string.Empty }
                    };
                }
            }
            else
            {
                _content = null;
            }
        }
    }
}
