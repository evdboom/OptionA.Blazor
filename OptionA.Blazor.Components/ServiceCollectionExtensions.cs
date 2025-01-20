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
    /// <returns></returns>
    public static IServiceCollection AddOptionAComponents(this IServiceCollection services, Action<OptAOptions>? configuration = null)
    {
        var options = new OptAOptions();
        configuration?.Invoke(options);

        return services
            .AddOptionAButtons(options.ButtonConfiguration)
            .AddOptionAMenu(options.MenuConfiguration)
            .AddOptionACarousel(options.CarouselConfiguration)
            .AddOptionAResponsive(options.ResponsiveConfiguration)
            .AddOptionAGallery(options.GalleryConfiguration)
            .AddOptionAModal(options.ModalConfiguration)
            .AddOptionASplitter(options.SplitterConfiguration)
            .AddOptionAMessageBox(options.MessageBoxConfiguration);
    }

    /// <summary>
    /// Adds all bootstrap (5.3) filled components to the service collection
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
            .AddOptionABootstrapButtons(options.ButtonConfiguration)
            .AddOptionABootstrapMenu(darkMode, options.MenuConfiguration)
            .AddOptionABootstrapCarousel(options.CarouselConfiguration)
            .AddOptionABootstrapResponsive(options.ResponsiveConfiguration)
            .AddOptionABootstrapGallery(options.GalleryConfiguration)
            .AddOptionABootstrapModal(options.ModalConfiguration)
            .AddOptionASplitter(options.SplitterConfiguration)
            .AddOptionABootstrapMessageBox(options.MessageBoxConfiguration);
    }


}
