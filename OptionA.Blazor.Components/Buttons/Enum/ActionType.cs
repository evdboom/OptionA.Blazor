namespace OptionA.Blazor.Components.Buttons.Enum
{
    /// <summary>
    /// Action type for buttons, determines the classes set
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// Default action type, will result in default icon and buton classes
        /// </summary>
        Default,
        /// <summary>
        /// Button is an add button
        /// </summary>
        Add,
        /// <summary>
        /// Button is a remove or delete button
        /// </summary>
        Remove,
        /// <summary>
        /// Button is a refresh button
        /// </summary>
        Refresh,
        /// <summary>
        /// Button is a search button
        /// </summary>
        Search,
        /// <summary>
        /// Button is an edit button
        /// </summary>
        Edit,
        /// <summary>
        /// Button is a cancel button
        /// </summary>
        Cancel,
        /// <summary>
        /// Button is a confirmation button
        /// </summary>
        Confirm,
        /// <summary>
        /// Button is something else, provide your own classes to set them correctly
        /// </summary>
        Other
    }
}
