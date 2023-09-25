using Microsoft.Extensions.DependencyInjection;

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
            .AddSingleton<IBlogBuilderDataProvider>(provider => new BlogBuilderDataProvider(configuration));

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



                options.PostBuilderOptions = new()
                { 
                    [BuilderType.Title] = GetDefaultBootstrapInputProperties("Title..."),
                    [BuilderType.Date] = GetDefaultBootstrapInputProperties(),
                    [BuilderType.Subtitle] = GetDefaultBootstrapInputProperties("Subtitle..."),
                    [BuilderType.Tag] = GetDefaultBootstrapInputListProperties("Tag..."),
                    [BuilderType.Paragraph] = GetDefaultComponentProperties("Paragraph..."),
                    [BuilderType.Header] = GetDefaultComponentProperties("Header..."),
                    [BuilderType.AdditionalClasses] = GetDefaultBootstrapSideListProperties("Class..."),
                    [BuilderType.RemovedClasses] = GetDefaultBootstrapSideListProperties("Class..."),
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

            return AddOptionABlogBuilder(services, bootstrapConfig);
        }

        private static BuilderTypeProperties GetDefaultComponentProperties(string? placeholder = null)
        {
            return new BuilderTypeProperties
            {
                Class = "form-control h-100",
                LabelClass = "form-label",
                GroupClass = "col-lg-12 mb-2 border rounded-2 p-2",
                InnerGroupClass = "row",
                ContainerClass = "col-lg-8",
                Placeholder = placeholder,
                ExtraPropertiesClass = "col-lg-4",
                AddButton = new ButtonProperties
                {
                    ContainerClass = "ms-1 d-inline mb-2",
                    Class = "btn btn-outline-primary btn-sm py-0",
                },
                RemoveButton = new ButtonProperties
                {
                    Class = "btn btn-outline-danger btn-sm py-0",
                    Content = "**🗙**",
                    ContainerClass = "float-end",
                    Title = "Remove"
                }
            };
        }

        private static BuilderTypeProperties GetDefaultBootstrapInputProperties(string? placeholder = null)
        {
            return new BuilderTypeProperties
            {
                Class = "form-control",
                LabelClass = "form-label",
                ContainerClass = "col-lg-8 mb-2",
                Placeholder = placeholder
            };
        }

        private static BuilderTypeProperties GetDefaultBootstrapInputListProperties(string? placeholder = null)
        {
            return new BuilderTypeProperties
            {
                Class = "form-control",
                LabelClass = "form-label",
                GroupClass = "col-lg-8 mb-2",
                ContainerClass = "col-lg-4",
                Placeholder = placeholder,
                InnerGroupClass = "row g-1 border rounded-2 p-2",
                AddButton = new ButtonProperties
                {
                    ContainerClass = "ms-1 d-inline",
                    Class = "btn btn-success btn-sm py-0",
                    Content = "**＋**",
                    Title = "Add"
                },
                RemoveButton = new ButtonProperties
                {
                    Class = "btn btn-outline-danger btn-sm py-0",
                    Content = "**🗙**",
                    ContainerClass = "col-auto me-2 d-flex align-items-center",
                    Title = "Remove"
                }
            };
        }

        private static BuilderTypeProperties GetDefaultBootstrapSideListProperties(string? placeholder = null)
        {
            return new BuilderTypeProperties
            {
                Class = "form-control",
                LabelClass = "form-label",
                GroupClass = "col-lg-12 mb-2",
                ContainerClass = "col-lg-9",
                Placeholder = placeholder,
                InnerGroupClass = "row g-1 border rounded-2 p-2",
                AddButton = new ButtonProperties
                {
                    ContainerClass = "ms-1 d-inline",
                    Class = "btn btn-success btn-sm py-0",
                    Content = "**＋**",
                    Title = "Add"
                },
                RemoveButton = new ButtonProperties
                {
                    Class = "btn btn-outline-danger btn-sm py-0",
                    Content = "**🗙**",
                    ContainerClass = "col-auto d-flex align-items-center",
                    Title = "Remove"
                }
            };
        }
    }
}
