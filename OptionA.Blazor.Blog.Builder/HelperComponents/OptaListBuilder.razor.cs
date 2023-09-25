using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.HelperComponents
{
    /// <summary>
    /// Component for builder list, like tags or classes
    /// </summary>
    public partial class OptaListBuilder
    {
        /// <summary>
        /// Type to get the list for
        /// </summary>
        [Parameter]
        public BuilderType BuilderType { get; set; }
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
        /// Called whenever an item in the list changes
        /// </summary>
        [Parameter]
        public EventCallback OnChange { get; set; }
        [Inject]
        private IBlogBuilderDataProvider DataProvider { get; set; } = null!;

        private async Task UpdateItem(string value, int index)
        {
            if (Items is null || Items.Count <= index)
            {
                return;
            }

            Items[index] = value;
            await OnChange.InvokeAsync();
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
            await OnChange.InvokeAsync();
        }

    }
}
