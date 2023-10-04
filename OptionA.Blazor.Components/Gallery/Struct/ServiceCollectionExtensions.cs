using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OptionA.Blazor.Components.Gallery.Struct;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Extension methods for adding carousel
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds gallery with these default classes to the serviceprovider
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddOptionAGallery(this IServiceCollection services, Action<GalleryOptions>? configuration = null)
        {
            services
                .TryAddSingleton<IGalleryDataProvider>(provider => new GalleryDataProvider(configuration));

            return services;
        }

        /// <summary>
        /// Adds default bootstrap (5.3) classes and icon for use of gallery
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">Additional configuration to be applied after setting bootstrap config</param>
        /// <returns></returns>
        public static IServiceCollection AddOptionABootstrapGallery(this IServiceCollection services, Action<GalleryOptions>? configuration = null)
        {
            var bootstrapConfig = (GalleryOptions options) =>
            {
                options.NextClasses = "carousel-control-next";
                options.NextIconClasses = "carousel-control-next-icon";
                options.PreviousClasses = "carousel-control-prev";
                options.PreviousIconClasses = "carousel-control-prev-icon";
                options.ModalCloseButtonClasses = "btn-close me-2 mt-2";
                options.ModalBackgroundClasses = "modal-backdrop show";
                options.ModalWrapperClasses = "modal";
                options.ModalClasses = "modal-content p-3";

                configuration?.Invoke(options);
            };

            return AddOptionAGallery(services, bootstrapConfig);
        }
    }
}
