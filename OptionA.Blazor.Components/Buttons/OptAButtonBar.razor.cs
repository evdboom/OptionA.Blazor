using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Button bar to display multiple buttons
    /// </summary>
    public partial class OptAButtonBar
    {
        /// <summary>
        /// Sets the id attribute of the bar
        /// </summary>
        [Parameter]
        public string? Id { get; set; }
        /// <summary>
        /// Adds a header to the bar with the given name
        /// </summary>
        [Parameter]
        public string? Name { get; set; }
        /// <summary>
        /// Orientation of the button bar
        /// </summary>
        [Parameter]
        public Orientation Orientation { get; set; }
        /// <summary>
        /// Location of the bar in the parent container
        /// </summary>
        [Parameter]
        public Location Location { get; set; }
        /// <summary>
        /// Buttons on the left or top (depending on <see cref="Orientation"/>)
        /// </summary>
        [Parameter]
        public RenderFragment? StartButtons { get; set; }
        /// <summary>
        /// Buttons in the middle
        /// </summary>
        [Parameter]
        public RenderFragment? CenterButtons { get; set; }
        /// <summary>
        /// Buttons on the right or bottom (depending on <see cref="Orientation"/>)
        /// </summary>
        [Parameter]
        public RenderFragment? EndButtons { get; set; }
        /// <summary>
        /// True if the bar should be sticky to its position
        /// </summary>
        [Parameter]
        public bool Sticky { get; set; }
        
        private Dictionary<string, object?> GetBarAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-bar"] = true,
                ["orientation"] = Orientation == Orientation.Horizontal
                    ? "horizontal"
                    : "vertical"
            };

            if (TryGetClasses(string.Empty, out var classes))
            {
                result["class"] = classes;
            }

            if (!string.IsNullOrEmpty(Id))
            {
                result["id"] = Id;
            }

            return result;
        }

        private Dictionary<string, object?> GetGroupAttributes(string alignment)
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-group"] = true,
                ["orientation"] = Orientation == Orientation.Horizontal
                    ? "horizontal"
                    : "vertical",
                ["button-alignment"] = alignment
            };

            return result;
        }
    }
}
