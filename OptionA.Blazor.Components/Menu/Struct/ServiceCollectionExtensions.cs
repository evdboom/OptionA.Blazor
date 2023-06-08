using Microsoft.Extensions.DependencyInjection;
using OptionA.Blazor.Components.Buttons.Enum;
using OptionA.Blazor.Components.Buttons.Struct;

namespace OptionA.Blazor.Components.Menu.Struct
{
    /// <summary>
    /// Add menu components to the service collection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a singleton <see cref="IMenuDataProvider"/> to the service collection for use in the OptAMenu component
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddMenuClasses(this IServiceCollection services, Action<MenuOptions>? configuration = null)
        {
            return services
                .AddSingleton<IMenuDataProvider>(provider => new MenuDataProvider(configuration));            
        }

        /// <summary>
        /// Adds a singleton <see cref="IMenuDataProvider"/> to the service collection for use in the OptAMenu component, prefilled with bootstrap classes
        /// </summary>
        /// <param name="services"></param>
        /// <param name="darkMode"></param>
        /// <param name="configuration">Additional configuration to be set after applying bootstrap config</param>
        /// <returns></returns>
        public static IServiceCollection AddBootstrapMenu(this IServiceCollection services, bool darkMode = false, Action<MenuOptions>? configuration = null)
        {
            var mode = darkMode
                ? "navbar-dark"
                : "navbar-light";

            var bootstrapConfig = (MenuOptions options) =>
            {
                options.DefaultMenuClass = "navbar-nav";
                options.DefaultMenuItemClass = "nav-item me-2";
                options.DefaultMenuLinkClass = "nav-link";
                options.DefaultMenuGroupClass = "nav-link dropdown-toggle";
                options.DefaultMenuDividerClass = "dropdown-divider";
                options.ActiveClass = "active";
                options.DefaultMenuContainerClass = $"navbar {mode}";

                configuration?.Invoke(options);
            };

            return AddMenuClasses(services, bootstrapConfig);
        }
    }
}
