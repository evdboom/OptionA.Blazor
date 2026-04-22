using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace OptionA.Blazor.Playground;

/// <summary>
/// Adds playground services to the dependency injection container.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds a <see cref="IPlaygroundDataProvider"/> to the service collection for use in playground components.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configuration">Optional playground configuration.</param>
    /// <param name="lifetime">The service lifetime to register.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance.</returns>
    /// <exception cref="NotSupportedException">Thrown when the requested lifetime is not supported.</exception>
    public static IServiceCollection AddOptionAPlayground(this IServiceCollection services, Action<PlaygroundOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        if (lifetime == ServiceLifetime.Singleton)
        {
            services.TryAddSingleton<IPlaygroundDataProvider>(provider => new PlaygroundDataProvider(configuration));
        }
        else if (lifetime == ServiceLifetime.Scoped)
        {
            services.TryAddScoped<IPlaygroundDataProvider>(provider => new PlaygroundDataProvider(configuration));
        }
        else
        {
            throw new NotSupportedException("Only Singleton and Scoped lifetimes are supported");
        }

        return services;
    }

    /// <summary>
    /// Adds a bootstrap-configured <see cref="IPlaygroundDataProvider"/> to the service collection.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configuration">Additional configuration applied after bootstrap defaults.</param>
    /// <param name="lifetime">The service lifetime to register.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddOptionABootstrapPlayground(this IServiceCollection services, Action<PlaygroundOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        var bootstrapConfig = (PlaygroundOptions options) =>
        {
            options.DefaultPlaygroundClass = "card";
            options.DefaultPreviewClass = "card-body";
            options.DefaultEditorClass = "card-body";
            options.DefaultCodeClass = "card-body bg-light";
            options.DefaultEditorLabelClass = "form-label";
            options.DefaultEditorInputClass = "form-control";
            options.DefaultEditorGroupClass = "fw-bold mb-2mt-3";

            configuration?.Invoke(options);
        };

        return AddOptionAPlayground(services, bootstrapConfig, lifetime);
    }
}
