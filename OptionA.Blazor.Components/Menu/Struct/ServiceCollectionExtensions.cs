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
        /// <returns></returns>
        public static IServiceCollection AddMenuClasses(
            this IServiceCollection services,            
            string defaultMenuClass,            
            string defaultMenuItemClass,
            string defaultMenuLinkClass,
            string defaultMenuGroupClass,
            string defaultMenuDividerClass)
        {
            services.AddSingleton<IMenuDataProvider>(provider => new MenuDataProvider(defaultMenuClass, defaultMenuItemClass, defaultMenuLinkClass, defaultMenuGroupClass, defaultMenuDividerClass));
            return services;
        }

        /// <summary>
        /// Adds a singleton <see cref="IMenuDataProvider"/> to the service collection for use in the OptAMenu component, prefilled with bootstrap classes
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddBootstrapMenu(this IServiceCollection services)
        {
            return AddMenuClasses(services, "navbar-nav", "nav-item me-2", "nav-link", "nav-link dropdown-toggle", "dropdown-divider");
        }
    }
}
