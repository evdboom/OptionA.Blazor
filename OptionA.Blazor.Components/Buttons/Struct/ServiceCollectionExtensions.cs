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
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddButtonClasses(this IServiceCollection services, Action<ButtonOptions>? configuration = null)
        {
            return services
                .AddSingleton<IButtonDataProvider>(provider => new ButtonDataProvider(configuration);            
        }

        /// <summary>
        /// Adds a singleton <see cref="IButtonDataProvider"/> to the service collection for use in the OptAButtons, prefilled with bootstrap classes
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">Additional configuration to be applied after setting bootstrap config</param>
        /// <returns></returns>
        public static IServiceCollection AddBootstrapButtons(this IServiceCollection services, Action<ButtonOptions>? configuration = null)
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

            var bootstrapConfig = (ButtonOptions options) =>
            {
                options.DefaultButtonClass = "btn btn-primary";
                options.DefaultIconClass = "bi bi-circle";
                options.ButtonClasses = buttonClasses;
                options.IconClasses = iconClasses;

                configuration?.Invoke(options);
            };

            return AddButtonClasses(services, bootstrapConfig);
        }
    }
}
