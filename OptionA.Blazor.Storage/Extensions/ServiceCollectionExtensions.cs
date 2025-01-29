using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OptionA.Blazor.Storage.FileServiceMigrations;
using OptionA.Blazor.Storage.Migrations;
using OptionA.Blazor.Storage.Services;

namespace OptionA.Blazor.Storage;

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
    /// <para><see cref="ServiceLifetime.Scoped"/> <see cref="IDatabaseService"/> for storing and retrieving from indexed db</para>
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddDatabaseService(this IServiceCollection services)
    {
        services
            .AddScoped<IDatabaseService, DatabaseService>()
            .TryAddSingleton<MigrationBuilder>();

        return services;
    }

    /// <summary>
    /// Adds the following services to the container:
    /// <para><see cref="ServiceLifetime.Scoped"/> <see cref="IFileSystem"/> for file system access using the browser</para>
    /// <para><see cref="ServiceLifetime.Scoped"/> <see cref="IDatabaseService"/> for storing filehandles</para>
    /// <para><see cref="ServiceLifetime.Scoped"/> <see cref="IStorageService"/> for session id</para>
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddStorageServices(this IServiceCollection services)
    {
        return services
            .AddDatabaseService()
            .AddStorageService()
            .AddScoped<IFileSystem, FileSystem>()
            .RegisterMigration<M001_FileSystem_Initialize>();
    }
}
