using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OptionA.Blazor.Components.Responsive.Struct;
using OptionA.Blazor.Components.Services;

namespace OptionA.Blazor.Components;

/// <inheritdoc/>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds a <see cref="IResponsiveService"/> to the service collection, after which it can be injected or used through <see cref="OptAResponsive"/> component with the provided configuration options and lifetime
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="lifetime"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionAResponsive(this IServiceCollection services, Action<ResponsiveOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        if (lifetime == ServiceLifetime.Singleton)
        {
            services
                .TryAddSingleton<IResponsiveDataProvider>(provider => new ResponsiveDataProvider(configuration));
            services
                .TryAddSingleton<IResponsiveService, ResponsiveService>();
        }
        else if (lifetime == ServiceLifetime.Scoped)
        {
            services
                .TryAddScoped<IResponsiveDataProvider>(provider => new ResponsiveDataProvider(configuration));
            services
                .TryAddScoped<IResponsiveService, ResponsiveService>();
        }
        else
        {
            throw new NotSupportedException("Only Singleton and Scoped lifetimes are supported");
        }

        return services;
    }

    /// <summary>
    /// Adds a <see cref="IResponsiveService"/> to the service collection, with the thresholds filled with the bootstrap thresholds, after which it can be injected or used through <see cref="OptAResponsive"/> component with the provided configuration options and lifetime
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="lifetime"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionABootstrapResponsive(this IServiceCollection services, Action<ResponsiveOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        var bootstrapConfig = (ResponsiveOptions options) =>
        {
            options.Sizes = new()
            {
                { 0, "ExtraSmall" },
                { 576, "Small"},
                { 768, "Medium" },
                { 992, "Large" },
                { 1200, "ExtraLarge" },
                { 1400, "ExtraExtraLarge" }
            };

            configuration?.Invoke(options);
        };

        return AddOptionAResponsive(services, bootstrapConfig, lifetime);
    }
}
