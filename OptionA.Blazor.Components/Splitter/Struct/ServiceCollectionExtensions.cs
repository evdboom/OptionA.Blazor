using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OptionA.Blazor.Components.Splitter.Struct;

namespace OptionA.Blazor.Components;

/// <summary>
/// Extensions for adding splitter
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds splitters to the serviceprovider
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="lifetime"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionASplitter(this IServiceCollection services, Action<SplitterOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        if (lifetime == ServiceLifetime.Singleton)
        {
            services.TryAddSingleton<ISplitterDataProvider>(provider => new SplitterDataProvider(configuration));
        }
        else if (lifetime == ServiceLifetime.Scoped)
        {
            services.TryAddScoped<ISplitterDataProvider>(provider => new SplitterDataProvider(configuration));
        }
        else
        {
            throw new NotSupportedException("Only Singleton and Scoped lifetimes are supported");
        }

        return services;
    }

    /// <summary>
    /// Adds splitters to the serviceprovider using default Bootstrap 5.3 configuration
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="lifetime"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionABootstrapSplitter(this IServiceCollection services, Action<SplitterOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        return AddOptionASplitter(services, configuration, lifetime);
    }
}
