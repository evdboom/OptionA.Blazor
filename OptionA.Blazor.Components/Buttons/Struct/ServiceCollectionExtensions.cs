using Microsoft.Extensions.DependencyInjection;
using OptionA.Blazor.Components.Buttons.Enum;

namespace OptionA.Blazor.Components.Buttons.Struct
{
    /// <summary>
    /// Add buttons to the service collection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a singleton <see cref="IButtonDataProvider"/> to the service collection for use in the OptAButtons
        /// </summary>
        /// <param name="services"></param>
        /// <param name="buttonClasses"></param>
        /// <param name="defaultButtonClass"></param>
        /// <param name="iconClasses"></param>
        /// <param name="defaultIconClass"></param>
        /// <returns></returns>
        public static IServiceCollection AddButtonClasses(
            this IServiceCollection services,
            IDictionary<ActionType, string> buttonClasses,
            string defaultButtonClass,
            IDictionary<ActionType, string> iconClasses,
            string defaultIconClass)
        {
            services.AddSingleton<IButtonDataProvider>(provider => new ButtonDataProvider(buttonClasses, defaultButtonClass, iconClasses, defaultIconClass));
            return services;
        }

        /// <summary>
        /// Adds a singleton <see cref="IButtonDataProvider"/> to the service collection for use in the OptAButtons, prefilled with bootstrap classes
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddBootstrapButtons(this IServiceCollection services)
        {
            var buttonClasses = new Dictionary<ActionType, string>
            {
                { ActionType.Add, "btn btn-success" },
                { ActionType.Remove, "btn btn-danger" },
                { ActionType.Cancel, "btn btn-secondary" },
            };
            var iconClasses = new Dictionary<ActionType, string>
            {
                { ActionType.Add, "bi bi-plus-lg" },
                { ActionType.Remove, "bi bi-trash" },
                { ActionType.Edit, "bi bi-pencil" },
                { ActionType.Search, "bi bi-search" },
                { ActionType.Refresh, "bi bi-arrow-repeat" },
                { ActionType.Cancel, "bi bi-x-lg" },
                { ActionType.Confirm, "bi bi-check-lg" },
            };
            return AddButtonClasses(services, buttonClasses, "btn btn-primary", iconClasses, "bi bi-circle");
        }
    }
}
