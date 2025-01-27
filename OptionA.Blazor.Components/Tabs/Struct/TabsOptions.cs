namespace OptionA.Blazor.Components;

/// <summary>
///  Options for the splitter component
/// </summary>
public class TabsOptions
{
    /// <summary>
    /// Class for the container around the active child content
    /// </summary>
    public string? ContainerClass { get; set; }
    /// <summary>
    /// Class to be added to active selected tab, will be added next to the active attribute
    /// </summary>
    public string? ActiveTabClass { get; set; }
    /// <summary>
    /// Class to be added to the tab select elements
    /// </summary>
    public string? TabClass { get; set; }
    /// <summary>
    /// Class to be added to the tab item, the &lt;li&gt; element
    /// </summary>
    public string? TabItemClass { get; set; }
    /// <summary>
    /// Class to be added to the header showing the tabs
    /// </summary>
    public string? HeaderClass { get; set; }
}
