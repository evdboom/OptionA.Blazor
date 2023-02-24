using Microsoft.Extensions.DependencyInjection;
using LandaPacs.Storage.Migrations;

namespace LandaPacs.Storage.Contracts.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterMigration<T>(this IServiceCollection services) where T : Migration
        {
            services.AddSingleton<Migration, T>();
            return services;
        }
    }
}
