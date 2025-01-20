using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.Parts
{
    /// <summary>
    /// Build code blog part
    /// </summary>
    public partial class OptACodeBuilder
    {
        private const string CodeId = "opta-code";
        private const string CodeLanguageId = "opta-code-Language";
        private const string OtherLanguageId = "opta-other-Language";

        /// <summary>
        /// Index of the current content in the collection
        /// </summary>
        [Parameter]
        public int ContentIndex { get; set; }
        /// <summary>
        /// Total number of content (for disabling move up, move down)
        /// </summary>
        [Parameter]
        public int TotalContentCount { get; set; }
        /// <summary>
        /// Content to create
        /// </summary>
        [Parameter]
        public CodeContent? Content { get; set; }
        /// <summary>
        /// Called when something changes
        /// </summary>
        [Parameter]
        public EventCallback ContentChanged { get; set; }
        /// <summary>
        /// Called when content should be removed
        /// </summary>
        [Parameter]
        public EventCallback ContentRemoved { get; set; }
        /// <summary>
        /// Occurs when move up is clicked
        /// </summary>
        [Parameter]
        public EventCallback MovedUp { get; set; }
        /// <summary>
        /// Occurs when move down is clicked
        /// </summary>
        [Parameter]
        public EventCallback MovedDown { get; set; }
        [Inject]
        private IBlogBuilderDataProvider DataProvider { get; set; } = null!;

        private CodeLanguage InternalLanguage
        {
            get => Content?.Language ?? default;
            set
            {
                if (Content is null)
                {
                    return;
                }
                if (!value.Equals(Content.Language))
                {
                    Content.Language = value;
                    if (ContentChanged.HasDelegate)
                    {
                        ContentChanged.InvokeAsync();
                    }
                }
            }
        }

        private Dictionary<string, object?> GetCodeAttributes()
        {
            var defaultAttributes = new Dictionary<string, object?>
            {
                ["placeholder"] = "Text...",
                ["id"] = $"{CodeId}-{ContentIndex}"
            };

            return DataProvider.GetAttributes(BuilderType.TextAreaInput, defaultAttributes);
        }

        private Dictionary<string, object?> GetCodeLanguageAttributes()
        {
            var defaultAttributes = new Dictionary<string, object?>
            {
                ["id"] = $"{CodeLanguageId}-{ContentIndex}"
            };

            return DataProvider.GetAttributes(BuilderType.SelectInput, defaultAttributes);
        }

        private Dictionary<string, object?> GetOtherLanguageAttributes()
        {
            var defaultAttributes = new Dictionary<string, object?>
            {
                ["id"] = $"{OtherLanguageId}-{ContentIndex}"
            };

            return DataProvider.GetAttributes(BuilderType.TextInput, defaultAttributes);
        }



        private Dictionary<string, object?> GetLabelAttributes(string id)
        {
            var defaultAttributes = new Dictionary<string, object?>
            {
                ["for"] = $"{id}-{ContentIndex}"
            };

            return DataProvider.GetAttributes(BuilderType.Label, defaultAttributes);
        }
    }
}
