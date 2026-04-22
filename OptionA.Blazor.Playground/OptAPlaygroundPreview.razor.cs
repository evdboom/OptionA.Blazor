using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Playground;

/// <summary>
/// Minimal preview shell used by the playground container.
/// </summary>
public partial class OptAPlaygroundPreview
{
    /// <summary>
    /// Gets or sets the descriptor being previewed.
    /// </summary>
    [Parameter]
    public PlaygroundDescriptorBase? Descriptor { get; set; }

    /// <summary>
    /// Gets or sets the current playground parameter values.
    /// </summary>
    [CascadingParameter]
    public Dictionary<string, object?> CurrentParameters { get; set; } = [];

    [Inject]
    private IPlaygroundDataProvider DataProvider { get; set; } = null!;

    private Dictionary<string, object?> GetPreviewAttributes()
    {
        var result = GetAttributes();
        result["opta-playground-preview"] = true;

        if (TryGetClasses(DataProvider.DefaultPreviewClass, out var classes))
        {
            result["class"] = classes;
        }

        return result;
    }

    private bool TryGetPreviewRenderContext(out PreviewRenderContext renderContext)
    {
        renderContext = default;

        if (Descriptor is null)
        {
            return false;
        }

        var parameters = CreatePreviewParameters(Descriptor);
        renderContext = new PreviewRenderContext(Descriptor.ComponentType, parameters);
        return true;
    }

    private Dictionary<string, object?> CreatePreviewParameters(PlaygroundDescriptorBase descriptor)
    {
        var previewParameters = new Dictionary<string, object?>(CurrentParameters);

        foreach (var parameter in descriptor.Parameters)
        {
            if (!previewParameters.TryGetValue(parameter.Name, out var value) || value is not string content)
            {
                continue;
            }

            if (!IsRenderFragmentParameter(descriptor.ComponentType, parameter.Name))
            {
                continue;
            }

            previewParameters[parameter.Name] = CreateRenderFragment(content);
        }

        if (descriptor.StaticContent is not null &&
            CanAcceptChildContent(descriptor.ComponentType) &&
            (!previewParameters.TryGetValue("ChildContent", out var childContent) || childContent is null))
        {
            previewParameters["ChildContent"] = descriptor.StaticContent;
        }

        return previewParameters;
    }

    private static bool IsRenderFragmentParameter(Type componentType, string parameterName)
    {
        return GetParameterType(componentType, parameterName) == typeof(RenderFragment);
    }

    private static bool CanAcceptChildContent(Type componentType)
    {
        return IsRenderFragmentParameter(componentType, "ChildContent");
    }

    private static Type? GetParameterType(Type componentType, string parameterName)
    {
        return componentType.GetProperty(parameterName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy)
            ?.PropertyType;
    }

    private static RenderFragment CreateRenderFragment(string value)
    {
        return builder => builder.AddMarkupContent(0, value);
    }

    private readonly record struct PreviewRenderContext(Type ComponentType, IDictionary<string, object?> Parameters);
}
