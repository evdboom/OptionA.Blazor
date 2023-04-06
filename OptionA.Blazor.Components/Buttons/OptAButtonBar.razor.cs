using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components.Buttons.Enum;

namespace OptionA.Blazor.Components.Buttons
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
        /// Additonal classes to add the the bar
        /// </summary>
        [Parameter]
        public string? AdditionalClasses { get; set; }
        /// <summary>
        /// True if the bar should be sticky to its position
        /// </summary>
        [Parameter]
        public bool Sticky { get; set; }

        private string GetOrientation()
        {
            return Orientation == Orientation.Horizontal
                ? "horizontal"
                : "vertical";
        }
    }
}
