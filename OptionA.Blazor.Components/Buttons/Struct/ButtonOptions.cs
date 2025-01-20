namespace OptionA.Blazor.Components;

/// <summary>
/// Options for ButtonDataProvider
/// </summary>
public class ButtonOptions
{
    /// <summary>
    /// Button classes for the variying actiontypes
    /// </summary>
    public Dictionary<ActionType, string>? ButtonClasses { get; set; }
    /// <summary>
    /// Button class to use on no matched actiontype
    /// </summary>
    public string? DefaultButtonClass { get; set; }
    /// <summary>
    /// Icon cloasses to use for the different actiontypes
    /// </summary>
    public Dictionary<ActionType, string>? IconClasses { get; set; }
    /// <summary>
    /// Icon class to use if no specific is found
    /// </summary>
    public string? DefaultIconClass { get; set; }
    /// <summary>
    /// Class to add to button bar
    /// </summary>
    public string? DefaultButtonBarClass { get; set; }
    /// <summary>
    /// Class to add to group of buttons in button bar
    /// </summary>
    public string? DefaultButtonGroupClass { get; set; }
}
