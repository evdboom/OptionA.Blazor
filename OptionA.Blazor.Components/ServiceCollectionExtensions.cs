using Microsoft.Extensions.DependencyInjection;
using OptionA.Blazor.Components.Buttons.Struct;
using OptionA.Blazor.Components.Carousel.Struct;
using OptionA.Blazor.Components.Menu.Struct;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Extensions for adding all components;
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all component dataproviders to the service collection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddOptionAComponents(this IServiceCollection services, Action<OptAOptions>? configuration = null)
        {
            var options = new OptAOptions();
            configuration?.Invoke(options);

            return services
                .AddButtonClasses(options.ButtonConfiguration)
                .AddMenuClasses(options.MenuConfiguration)
                .AddCarouselClasses(options.CarouselConfiguration);
        }

        /// <summary>
        /// Adds all bootstrap filled components to the service collection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="darkMode"></param>
        /// <param name="configuration">Additional config to be set after applying bootstrap configs</param>
        /// <returns></returns>
        public static IServiceCollection AddOptionABootstrapComponents(this IServiceCollection services, bool darkMode = false, Action<OptAOptions>? configuration = null)
        {
            var options = new OptAOptions();
            configuration?.Invoke(options);

            return services
                .AddBootstrapButtons(options.ButtonConfiguration)
                .AddBootstrapMenu(darkMode, options.MenuConfiguration)
                .AddBootstrapCarousel(options.CarouselConfiguration);
        }


    }
}
