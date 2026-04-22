using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OptionA.Blazor.Helpers.Contracts;
using OptionA.Blazor.Helpers.Infrastructure;

namespace OptionA.Blazor.Helpers;

/// <summary>
/// Service registration for helper components.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the helper components with optional styling configuration.
    /// </summary>
    public static IServiceCollection AddOptionAHelpers(this IServiceCollection services, Action<ComponentStyleOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        if (lifetime == ServiceLifetime.Singleton)
        {
            services.TryAddSingleton<IComponentStyleProvider>(_ => new ComponentStyleProvider(configuration));
        }
        else if (lifetime == ServiceLifetime.Scoped)
        {
            services.TryAddScoped<IComponentStyleProvider>(_ => new ComponentStyleProvider(configuration));
        }
        else
        {
            throw new NotSupportedException("Only Singleton and Scoped lifetimes are supported");
        }

        return services;
    }

    /// <summary>
    /// Adds the helper components with bootstrap-friendly defaults.
    /// </summary>
    public static IServiceCollection AddOptionABootstrapHelpers(this IServiceCollection services, Action<ComponentStyleOptions>? configuration = null, ServiceLifetime lifetime = ServiceLifetime.Singleton)
    {
        var bootstrapConfig = (ComponentStyleOptions options) =>
        {
            options.ComponentStyles = new()
            {
                [ComponentElementType.Component] = new() { Class = "row g-1" },
                [ComponentElementType.ComponentHeaderBar] = new() { Class = "align-items-center" },
                [ComponentElementType.ButtonContainer] = new() { Class = "col-auto d-flex flex-column" },
                [ComponentElementType.RemoveButton] = new() { Class = "btn btn-danger btn-sm mb-1 p-1", Content = "**🗙**" },
                [ComponentElementType.PropertiesButton] = new() { Class = "btn btn-primary btn-sm p-1 mb-1" },
                [ComponentElementType.MoveButtonContainer] = new() { Class = "btn-group-vertical" },
                [ComponentElementType.MoveUpButton] = new() { Class = "btn btn-secondary btn-sm p-1" },
                [ComponentElementType.MoveDownButton] = new() { Class = "btn btn-secondary btn-sm p-1" },
                [ComponentElementType.ComponentTitle] = new() { Class = "align-items-center" },
                [ComponentElementType.ComponentContent] = new() { Class = "border border-secondary rounded-2 p-2 col-10 bg-secondary-subtle" },
                [ComponentElementType.PropertiesModal] = new() { Class = "opta-bs-modal modal-dialog" },
                [ComponentElementType.PropertiesModalHeader] = new() { Class = "modal-header", ContentClass = "modal-title" },
                [ComponentElementType.PropertiesModalContent] = new() { Class = "modal-body" },
                [ComponentElementType.PropertiesModalCloseButton] = new() { Class = "btn-close", Content = string.Empty },
                [ComponentElementType.PropertiesModalSection] = new() { Class = "modal-content" },
                [ComponentElementType.CollapseButton] = new() { Class = "btn btn-outline-secondary btn-sm no-border me-1", Content = "**△**" },
                [ComponentElementType.ExpandButton] = new() { Class = "btn btn-outline-secondary btn-sm no-border me-1", Content = "**▽**" },
                [ComponentElementType.ListItemsHeader] = new() { Class = "form-label" },
                [ComponentElementType.ListItemsContainer] = new() { Class = "row g-1" },
                [ComponentElementType.ListItemContainer] = new() { Class = "col-md-6 row g-1 mb-1" },
                [ComponentElementType.ListRemoveButtonContainer] = new() { Class = "col-auto" },
                [ComponentElementType.ListRemoveButton] = new() { Class = "btn-close", Content = string.Empty },
                [ComponentElementType.ListItemInputContainer] = new() { Class = "col" },
                [ComponentElementType.ListItemInput] = new() { Class = "form-control" },
                [ComponentElementType.Label] = new() { Class = "form-label" },
                [ComponentElementType.TextAreaAutoGrowContainer] = new() { Class = "bootstrap" },
                [ComponentElementType.FlexibleTextArea] = new() { Class = "bootstrap" },
                [ComponentElementType.CheckboxInput] = new() { Class = "form-check-input" },
                [ComponentElementType.MoveToIndexInputContainer] = new() { Class = "btn btn-secondary btn-sm p-1" },
                [ComponentElementType.MoveToIndexInput] = new() { Class = "input3em" }
            };

            configuration?.Invoke(options);
        };

        return AddOptionAHelpers(services, bootstrapConfig, lifetime);
    }
}
