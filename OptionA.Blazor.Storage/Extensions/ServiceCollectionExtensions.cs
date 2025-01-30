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
    /// <para><see cref="IStorageService"/> with given <see cref="ServiceLifetime" /> for storing and retrieving from local and or session storage</para>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="serviceLifetime"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionAStorageService(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        switch (serviceLifetime)
        {
            case ServiceLifetime.Singleton:
                services
                    .TryAddSingleton<IStorageService, StorageService>();
                break;
            case ServiceLifetime.Transient:
                services
                    .TryAddTransient<IStorageService, StorageService>();
                break;
            case ServiceLifetime.Scoped:
                services
                    .TryAddScoped<IStorageService, StorageService>();
                break;
        }

        return services;
    }

    /// <summary>
    /// Adds the following services to the container:        
    /// <para> <see cref="IDatabaseService"/> with given <see cref="ServiceLifetime" /> for storing and retrieving from indexed db</para>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="serviceLifetime"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionADatabaseService(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        switch (serviceLifetime)
        {
            case ServiceLifetime.Singleton:
                services
                    .TryAddSingleton<IDatabaseService, DatabaseService>();
                break;
            case
            ServiceLifetime.Transient:
                services
                    .TryAddTransient<IDatabaseService, DatabaseService>();
                break;
            case ServiceLifetime.Scoped:
                services
                    .TryAddScoped<IDatabaseService, DatabaseService>();
            break;
        }
        services
            .TryAddSingleton<MigrationBuilder>();

        return services;
    }

    /// <summary>
    /// Adds the following services to the container:
    /// <para><see cref="IFileSystem"/> with given <see cref="ServiceLifetime" /> for file system access using the browser</para>
    /// <para><see cref="IDatabaseService"/> with given <see cref="ServiceLifetime" /> for storing filehandles</para>
    /// <para><see cref="IStorageService"/> with given <see cref="ServiceLifetime" /> for session id</para>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="serviceLifetime"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionAStorageServices(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        switch (serviceLifetime)
        {
            case ServiceLifetime.Singleton:
                services
                    .TryAddSingleton<IFileSystem, FileSystem>();
                break;
            case ServiceLifetime.Transient:
                services
                    .TryAddTransient<IFileSystem, FileSystem>();
                break;
            case ServiceLifetime.Scoped:
                services
                    .TryAddScoped<IFileSystem, FileSystem>();
                break;
        }

        return services
            .AddOptionADatabaseService(serviceLifetime)
            .AddOptionAStorageService(serviceLifetime)
            .RegisterMigration<M001_FileSystem_Initialize>();           
    }
}
