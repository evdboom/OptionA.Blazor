namespace OptionA.Blazor.Components;

/// <summary>
/// Provides default classes and behavior for tabs
/// </summary>
public interface ITabsDataProvider
{
    /// <summary>
    /// Class for the container around the active child content
    /// </summary>
    string? ContainerClass { get; }
    /// <summary>
    /// Class to be added to active selected tab, will be added next to the active attribute
    /// </summary>
    string? ActiveTabClass { get; }
    /// <summary>
    /// Class to be added to the tab select elements
    /// </summary>
    string? TabClass { get; }
    /// <summary>
    /// Class to be added to the tab item, the &lt;li&gt; element
    /// </summary>
    string? TabItemClass { get; }    
    /// <summary>
    /// Class to be added to the header showing the tabs
    /// </summary>
    string? HeaderClass { get; }
}
