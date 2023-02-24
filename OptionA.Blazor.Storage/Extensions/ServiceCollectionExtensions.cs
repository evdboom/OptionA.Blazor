using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OptionA.Blazor.Storage.Interfaces;
using OptionA.Blazor.Storage.Services;
using OptionA.Blazor.Storage.Utilities;
using OptionA.Storage.Options;

namespace OptionA.Blazor.Storage.Extensions
{
    /// <summary>
    /// Helper class for registering services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the following services to the container:
        /// <para><see cref="ServiceLifetime.Scoped"/> <see cref="IStorageService"/> for storing and retrieving from local and or session storage</para>
        /// <para><see cref="ServiceLifetime.Scoped"/> <see cref="IDatabaseService"/> for storing and retrieving from indexed db</para>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="optionsKey"></param>
        /// <returns></returns>
        public static IServiceCollection AddStorageServices(this IServiceCollection services, IConfiguration configuration, string optionsKey)
        {
            services
                .AddScoped<IStorageService, StorageService>()
                .AddScoped<IDatabaseService, DatabaseService>()
                .AddSingleton<MigrationBuilder>()
                .Configure<StorageOptions>(options => configuration.Bind(optionsKey, options));

            return services;
        }
    }
}
