using Microsoft.Extensions.DependencyInjection;
using OptionA.Blazor.Blog.Code.Parsers;
using OptionA.Blazor.Blog.Services;
using OptionA.Blazor.Blog.Text.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionA.Blazor.Blog.Builder
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

            .AddSingleton<ICodeParser, CSharpParser>()
            .AddSingleton<ICodeParser, HtmlParser>()

            .AddSingleton<IBlogBuilderDataProvider>(provider => new BlogBuilderDataProvider(configuration));

        return services;
    }


    /// <summary>
    /// Adds all blogparts to the servicecollection, prefilled with bootstrap (5.3) classes
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration">Additional configuration to be applied after setting bootstrap config</param>
    /// <returns></returns>
    public static IServiceCollection AddOptionABootstrapBlogBuilder(this IServiceCollection services, Action<OptaBlogBuilderOptions>? configuration = null)
    {
        var bootstrapConfig = (OptaBlogBuilderOptions options) =>
        {
            options.LabelClass = "form-label";
            options.InputClass = "form-control";
            options.Form = "row";
            options.BuilderTagListClass = "row g-1 border rounded-2 p-2";
            options.PostBuilderOptions = new()
            {
                [BuilderType.Title] = new BuilderTypeProperties
                {
                    ContainerClass = "col-lg-9 mb-2",
                },
                [BuilderType.Date] = new BuilderTypeProperties
                {
                    ContainerClass = "col-lg-9 mb-2",
                },
                [BuilderType.Subtitle] = new BuilderTypeProperties
                {
                    ContainerClass = "col-lg-9 mb-2",
                },
                [BuilderType.TagContainer] = new BuilderTypeProperties
                {
                    ContainerClass = "col-lg-9 mb-2",
                    Label = "Tags"
                },
                [BuilderType.RemoveTagButton] = new BuilderTypeProperties
                {
                    Class = "btn btn-danger btn-sm py-0",
                    Content = "**X**",
                    ContainerClass = "col-auto me-2 d-flex align-items-center"
                },
                [BuilderType.Tag] = new BuilderTypeProperties
                {
                    ContainerClass = "col-5",
                },
                [BuilderType.AddTagButton] = new BuilderTypeProperties
                {
                    ContainerClass = "ms-1 d-inline",
                    Class = "btn btn-success btn-sm py-0",
                    Content = "**+**",
                },
                [BuilderType.SavePostButton] = new BuilderTypeProperties
                {
                    Class = "btn btn-primary"
                },
                [BuilderType.AddPostButton] = new BuilderTypeProperties
                {
                    Class = "btn btn-primary"
                }

            };

            configuration?.Invoke(options);
        };

        return AddOptionABlog(services, bootstrapConfig);
    }
}
