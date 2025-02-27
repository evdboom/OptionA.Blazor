﻿namespace OptionA.Blazor.Components;

/// <summary>
/// Options to set for menu components
/// </summary>
public class MenuOptions
{        
    /// <summary>
    /// Default classes for menu
    /// </summary>
    public string? DefaultMenuClass { get; set; }
    /// <summary>
    /// Default classes for menu items
    /// </summary>
    public string? DefaultMenuItemClass { get; set; }
    /// <summary>
    /// Default classes for links in menu
    /// </summary>
    public string? DefaultMenuLinkClass { get; set; }
    /// <summary>
    /// Default classes for menu groups
    /// </summary>
    public string? DefaultMenuGroupClass { get; set; }
    /// <summary>
    /// Default classes for the divider in a menu group
    /// </summary>
    public string? DefaultMenuDividerClass { get; set; }
    /// <summary>
    /// Class to add to active link
    /// </summary>
    public string? ActiveClass { get; set; }
    /// <summary>
    /// Classes to add to top level container
    /// </summary>
    public string? DefaultMenuContainerClass { get; set; }
    /// <summary>
    /// True if the menu group should open on a mouse over and a click.
    /// </summary>
    public bool? OpenGroupOnMouseOver { get; set; }
    /// <summary>
    /// If set will wait the set time before closing (for doing animations and stuff)
    /// </summary>
    public int? GroupCloseTime { get; set; }
    /// <summary>
    /// Classes to add to an open dropdown
    /// </summary>
    public string? DefaultDropdownClass { get; set; }
}
