using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.HelperComponents
{
    /// <summary>
    /// Component for builder list, like tags or classes
    /// </summary>
    public partial class OptaListBuilder
    {
        /// <summary>
        /// List of items
        /// </summary>
        [Parameter]
        public List<string>? Items { get; set; }
        /// <summary>
        /// Label for the list
        /// </summary>
        [Parameter]
        public string? Label { get; set; }
        /// <summary>
        /// Placeholder for items
        /// </summary>
        [Parameter]
        public string? ItemPlaceholder { get; set; }
        /// <summary>
        /// Title for items
        /// </summary>
        [Parameter]
        public string? ItemTitle { get; set; }
        /// <summary>
        /// Called whenever an item in the list changes
        /// </summary>
        [Parameter]
        public EventCallback ContentChanged { get; set; }
        [Inject]
        private IBlogBuilderDataProvider DataProvider { get; set; } = null!;

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            if (Items is not null && !Items.Any())
            {
                AddItem();                
            }
        }

        private async Task UpdateItem(string value, int index)
        {
            if (Items is null || Items.Count <= index)
            {
                return;
            }

            Items[index] = value;

            if (!string.IsNullOrEmpty(value) && index == Items.Count - 1) 
            {
                AddItem();
            }

            await ContentChanged.InvokeAsync();
        }

        private void AddItem()
        {
            if (Items is null)
            {
                return;
            }
            Items.Add(string.Empty);
        }

        private async Task RemoveItem(int index)
        {
            if (Items is null || Items.Count <= index)
            {
                return;
            }

            Items.RemoveAt(index);
            await ContentChanged.InvokeAsync();
        }

        private Dictionary<string, object?> GetItemAttributes()
        {
            var defaultAttributes = new Dictionary<string, object?>();

            if (!string.IsNullOrEmpty(ItemPlaceholder))
            {
                defaultAttributes["placeholder"] = ItemPlaceholder;
            }
            if (!string.IsNullOrEmpty(ItemTitle))
            {
                defaultAttributes["title"] = ItemTitle;
            }

            return DataProvider.GetAttributes(BuilderType.ListItemInput, defaultAttributes);
        }

    }
}