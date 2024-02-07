namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Location to place
    /// </summary>
    [Flags]
    public enum Location
    {
        /// <summary>
        /// Default locations
        /// </summary>
        Unset = 0,
        /// <summary>
        /// Place at the top
        /// </summary>
        Top = 1,
        /// <summary>
        /// Place on the right
        /// </summary>
        Right = 2,
        /// <summary>
        /// Place at the bottom
        /// </summary>
        Bottom = 4,
        /// <summary>
        /// Place on the left
        /// </summary>
        Left = 8
    }
}
