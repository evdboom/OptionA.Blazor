using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OptionA.Blazor.Components.Modal.Struct;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Extension methods for adding carousel
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds modal with these default classes to the serviceprovider
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddOptionAModal(this IServiceCollection services, Action<ModalOptions>? configuration = null)
        {
            services.TryAddSingleton<IModalDataProvider>(provider => new ModalDataProvider(configuration));

            return services;
        }


        /// <summary>
        /// Adds default bootstrap (5.3) classes for use in modal
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">Additional configuration to be applied after setting bootstrap config</param>
        /// <returns></returns>
        public static IServiceCollection AddOptionABootstrapModal(this IServiceCollection services, Action<ModalOptions>? configuration = null)
        {            
            var bootstrapConfig = (ModalOptions options) =>
            {
                options.DialogClass = "opta-bs-modal modal-dialog";
                options.SectionClass = "modal-content";
                options.HeaderClass = "modal-header";
                options.ContentClass = "modal-body";
                options.FooterClass = "modal-footer";
                options.CloseButtonClass = "btn-close";
                options.SizeClasses = new Dictionary<ModalSize, string>
                {
                    [ModalSize.Small] = "modal-sm",
                    [ModalSize.Large] = "modal-lg",
                    [ModalSize.ExtraLarge] = "modal-xl"
                };

                configuration?.Invoke(options);
            };

            return AddOptionAModal(services, bootstrapConfig);
        }
    }
}
