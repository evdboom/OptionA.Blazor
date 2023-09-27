using Microsoft.Extensions.DependencyInjection;
using OptionA.Blazor.Storage.Migrations;

namespace OptionA.Blazor.Storage
{
    /// <summary>
    /// Helper class for adding services to the servies collection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add a migration to the service collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterMigration<T>(this IServiceCollection services) where T : Migration
        {
            services.AddSingleton<Migration, T>();
            return services;
        }
    }
}
