using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using OptionA.Blazor.Blog.Code.Parsers;
using OptionA.Blazor.Blog.Document.Internal;
using OptionA.Blazor.Blog.Services;
using OptionA.Blazor.Blog.Struct;
using OptionA.Blazor.Blog.Text.Parser;
using OptionA.Blazor.Playground;
using System.Reflection;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Class for adding blog parts to the service collection
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all blogparts to the servicecollection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="lifetime"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionABlog(this IServiceCollection services, Action<OptABlogOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        if (lifetime == ServiceLifetime.Singleton)
        {
            services
                .AddSingleton<IBuilderService, BuilderService>()
                .AddSingleton<IMarkDownParser, MarkDownParser>()
                .AddSingleton<IMarkdownDocumentParser>(sp =>
                    new MarkdownDocumentParser(
                        sp.GetService<IPlaygroundDescriptorResolver>(),
                        sp.GetService<IDocumentComponentRegistry>()))
                .AddSingleton<IBlogDataProvider>(provider => new BlogDataProvider(configuration));
        }
        else if (lifetime == ServiceLifetime.Scoped)
        {
            services
                .AddScoped<IBuilderService, BuilderService>()
                .AddScoped<IMarkDownParser, MarkDownParser>()
                .AddScoped<IMarkdownDocumentParser>(sp =>
                    new MarkdownDocumentParser(
                        sp.GetService<IPlaygroundDescriptorResolver>(),
                        sp.GetService<IDocumentComponentRegistry>()))
                .AddScoped<IBlogDataProvider>(provider => new BlogDataProvider(configuration));
        }
        else
        {
            throw new NotSupportedException("Only Singleton and Scoped lifetimes are supported");
        }

        var interfaces = new[]
        {
            typeof(IMarkerDefinition),
            typeof(ICodeParser)
        };

        var types = Assembly
            .GetAssembly(typeof(BoldMarker))?
            .GetExportedTypes()
            .Where(type => !type.IsAbstract && type.IsClass)
            .Select(type => (Type: type, Interface: GetValidInterface(type, interfaces)))
            .Where(type => type.Interface is not null)
            .ToList();

        if (types is null)
        {
            return services;
        }

        foreach (var type in types)
        {
            if (lifetime == ServiceLifetime.Singleton)
            {
                services
                    .AddSingleton(type.Interface!, type.Type);
            }
            else if (lifetime == ServiceLifetime.Scoped)
            {
                services
                    .AddScoped(type.Interface!, type.Type);
            }
        }

        return services;
    }

    /// <summary>
    /// Adds all blogparts to the servicecollection, prefilled with bootstrap (5.3) classes
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration">Additional configuration to be applied after setting bootstrap config</param>
    /// <param name="lifetime"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionABootstrapBlog(this IServiceCollection services, Action<OptABlogOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        var bootstrapConfig = (OptABlogOptions options) =>
        {
            configuration?.Invoke(options);
        };

        return AddOptionABlog(services, bootstrapConfig, lifetime);
    }

    /// <summary>
    /// Registers a Blazor component type in the document-component whitelist so that
    /// literal <c>&lt;OptA*&gt;</c> tags of the matching name in Markdown source are rendered
    /// via <see cref="Microsoft.AspNetCore.Components.DynamicComponent"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The Blazor component type to whitelist. The tag name is derived from <c>typeof(T).Name</c>
    /// (e.g. registering <c>OptAButton</c> matches <c>&lt;OptAButton /&gt;</c> in Markdown).
    /// </typeparam>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddDocumentComponent<T>(this IServiceCollection services)
        where T : ComponentBase
    {
        var tagName = typeof(T).Name;

        var existing = services.FirstOrDefault(sd =>
            sd.ServiceType == typeof(IDocumentComponentRegistry) &&
            sd.ImplementationInstance != null);

        DocumentComponentRegistry registry;

        if (existing?.ImplementationInstance is DocumentComponentRegistry existingRegistry)
        {
            registry = existingRegistry;
        }
        else
        {
            // Remove any non-instance registrations and replace with a concrete instance.
            var stale = services.Where(sd => sd.ServiceType == typeof(IDocumentComponentRegistry)).ToList();
            foreach (var staleDescriptor in stale)
            {
                services.Remove(staleDescriptor);
            }

            registry = new DocumentComponentRegistry();
            services.AddSingleton<IDocumentComponentRegistry>(registry);
        }

        registry.Register(tagName, typeof(T));

        return services;
    }

    private static Type? GetValidInterface(Type type, Type[] interfaces)
    {
        return type
            .GetInterfaces()
            .Intersect(interfaces)
            .FirstOrDefault();
    }
}
