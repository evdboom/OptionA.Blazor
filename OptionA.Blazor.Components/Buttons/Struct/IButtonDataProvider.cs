namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Interface for use in buttons to provide the correct classes and icons
    /// </summary>
    public interface IButtonDataProvider
    {
        /// <summary>
        /// Class to add to button bar
        /// </summary>
        public string? DefaultButtonBarClass { get; }
        /// <summary>
        /// Class to add to group of buttons in button bar
        /// </summary>
        public string? DefaultButtonGroupClass { get; }
        /// <summary>
        /// Get the button classes for the given <see cref="ActionType"/>
        /// </summary>
        /// <param name="actionType"></param>
        /// <returns></returns>
        string? GetActionClass(ActionType actionType);
        /// <summary>
        /// Get the button classes for the given actiontype, returns otherButtonClass for <see cref="ActionType.Other"/>
        /// </summary>
        /// <param name="actionType"></param>
        /// <param name="otherButtonClass"></param>
        /// <returns></returns>
        string? GetActionClass(ActionType actionType, string? otherButtonClass);
        /// <summary>
        /// Gets the icon classes for the given <see cref="ActionType"/>
        /// </summary>
        /// <param name="actionType"></param>
        /// <returns></returns>
        string? GetIconClass(ActionType actionType);
        /// <summary>
        ///  Get the button classes for the given actiontype, returns otherIconClass for <see cref="ActionType.Other"/>
        /// </summary>
        /// <param name="actionType"></param>
        /// <param name="otherIconClass"></param>
        /// <returns></returns>
        string? GetIconClass(ActionType actionType, string? otherIconClass);
    }
}
