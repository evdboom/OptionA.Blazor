using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OptionA.Blazor.Components.Tabs.Struct;

namespace OptionA.Blazor.Components;

/// <summary>
/// Extensions for adding tabs
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds tabs to the serviceprovider
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="lifetime"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionATabs(this IServiceCollection services, Action<TabsOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        if (lifetime == ServiceLifetime.Singleton)
        {
            services.TryAddSingleton<ITabsDataProvider>(provider => new TabsDataProvider(configuration));
        }
        else if (lifetime == ServiceLifetime.Scoped)
        {
            services.TryAddScoped<ITabsDataProvider>(provider => new TabsDataProvider(configuration));
        }
        else
        {
            throw new NotSupportedException("Only Singleton and Scoped lifetimes are supported");
        }

        return services;
    }

    /// <summary>
    /// Adds tabs to the serviceprovider using default Bootstrap 5.3 configuration
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="lifetime"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionABootstrapTabs(this IServiceCollection services, Action<TabsOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        void bootstrapConfig(TabsOptions options)
        {
            options.HeaderClass = "nav nav-tabs";
            options.ActiveTabClass = "active";
            options.TabItemClass = "nav-item";
            options.TabClass = "nav-link";

            configuration?.Invoke(options);
        }

        return AddOptionATabs(services, bootstrapConfig, lifetime);
    }
}
