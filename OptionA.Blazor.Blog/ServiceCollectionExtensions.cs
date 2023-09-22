using Microsoft.Extensions.DependencyInjection;
using OptionA.Blazor.Blog.Services;
using OptionA.Blazor.Blog.Struct;
using OptionA.Blazor.Blog.Text.Parser;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Class for adding blog parts to the service collection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the option A blogparts to the servicecollection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddOptionABlog(this IServiceCollection services, Action<BlogOptions>? configuration = null)
        {
            services
                .AddSingleton<IBuilderService, BuilderService>()
                .AddSingleton<IMarkDownParser, MarkDownParser>()
                .AddSingleton<IMarkerDefinition, BoldMarker>()
                .AddSingleton<IMarkerDefinition, ItalicMarker>()
                .AddSingleton<IMarkerDefinition, LinkMarker>()
                .AddSingleton<IMarkerDefinition, LineBreakMarker>()
                .AddSingleton<IBlogDataProvider>(provider => new BlogDataProvider(configuration));

            return services;

        }
    }
}
