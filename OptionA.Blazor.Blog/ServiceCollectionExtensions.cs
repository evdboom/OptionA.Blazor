using Microsoft.Extensions.DependencyInjection;
using OptionA.Blazor.Blog.Code.Parsers;
using OptionA.Blazor.Blog.Services;
using OptionA.Blazor.Blog.Struct;
using OptionA.Blazor.Blog.Text.Parser;
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
    /// <returns></returns>
    public static IServiceCollection AddOptionABlog(this IServiceCollection services, Action<OptABlogOptions>? configuration = null)
    {
        services
            .AddSingleton<IBuilderService, BuilderService>()
            .AddSingleton<IMarkDownParser, MarkDownParser>()
            .AddSingleton<IBlogDataProvider>(provider => new BlogDataProvider(configuration));

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
            services
                .AddSingleton(type.Interface!, type.Type);
        }

        return services;
    }

    /// <summary>
    /// Adds all blogparts to the servicecollection, prefilled with bootstrap (5.3) classes
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration">Additional configuration to be applied after setting bootstrap config</param>
    /// <returns></returns>
    public static IServiceCollection AddOptionABootstrapBlog(this IServiceCollection services, Action<OptABlogOptions>? configuration = null)
    {
        var bootstrapConfig = (OptABlogOptions options) =>
        {


            configuration?.Invoke(options);
        };

        return AddOptionABlog(services, bootstrapConfig);
    }

    private static Type? GetValidInterface(Type type, Type[] interfaces)
    {
        return type
            .GetInterfaces()
            .Intersect(interfaces)
            .FirstOrDefault();
    }
}
