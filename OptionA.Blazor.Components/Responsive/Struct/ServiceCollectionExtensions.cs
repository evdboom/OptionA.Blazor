using Microsoft.Extensions.DependencyInjection;
using OptionA.Blazor.Components.Services;

namespace OptionA.Blazor.Components
{
    /// <inheritdoc/>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a Singleton <see cref="IResponsiveService"/> to the service collection, after which it can be injected or used through <see cref="OptAResponsive"/> component
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddOptionAResponsive(this IServiceCollection services, Action<ResponsiveOptions>? configuration = null)
        {
            return services
                .Configure<ResponsiveOptions>(options => configuration?.Invoke(options))
                .AddSingleton<IResponsiveService, ResponsiveService>();

        }

        /// <summary>
        /// Adds a Singleton <see cref="IResponsiveService"/> to the service collection, with the thresholds filled with the bootstrap thresholds, after which it can be injected or used through <see cref="OptAResponsive"/> component
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddOptionABootstrapResponsive(this IServiceCollection services, Action<ResponsiveOptions>? configuration = null)
        {
            var bootstrapConfig = (ResponsiveOptions options) =>
            {
                options.Sizes = new()
                {
                    { 0, "ExtraSmall" },
                    { 576, "Small"},
                    { 768, "Medium" },
                    { 992, "Large" },
                    { 1200, "ExtraLarge" },
                    { 1400, "ExtraExtraLarge" }
                };

                configuration?.Invoke(options);
            };

            return AddOptionAResponsive(services, bootstrapConfig);
        }
    }
}
