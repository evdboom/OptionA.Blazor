using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OptionA.Blazor.Interactive.Infrastructure;
using OptionA.Blazor.Interactive.Interfaces;
using OptionA.Blazor.Playground;

namespace OptionA.Blazor.Interactive;

/// <summary>
/// Adds interactive documentation services to the dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the interactive provider and playground bridge to the service collection.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configuration">Optional interactive configuration.</param>
    /// <param name="lifetime">The service lifetime to register.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance.</returns>
    /// <exception cref="NotSupportedException">Thrown when the requested lifetime is not supported.</exception>
    public static IServiceCollection AddOptionAInteractive(this IServiceCollection services, Action<InteractiveOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        if (lifetime == ServiceLifetime.Singleton)
        {
            services.TryAddSingleton<IInteractiveDataProvider>(provider => new InteractiveDataProvider(configuration));
            services.TryAddSingleton<IPlaygroundDataProvider>(provider => (IPlaygroundDataProvider)provider.GetRequiredService<IInteractiveDataProvider>());
        }
        else if (lifetime == ServiceLifetime.Scoped)
        {
            services.TryAddScoped<IInteractiveDataProvider>(provider => new InteractiveDataProvider(configuration));
            services.TryAddScoped<IPlaygroundDataProvider>(provider => (IPlaygroundDataProvider)provider.GetRequiredService<IInteractiveDataProvider>());
        }
        else
        {
            throw new NotSupportedException("Only Singleton and Scoped lifetimes are supported");
        }

        return services;
    }

    /// <summary>
    /// Adds bootstrap-oriented defaults for the interactive package.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configuration">Additional configuration applied after bootstrap defaults.</param>
    /// <param name="lifetime">The service lifetime to register.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddOptionABootstrapInteractive(this IServiceCollection services, Action<InteractiveOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        var bootstrapConfig = (InteractiveOptions options) =>
        {
            options.DefaultInteractiveClass = "card";
            options.DefaultPlaygroundClass = "card";
            options.DefaultPreviewClass = "card-body";
            options.DefaultEditorClass = "card-body";
            options.DefaultCodeClass = "card-body bg-light";
            options.DefaultEditorLabelClass = "form-label";
            options.DefaultEditorInputClass = "form-control";
            options.DefaultEditorGroupClass = "fw-bold mb-2 mt-3";

            configuration?.Invoke(options);
        };

        return AddOptionAInteractive(services, bootstrapConfig, lifetime);
    }
}
