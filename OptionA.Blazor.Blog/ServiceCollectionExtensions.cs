using Microsoft.Extensions.DependencyInjection;
using OptionA.Blazor.Blog.Code.Parsers;
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
        /// Adds all blogparts to the servicecollection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddOptionABlog(this IServiceCollection services, Action<OptaBlogOptions>? configuration = null)
        {
            services
                .AddSingleton<IBuilderService, BuilderService>()
                .AddSingleton<IMarkDownParser, MarkDownParser>()

                .AddSingleton<IMarkerDefinition, BoldMarker>()
                .AddSingleton<IMarkerDefinition, ItalicMarker>()
                .AddSingleton<IMarkerDefinition, LinkMarker>()
                .AddSingleton<IMarkerDefinition, LineBreakMarker>()
                .AddSingleton<IMarkerDefinition, IconMarker>()
                .AddSingleton<IMarkerDefinition, CiteMarker>()

                .AddSingleton<ICodeParser, CSharpParser>()
                .AddSingleton<ICodeParser, HtmlParser>()

                .AddSingleton<IBlogDataProvider>(provider => new BlogDataProvider(configuration));

            return services;
        }


        /// <summary>
        /// Adds all blogparts to the servicecollection, prefilled with bootstrap (5.3) classes
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">Additional configuration to be applied after setting bootstrap config</param>
        /// <returns></returns>
        public static IServiceCollection AddOptionABootstrapBlog(this IServiceCollection services, Action<OptaBlogOptions>? configuration = null)
        {
            var bootstrapConfig = (OptaBlogOptions options) =>
            {
                
  
                configuration?.Invoke(options);
            };

            return AddOptionABlog(services, bootstrapConfig);
        }
    }
}
