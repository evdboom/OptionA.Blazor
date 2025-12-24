using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OptionA.Blazor.Components.Carousel.Struct;

namespace OptionA.Blazor.Components;

/// <summary>
/// Extension methods for adding carousel
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds carousels with these default classes to the serviceprovider
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="lifetime"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionACarousel(this IServiceCollection services, Action<CarouselOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        if (lifetime == ServiceLifetime.Singleton)
        {
            services
                .TryAddSingleton<ICarouselDataProvider>(provider => new CarouselDataProvider(configuration));
        }
        else if (lifetime == ServiceLifetime.Scoped)
        {
            services
                .TryAddScoped<ICarouselDataProvider>(provider => new CarouselDataProvider(configuration));
        }
        else
        {
            throw new NotSupportedException("Only Singleton and Scoped lifetimes are supported");
        }

        return services;
    }

    /// <summary>
    /// Adds default bootstrap (5.3) classes and icon for use of carousel
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration">Additional configuration to be applied after setting bootstrap config</param>
    /// <param name="lifetime"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionABootstrapCarousel(this IServiceCollection services, Action<CarouselOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
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

        return AddOptionACarousel(services, bootstrapConfig, lifetime);
    }
}
