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

    /// <inheritdoc/>
    public string? ContainerClass => _options.ContainerClass;
    /// <inheritdoc/>
    public string? ActiveTabClass => _options.ActiveTabClass;
    /// <inheritdoc/>
    public string? TabClass => _options.TabClass;
    /// <inheritdoc/>
    public string? HeaderClass => _options.HeaderClass;
    /// <inheritdoc/>
    public string? TabItemClass => _options.TabItemClass;
}
