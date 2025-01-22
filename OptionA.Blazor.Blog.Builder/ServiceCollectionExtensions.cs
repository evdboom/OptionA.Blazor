using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OptionA.Blazor.Blog.Services;
using OptionA.Blazor.Storage;

namespace OptionA.Blazor.Blog.Builder;

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
    public static IServiceCollection AddOptionABlogBuilder(this IServiceCollection services, Action<OptABlogBuilderOptions>? configuration = null)
    {
        services
            .AddStorageService();
        services
            .TryAddSingleton<IBuilderService, BuilderService>();
        services
            .TryAddSingleton<IBlogBuilderDataProvider>(provider => new BlogBuilderDataProvider(configuration));

        return services;
    }


    /// <summary>
    /// Adds all blogparts to the servicecollection, prefilled with bootstrap (5.3) classes
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration">Additional configuration to be applied after setting bootstrap config</param>
    /// <returns></returns>
    public static IServiceCollection AddOptionABootstrapBlogBuilder(this IServiceCollection services, Action<OptABlogBuilderOptions>? configuration = null)
    {
        var bootstrapConfig = (OptABlogBuilderOptions options) =>
        {
            options.PostBuilderOptions = new()
            {
                [BuilderType.Form] = new BuilderTypeProperties
                {
                    Class = "row"
                },
                [BuilderType.CreatePostButton] = new BuilderTypeProperties
                {
                    Class = "btn btn-primary"
                },
                [BuilderType.SavePostButton] = new BuilderTypeProperties
                {
                    Class = "btn btn-primary"
                },
                [BuilderType.SavePostButtonContainer] = new BuilderTypeProperties
                {
                    Class = "mt-2"
                },
                [BuilderType.TextInput] = GetDefaultBootstrapInputProperties(),
                [BuilderType.DateInput] = GetDefaultBootstrapInputProperties(),
                [BuilderType.TextAreaInput] = GetDefaultBootstrapInputProperties(),
                [BuilderType.TextAreaAutoGrow] = new BuilderTypeProperties
                {
                    Class = "bootstrap"
                },
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
                    Class = "align-items-center",                    
                },
                [BuilderType.Component] = new BuilderTypeProperties
                {
                    Class = "row g-1"
                },
                [BuilderType.ButtonContainer] = new BuilderTypeProperties
                {
                    Class = "col-auto d-flex flex-column",
                },
                [BuilderType.RemoveButton] = new BuilderTypeProperties
                {
                    Class = "btn btn-danger btn-sm mb-1 p-1",
                    Content = "**🗙**"
                },
                [BuilderType.CollapseButton] = new BuilderTypeProperties
                {
                    Class = "btn btn-outline-secondary btn-sm no-border me-1",
                    Content = "**△**"
                },
                [BuilderType.ExpandButton] = new BuilderTypeProperties
                {
                    Class = "btn btn-outline-secondary btn-sm no-border me-1",
                    Content = "**▽**"
                },
                [BuilderType.MoveButtonContainer] = new BuilderTypeProperties
                {
                    Class = "btn-group-vertical",
                },
                [BuilderType.MoveUpButton] = new BuilderTypeProperties
                {
                    Class = "btn btn-secondary btn-sm p-1",
                },
                [BuilderType.MoveDownButton] = new BuilderTypeProperties
                {
                    Class = "btn btn-secondary btn-sm p-1",
                },
                [BuilderType.PropertiesButton] = new BuilderTypeProperties
                {
                    Class = "btn btn-primary btn-sm p-1 mb-1"
                },
                [BuilderType.ListRemoveButton] = new BuilderTypeProperties
                {
                    Class = "btn-close",
                    Content = string.Empty,
                },
                [BuilderType.ListRemoveButtonContainer] = new BuilderTypeProperties
                {
                    Class = "col-auto",
                },
                [BuilderType.ListItemInput] = GetDefaultBootstrapInputProperties(),
                [BuilderType.ListItemInputContainer] = new BuilderTypeProperties
                {
                    Class = "col"
                },
                [BuilderType.ListItemContainer] = new BuilderTypeProperties
                {
                    Class = "col-md-6 row g-1 mb-1"
                },
                [BuilderType.ListItemsContainer] = new BuilderTypeProperties
                {
                    Class = "row g-1"
                },
                [BuilderType.AddContentButton] = new BuilderTypeProperties
                {
                    Class = "btn btn-primary btn-sm p-1"
                },
                [BuilderType.AddContentButtonContainer] = new BuilderTypeProperties
                {
                    Class = "col-auto"
                },
                [BuilderType.ComponentBar] = new BuilderTypeProperties
                {
                    Class = "row g-1 sticky-top"
                },
                [BuilderType.PropertiesModal] = new BuilderTypeProperties
                {
                    Class = "opta-bs-modal modal-dialog"
                },
                [BuilderType.PropertiesModalCloseButton] = new BuilderTypeProperties
                {
                    Class = "btn-close",
                    Content = string.Empty
                },
                [BuilderType.PropertiesModalHeader] = new BuilderTypeProperties
                {
                    Class = "modal-header",
                    ContentClass = "modal-title"
                },
                [BuilderType.PropertiesModalContent] = new BuilderTypeProperties
                {
                    Class = "modal-body"
                },
                [BuilderType.PropertiesModalSection] = new BuilderTypeProperties
                {
                    Class = "modal-content"
                }

            };

            configuration?.Invoke(options);
        };

        return AddOptionABlogBuilder(services, bootstrapConfig);
    }


    private static BuilderTypeProperties GetDefaultBootstrapInputProperties()
    {
        return new BuilderTypeProperties
        {
            Class = $"form-control"
        };
    }
}
