using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionA.Blazor.Components.Carousel.Struct
{
    /// <summary>
    /// Extension methods for adding carousel
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds carousels with these default classes to the serviceprovider
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCarouselClasses(this IServiceCollection services, Action<CarouselOptions>? configuration = null)            
        {
            return services
                .AddSingleton<ICarouselDataProvider>(provider => new CarouselDataProvider(configuration));             
        }

        /// <summary>
        /// Adds default bootstrap classes and icon for use of carousel
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">Additional configuration to be applied after setting bootstrap config</param>
        /// <returns></returns>
        public static IServiceCollection AddBootstrapCarousel(this IServiceCollection services, Action<CarouselOptions>? configuration = null)
        {
            var bootstrapConfig = (CarouselOptions options) =>
            {
                options.ItemSelectClasses = "carousel-indicators";
                options.ItemSelectAttributes = new()
                {
                    { "data-bs-target", string.Empty }
                };
                options.ActiveItemSelectClasses = "active";
                options.NextClasses = "carousel-control-next";
                options.NextIconClasses = "carousel-control-next-icon";
                options.PreviousClasses = "carousel-control-prev";
                options.PreviousIconClasses = "carousel-control-prev-icon";

                configuration?.Invoke(options);                
            };

            return AddCarouselClasses(services, bootstrapConfig);
        }
    }
}
