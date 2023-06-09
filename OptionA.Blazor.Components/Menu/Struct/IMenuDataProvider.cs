namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Interface for adding base cvlasses to menu components
    /// </summary>
    public interface IMenuDataProvider
    {
        /// <summary>
        /// Classes added to top level menu
        /// </summary>
        /// <returns></returns>
        string GetMenuClass();
        /// <summary>
        /// Classes added to menu items
        /// </summary>
        /// <returns></returns>
        string GetMenuItemClass();
        /// <summary>
        /// Classes added to divider inside dropdown
        /// </summary>
        /// <returns></returns>
        string GetDividerClass();
        /// <summary>
        /// Classes added to Group item (drowdown)
        /// </summary>
        /// <returns></returns>
        string GetGroupClass();
        /// <summary>
        /// Classes added to links in the menu
        /// </summary>
        /// <returns></returns>
        string GetLinkClass();
        /// <summary>
        /// Classes added to menu items considered active
        /// </summary>
        /// <returns></returns>
        string GetActiveClass();
        /// <summary>
        /// Classes for the toplevel nav element of the menu
        /// </summary>
        /// <returns></returns>
        string GetMenuContainerClass();
    }
}
