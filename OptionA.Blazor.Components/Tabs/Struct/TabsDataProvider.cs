namespace OptionA.Blazor.Components.Tabs.Struct;

/// <summary>
/// Implementation of <see cref="ITabsDataProvider"/>
/// </summary>
public class TabsDataProvider : ITabsDataProvider
{
    private readonly TabsOptions _options;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    public TabsDataProvider(Action<TabsOptions>? configuration = null)
    {
        _options = new TabsOptions();
        configuration?.Invoke(_options);
    }
}
