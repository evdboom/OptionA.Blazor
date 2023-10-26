using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Component for placing a menu on the page
    /// </summary>
    public partial class OptAMenu
    {
        [Inject]
        private IMenuDataProvider DataProvider { get; set; } = null!;
        /// <summary>
        /// Menu items to display
        /// </summary>
        [Parameter]
        public RenderFragment? Items { get; set; }
        /// <summary>
        /// Additonal classes to add to the superceding nav container
        /// </summary>
        [Parameter]
        public string? AdditionalContainerClasses { get; set; }
        /// <summary>
        /// Orientation for menu, default is Horizontal
        /// </summary>
        [Parameter]
        public Orientation Orientation { get; set; } = Orientation.Horizontal;
        /// <summary>
        /// Called when a menu item is clicked
        /// </summary>
        [Parameter]
        public EventCallback OnItemSelected { get; set; }

        private Dictionary<string, object?> GetMenuAttributes()
        {
            var result = GetAttributes();
            result["opta-menu"] = true;
            
            if (Orientation == Orientation.Vertical)
            {
                result["vertical"] = true;
            }
            if (TryGetClasses(DataProvider.GetMenuClass(), out string classes))
            {
                result["class"] = classes;
            }
            if (Attributes is not null)
            {
                foreach (var attribute in Attributes)
                {
                    result[attribute.Key] = attribute.Value;
                }
            }

            return result;
        }

        private Dictionary<string, object?> GetContainerAttributes()
        {
            var result = new Dictionary<string, object?>();
            var defaultClasses = DataProvider
                .GetMenuContainerClass()
                .Split(' ')
                .ToList();
            var additional = (AdditionalContainerClasses ?? string.Empty)
                .Split(' ')
                .ToList();
            var resultClasses = defaultClasses
                .Concat(additional)
                .Distinct()
                .Where(c => !string.IsNullOrEmpty(c))
                .ToList();

            if (resultClasses.Any())
            {
                result["class"] = string.Join(' ', resultClasses);
            }                        

            return result;
        }
    }
}
