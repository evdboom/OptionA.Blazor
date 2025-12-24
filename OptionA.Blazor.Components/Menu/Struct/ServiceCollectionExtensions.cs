using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OptionA.Blazor.Components.Menu.Struct;

namespace OptionA.Blazor.Components;

/// <summary>
/// Add menu components to the service collection
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds a <see cref="IMenuDataProvider"/> to the service collection for use in the OptAMenu component with the provided configuration and lifetime
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="lifetime"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionAMenu(this IServiceCollection services, Action<MenuOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        if (lifetime == ServiceLifetime.Singleton)
        {
            services
                .TryAddSingleton<IMenuDataProvider>(provider => new MenuDataProvider(configuration));
        }
        else if (lifetime == ServiceLifetime.Scoped)
        {
            services
                .TryAddScoped<IMenuDataProvider>(provider => new MenuDataProvider(configuration));
        }
        else
        {
            throw new NotSupportedException("Only Singleton and Scoped lifetimes are supported");
        }

        return services;
    }

    /// <summary>
    /// Adds a <see cref="IMenuDataProvider"/> to the service collection for use in the OptAMenu component, prefilled with bootstrap (5.3) classes with the provided configuration and lifetime
    /// </summary>
    /// <param name="services"></param>
    /// <param name="darkMode"></param>
    /// <param name="configuration">Additional configuration to be set after applying bootstrap config</param>
    /// <param name="lifetime"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionABootstrapMenu(this IServiceCollection services, bool darkMode = false, Action<MenuOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        var mode = darkMode
            ? "navbar-dark"
            : "navbar-light";

        var bootstrapConfig = (MenuOptions options) =>
        {
            options.DefaultMenuClass = "navbar-nav";
            options.DefaultMenuItemClass = "nav-item";
            options.DefaultMenuLinkClass = "nav-link";
            options.DefaultMenuGroupClass = "nav-link dropdown-toggle";
            options.DefaultMenuDividerClass = "dropdown-divider";
            options.ActiveClass = "active";
            options.DefaultMenuContainerClass = $"navbar {mode}";

            configuration?.Invoke(options);
        };

        return AddOptionAMenu(services, bootstrapConfig, lifetime);
    }
}
