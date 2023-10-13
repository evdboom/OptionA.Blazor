using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.Parts
{
    /// <summary>
    /// Component for the general post properties
    /// </summary>
    public partial class OptAPostProperties
    {
        private const string TitleId = "post-builder-title";
        private const string SubtitleId = "post-builder-subtitle";
        private const string DateId = "post-builder-date";

        /// <summary>
        /// Post to create
        /// </summary>
        [Parameter]
        public Post? Post { get; set; }
        /// <summary>
        /// Called when a property gets updated
        /// </summary>
        [Parameter]
        public EventCallback<string> PropertyChanged { get; set; }
        [Inject]
        private IBlogBuilderDataProvider DataProvider { get; set; } = null!;

        private Dictionary<string, object?> GetLabelAttributes(string id)
        {
            var defaultAttributes = new Dictionary<string, object?>
            {
                ["for"] = id
            };

            return DataProvider.GetAttributes(BuilderType.Label, defaultAttributes);
        }

        private Dictionary<string, object?> GetTitleAttributes()
        {
            var defaultAttributes = new Dictionary<string, object?>
            {
                ["id"] = TitleId,
                ["placeholder"] = "Title..."
            };

            return DataProvider.GetAttributes(BuilderType.TextInput, defaultAttributes);
        }

        private Dictionary<string, object?> GetDateAttributes()
        {
            var defaultAttributes = new Dictionary<string, object?>
            {
                ["id"] = DateId
            };

            return DataProvider.GetAttributes(BuilderType.DateInput, defaultAttributes);
        }

        private Dictionary<string, object?> GetSubtitleAttributes()
        {
            var defaultAttributes = new Dictionary<string, object?>
            {
                ["id"] = SubtitleId,
                ["placeholder"] = "Subtitle..."
            };

            return DataProvider.GetAttributes(BuilderType.TextAreaInput, defaultAttributes);
        }
    }
}
