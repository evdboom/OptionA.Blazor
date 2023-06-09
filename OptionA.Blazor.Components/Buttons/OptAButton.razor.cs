using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// A button to be used seperately or inside a button bar
    /// </summary>
    public partial class OptAButton
    {
        [Inject]
        private IButtonDataProvider Provider { get; set; } = null!;

        /// <summary>
        /// Value to be set for the id attribute
        /// </summary>
        [Parameter]
        public string? Id { get; set; }
        /// <summary>
        /// Name of the button
        /// </summary>
        [Parameter]
        public string? Name { get; set; }
        /// <summary>
        /// Tooltip for the button
        /// </summary>
        [Parameter]
        public string? Description { get; set; }
        /// <summary>
        /// Action to be performed when clicked
        /// </summary>
        [Parameter]
        public Func<MouseEventArgs, Task>? ClickAction { get; set; }
        /// <summary>
        /// If set and when true the button will be disabled
        /// </summary>
        [Parameter]
        public Func<bool>? DisabledCondition { get; set; }
        /// <summary>
        /// Actiontype for the button, determines the icon and classes set
        /// </summary>
        [Parameter]
        public ActionType ActionType { get; set; }
        /// <summary>
        /// Button type, icon, name or full
        /// </summary>
        [Parameter]
        public ButtonTypes ButtonType { get; set; }
        /// <summary>
        /// Set to true if the button is part of a form and should submit
        /// </summary>
        [Parameter]
        public bool IsSubmit { get; set; }
        /// <summary>
        /// Provide with custom icon for this button
        /// </summary>
        [Parameter]
        public string? OtherIconClass { get; set; }
        /// <summary>
        /// Provide with custom button class for this button
        /// </summary>
        [Parameter]
        public string? OtherButtonClass { get; set; }
        /// <summary>
        /// Additional classes to be added to the button
        /// </summary>
        [Parameter]
        public string? AdditionalClasses { get; set; }

        private async Task Click(MouseEventArgs args)
        {
            if (ClickAction is not null)
            {
                await ClickAction.Invoke(args);
            }
        }

        private bool IsDisabled()
        {
            return DisabledCondition != null && DisabledCondition.Invoke();
        }

        private string GetButtonClass()
        {
            return $"{Provider.GetActionClass(ActionType, OtherButtonClass)} {AdditionalClasses}";
        }

        private string GetToolTip()
        {
            return ButtonType.HasFlag(ButtonTypes.Name)
                ? Description ?? string.Empty
                : $"{Name} - {Description}";
        }

        private string GetIcon()
        {
            return $"{Provider.GetIconClass(ActionType, OtherIconClass)}";
        }

        private string GetButtonType()
        {
            return IsSubmit ? "submit" : "button";
        }
    }
}
