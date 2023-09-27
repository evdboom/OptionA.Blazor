using Microsoft.Extensions.DependencyInjection;
using OptionA.Blazor.Storage;

namespace OptionA.Blazor.Blog.Builder
{
    /// <summary>
    /// Class for adding blogbuilder parts to the service collection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all blogparts to the servicecollection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddOptionABlogBuilder(this IServiceCollection services, Action<OptaBlogBuilderOptions>? configuration = null) => services
            .AddSingleton<IBlogBuilderDataProvider>(provider => new BlogBuilderDataProvider(configuration))
            .AddStorageService();

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
                options.FormClass = "row";
                options.CreatePostButton = new BuilderTypeProperties
                {
                    Class = "btn btn-primary"
                };
                options.SavePostButton = new BuilderTypeProperties
                {
                    Class = "btn btn-primary",
                    ContainerClass = "mt-2"
                };
                options.PostBuilderOptions = new()
                {
                    [BuilderType.TextInput] = GetDefaultBootstrapInputProperties(),
                    [BuilderType.DateInput] = GetDefaultBootstrapInputProperties(),
                    [BuilderType.TextAreaInput] = GetDefaultBootstrapInputProperties(container: "bootstrap"),
                    [BuilderType.SelectInput] = new BuilderTypeProperties
                    { 
                        Class = "form-select"
                    },
                    [BuilderType.Label] = new BuilderTypeProperties 
                    {
                        Class = "form-label" 
                    },
                    [BuilderType.ComponentContent] = new BuilderTypeProperties
                    {
                        Class = "border border-secondary rounded-2 p-2 col-10 bg-secondary-subtle"
                    },
                    [BuilderType.ComponentTitle] = new BuilderTypeProperties
                    {
                        ContainerClass = "col-12"
                    },
                    [BuilderType.Component] = new BuilderTypeProperties
                    {
                        Class = "row g-1"
                    },
                    [BuilderType.RemoveButton] = new BuilderTypeProperties
                    {
                        Class="btn btn-danger btn-sm mb-1",
                        ContainerClass="col-auto d-flex flex-column",
                        Content = "**🗙**"
                    },
                    [BuilderType.MoveUpButton] = new BuilderTypeProperties
                    {
                        Class = "btn btn-secondary btn-sm p-1",
                        ContainerClass = "btn-group-vertical",
                    },
                    [BuilderType.MoveDownButton] = new BuilderTypeProperties
                    {
                        Class = "btn btn-secondary btn-sm p-1",
                    }
            };

                configuration?.Invoke(options);
            };

            return AddOptionABlogBuilder(services, bootstrapConfig);
        }


        private static BuilderTypeProperties GetDefaultBootstrapInputProperties(string? additional = null, string? container = null)
        {
            return new BuilderTypeProperties
            {
                Class = $"form-control {additional}".Trim(),
                ContainerClass = container
            };
        }      
    }
}
