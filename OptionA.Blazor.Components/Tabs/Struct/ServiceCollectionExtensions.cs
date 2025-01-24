using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OptionA.Blazor.Components.Tabs.Struct;

namespace OptionA.Blazor.Components;

/// <summary>
/// Extensions for adding splitter
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds tabs to the serviceprovider
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionATabs(this IServiceCollection services, Action<TabsOptions>? configuration = null)
    {
        services.TryAddSingleton<ITabsDataProvider>(provider => new TabsDataProvider(configuration));

        return services;
    }

    /// <summary>
    /// Adds tabs to the serviceprovider using default Bootstrap 5.3 configuration
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionABootstrapTabs(this IServiceCollection services, Action<TabsOptions>? configuration = null)
    {
        services.TryAddSingleton<ITabsDataProvider>(provider => new TabsDataProvider(configuration));

        return services;
    }
}
