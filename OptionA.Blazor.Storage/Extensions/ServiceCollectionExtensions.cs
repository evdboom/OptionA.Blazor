using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OptionA.Blazor.Storage.Migrations;
using OptionA.Blazor.Storage.Services;

namespace OptionA.Blazor.Storage
{
    /// <summary>
    /// Helper class for registering services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the following services to the container:
        /// <para><see cref="ServiceLifetime.Scoped"/> <see cref="IStorageService"/> for storing and retrieving from local and or session storage</para>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddStorageService(this IServiceCollection services)
        {
            services
                .TryAddScoped<IStorageService, StorageService>();

            return services;
        }

        /// <summary>
        /// Adds the following services to the container:        
        /// <para><see cref="ServiceLifetime.Scoped"/> <see cref="IDatabaseService"/> for storing and retrieving from indexed db for the database of the given config <see cref="StorageOptions"/></para>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="optionsKey"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration configuration, string optionsKey)
        {
            services
                .AddScoped<IDatabaseService, DatabaseService>()
                .Configure<StorageOptions>(options => configuration.Bind(optionsKey, options))
                .TryAddSingleton<MigrationBuilder>();

            return services;
        }
    }
}
