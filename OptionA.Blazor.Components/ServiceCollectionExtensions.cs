using Microsoft.Extensions.DependencyInjection;

namespace OptionA.Blazor.Components;

/// <summary>
/// Extensions for adding all components;
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all component dataproviders to the service collection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="lifetime"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionAComponents(this IServiceCollection services, Action<OptAOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        var options = new OptAOptions();
        configuration?.Invoke(options);

        return services
            .AddOptionAButtons(options.ButtonConfiguration, lifetime)
            .AddOptionAMenu(options.MenuConfiguration, lifetime)
            .AddOptionACarousel(options.CarouselConfiguration, lifetime)
            .AddOptionAResponsive(options.ResponsiveConfiguration, lifetime)
            .AddOptionAGallery(options.GalleryConfiguration, lifetime)
            .AddOptionAModal(options.ModalConfiguration, lifetime)
            .AddOptionASplitter(options.SplitterConfiguration, lifetime)
            .AddOptionAMessageBox(options.MessageBoxConfiguration, lifetime)
            .AddOptionATabs(options.TabsConfiguration, lifetime);
    }

    /// <summary>
    /// Adds all bootstrap (5.3) filled components to the service collection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="darkMode"></param>
    /// <param name="configuration">Additional config to be set after applying bootstrap configs</param>
    /// <param name="lifetime"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionABootstrapComponents(this IServiceCollection services, bool darkMode = false, Action<OptAOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        var options = new OptAOptions();
        configuration?.Invoke(options);

        return services
            .AddOptionABootstrapButtons(options.ButtonConfiguration, lifetime)
            .AddOptionABootstrapMenu(darkMode, options.MenuConfiguration, lifetime)
            .AddOptionABootstrapCarousel(options.CarouselConfiguration, lifetime)
            .AddOptionABootstrapResponsive(options.ResponsiveConfiguration, lifetime)
            .AddOptionABootstrapGallery(options.GalleryConfiguration, lifetime)
            .AddOptionABootstrapModal(options.ModalConfiguration, lifetime)
            .AddOptionABootstrapSplitter(options.SplitterConfiguration, lifetime)
            .AddOptionABootstrapMessageBox(options.MessageBoxConfiguration, lifetime)
            .AddOptionABootstrapTabs(options.TabsConfiguration, lifetime);
    }


}
