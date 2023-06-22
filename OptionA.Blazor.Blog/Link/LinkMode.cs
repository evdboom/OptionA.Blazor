namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Enum to determine what type of link to use
    /// </summary>
    public enum LinkMode
    {
        /// <summary>
        /// Use for internal routing through NavigationManager
        /// </summary>
        Internal,
        /// <summary>
        /// External link, but opens over self
        /// </summary>
        Self,
        /// <summary>
        /// Extenanl link, opens in new tab
        /// </summary>
        NewTab
    }
}
