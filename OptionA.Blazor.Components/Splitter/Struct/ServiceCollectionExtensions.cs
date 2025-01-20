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
    /// <returns></returns>
    public static IServiceCollection AddOptionASplitter(this IServiceCollection services, Action<SplitterOptions>? configuration = null)
    {
        services.TryAddSingleton<ISplitterDataProvider>(provider => new SplitterDataProvider(configuration));

        return services;
    }
}
