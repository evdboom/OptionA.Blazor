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
        /// <param name="defaultMenuClass"></param>
        /// <param name="defaultMenuItemClass"></param>
        /// <param name="defaultMenuLinkClass"></param>
        /// <param name="defaultMenuGroupClass"></param>
        /// <param name="defaultMenuDividerClass"></param>
        /// <param name="activeClass"></param>
        /// <param name="defaultMenuContainerClass"></param>
        /// <returns></returns>
        public static IServiceCollection AddMenuClasses(
            this IServiceCollection services,            
            string defaultMenuClass,            
            string defaultMenuItemClass,
            string defaultMenuLinkClass,
            string defaultMenuGroupClass,
            string defaultMenuDividerClass,
            string activeClass,
            string defaultMenuContainerClass)
        {
            services.AddSingleton<IMenuDataProvider>(provider => new MenuDataProvider(defaultMenuClass, defaultMenuItemClass, defaultMenuLinkClass, defaultMenuGroupClass, defaultMenuDividerClass, activeClass, defaultMenuContainerClass));
            return services;
        }

        /// <summary>
        /// Adds a singleton <see cref="IMenuDataProvider"/> to the service collection for use in the OptAMenu component, prefilled with bootstrap classes
        /// </summary>
        /// <param name="services"></param>
        /// <param name="darkMode"></param>
        /// <returns></returns>
        public static IServiceCollection AddBootstrapMenu(this IServiceCollection services, bool darkMode = false)
        {
            var mode = darkMode
                ? "navbar-dark"
                : "navbar-light";
            return AddMenuClasses(services, "navbar-nav", "nav-item me-2", "nav-link", "nav-link dropdown-toggle", "dropdown-divider", "active", $"navbar {mode}");
        }
    }
}
