using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OptionA.Blazor.Components.Message.Struct;

namespace OptionA.Blazor.Components;

public partial class ServiceCollectionExtensions
{

    /// <summary>
    /// Adds a singleton <see cref="IMessageBoxDataProvider"/> to the service collection for use in the OptAMessageBox component
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionAMessageBox(this IServiceCollection services, Action<MessageBoxOptions>? configuration = null)
    {
        services
            .TryAddSingleton<IMessageBoxDataProvider>(provider => new MessageBoxDataProvider(configuration));
        services
            .TryAddSingleton<IMessageService, MessageService>();
        return services;
    }

    /// <summary>
    /// Adds a singleton <see cref="IMenuDataProvider"/> to the service collection for use in the OptAMessageBox component, prefilled with bootstrap (5.3) classes
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration">Additional configuration to be set after applying bootstrap config</param>
    /// <returns></returns>
    public static IServiceCollection AddOptionABootstrapMessageBox(this IServiceCollection services, Action<MessageBoxOptions>? configuration = null)
    {
        void bootstrapConfig(MessageBoxOptions options)
        {
            options.DefaultZIndex = 1090;
            options.MessageClasses = new()
            {
                [MessageType.Info] = "toast show d-flex",
                [MessageType.Success] = "toast show text-bg-success bg-opacity-75 d-flex",
                [MessageType.Warning] = "toast show text-bg-warning bg-opacity-75 d-flex",
                [MessageType.Error] = "toast show text-bg-danger bg-opacity-75 d-flex"
            };
            options.TimeOuts = new()
            {
                [MessageType.Error] = Timeout.Infinite
            };
            options.CloseButtonClasses = new()
            {
                [MessageType.Success] = "btn-close btn-close-white me-2 mt-2 ms-auto",
                [MessageType.Error] = "btn-close btn-close-white me-2 mt-2 ms-auto"
            };
            options.ContainerClass = "toast-container";
            options.DefaultCloseButtonClass = "btn-close me-2 mt-2 ms-auto";
            options.HeaderClass = "pb-2 m-2 border-bottom d-flex justify-content-between";
            options.BodyClass = "toast-body";
            options.ContentClass = "w-100";

            configuration?.Invoke(options);
        }

        return AddOptionAMessageBox(services, bootstrapConfig);
    }
}
