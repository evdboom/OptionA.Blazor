using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OptionA.Blazor.Interactive.Infrastructure;
using OptionA.Blazor.Interactive.Interfaces;
using OptionA.Blazor.Playground;
using OptionA.Blazor.Interactive.Editors;
using OptionA.Blazor.Interactive.Exporters;
using System.Collections.Generic;

namespace OptionA.Blazor.Interactive
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOptionAInteractive(this IServiceCollection services)
        {
            // Register interactive/demo services here.
            services.TryAddSingleton<IInteractiveDataProvider, InteractiveDataProvider>();
            // Ensure the playground contract resolves to the same provider instance.
            services.TryAddSingleton<IPlaygroundDataProvider>(provider => provider.GetRequiredService<IInteractiveDataProvider>());
            return services;
        }

        public static IServiceCollection AddOptionABootstrapInteractive(this IServiceCollection services)
        {
            // Bootstrap default configuration for interactive documentation.
            void BootstrapConfig(InteractiveOptions options)
            {
                options.DefaultInteractiveClass = "card";
                options.DefaultPlaygroundClass = "card";
                options.DefaultPreviewClass = "card-body";
                options.DefaultEditorClass = "card-body";
                options.DefaultCodeClass = "card-body bg-light";
                options.DefaultEditorLabelClass = "form-label";
                options.DefaultEditorInputClass = "form-control";
                options.DefaultEditorGroupClass = "fw-bold mb-2 mt-3";
                options.CodeEditingEnabled = true;
                options.PreferredCodeEditor = InteractiveEditorKind.TextArea;
                options.DefaultCodeLanguage = "razor";
                options.EnabledExportFormats = new List<InteractiveExportFormat> { InteractiveExportFormat.Razor, InteractiveExportFormat.Json };
            }

            // Register provider with bootstrap configuration.
            services.TryAddSingleton<IInteractiveDataProvider>(provider => new InteractiveDataProvider(BootstrapConfig));
            services.TryAddSingleton<IPlaygroundDataProvider>(provider => provider.GetRequiredService<IInteractiveDataProvider>());


            return services;
        }
    }
}
