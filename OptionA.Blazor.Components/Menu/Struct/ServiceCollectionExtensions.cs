using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OptionA.Blazor.Components.Menu.Struct;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Add menu components to the service collection
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a singleton <see cref="IMenuDataProvider"/> to the service collection for use in the OptAMenu component
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddOptionAMenu(this IServiceCollection services, Action<MenuOptions>? configuration = null)
        {
            services
                .TryAddSingleton<IMenuDataProvider>(provider => new MenuDataProvider(configuration));
            return services;
        }

        /// <summary>
        /// Adds a singleton <see cref="IMenuDataProvider"/> to the service collection for use in the OptAMenu component, prefilled with bootstrap (5.3) classes
        /// </summary>
        /// <param name="services"></param>
        /// <param name="darkMode"></param>
        /// <param name="configuration">Additional configuration to be set after applying bootstrap config</param>
        /// <returns></returns>
        public static IServiceCollection AddOptionABootstrapMenu(this IServiceCollection services, bool darkMode = false, Action<MenuOptions>? configuration = null)
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

            return AddOptionAMenu(services, bootstrapConfig);
        }
    }
}
