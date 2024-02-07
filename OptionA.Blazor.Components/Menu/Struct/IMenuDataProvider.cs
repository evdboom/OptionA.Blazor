namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Interface for adding base classes to menu components
    /// </summary>
    public interface IMenuDataProvider
    {
        /// <summary>
        /// Classes added to top level menu
        /// </summary>
        /// <returns></returns>
        string MenuClass { get; }
        /// <summary>
        /// Classes added to menu items
        /// </summary>
        /// <returns></returns>
        string MenuItemClass { get; }
        /// <summary>
        /// Classes added to divider inside dropdown
        /// </summary>
        /// <returns></returns>
        string DividerClass { get; }
        /// <summary>
        /// Classes added to Group item (drowdown)
        /// </summary>
        /// <returns></returns>
        string GroupClass { get; }
        /// <summary>
        /// Classes added to links in the menu
        /// </summary>
        /// <returns></returns>
        string LinkClass { get; }
        /// <summary>
        /// Classes added to menu items considered active
        /// </summary>
        /// <returns></returns>
        string ActiveClass { get; }
        /// <summary>
        /// Classes for the toplevel nav element of the menu
        /// </summary>
        /// <returns></returns>
        string MenuContainerClass { get; }
        /// <summary>
        /// Classes to add to an open dropdown
        /// </summary>
        string DropdownClass { get; }
        /// <summary>
        /// True if the menugroup should also open on a mouse over, instead of just on a click
        /// </summary>
        /// <returns></returns>
        bool OpenGroupOnMouseOver { get; }
        /// <summary>
        /// If set and <see cref="OpenGroupOnMouseOver"/> returns true, will wait the set time before closing (for doing animations and stuff)
        /// </summary>
        /// <returns></returns>
        int GroupCloseTime { get; }
    }
}
