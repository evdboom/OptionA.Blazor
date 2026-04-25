using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Blog.Document.Internal;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Renders a whitelisted inline <c>&lt;OptA*&gt;</c> component tag parsed from a Markdown document.
/// When the component type is resolved from the registry, renders it via <see cref="DynamicComponent"/>.
/// When the tag is not whitelisted, renders escaped text and a visible warning instead.
/// </summary>
public partial class OptADocumentComponent
{
    /// <summary>
    /// The parsed inline component content, carrying either a resolved component type or a warning.
    /// </summary>
    [Parameter]
    public InlineComponentContent? Content { get; set; }

    private Dictionary<string, object?> _parameters = [];

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        _parameters = Content?.ComponentType is not null
            ? CoerceParameters(Content.ComponentType, Content.RawAttributes)
            : [];
    }

    /// <summary>
    /// Converts raw string attribute values to the types expected by the component's parameters,
    /// using reflection on <see cref="ParameterAttribute"/>-marked properties.
    /// Supported coercions: string, bool (including shorthand), int, and enum.
    /// </summary>
    private static Dictionary<string, object?> CoerceParameters(
        Type componentType,
        IReadOnlyDictionary<string, string?> rawAttributes)
    {
        var result = new Dictionary<string, object?>();

        if (rawAttributes.Count == 0)
        {
            return result;
        }

        var paramProperties = componentType
            .GetProperties()
            .Where(p => p.GetCustomAttributes(typeof(ParameterAttribute), inherit: true).Length > 0)
            .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

        foreach (var (key, rawValue) in rawAttributes)
        {
            if (!paramProperties.TryGetValue(key, out var prop))
            {
                continue;
            }

            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            object? coerced = TryCoerce(targetType, rawValue);

            if (coerced is not null)
            {
                result[prop.Name] = coerced;
            }
        }

        return result;
    }

    private static object? TryCoerce(Type targetType, string? rawValue)
    {
        // Boolean shorthand: attribute present with no value means true.
        if (rawValue is null)
        {
            return targetType == typeof(bool) ? (object)true : null;
        }

        if (targetType == typeof(string))
        {
            return rawValue;
        }

        if (targetType == typeof(bool))
        {
            return bool.TryParse(rawValue, out var b) ? b : (object?)null;
        }

        if (targetType == typeof(int))
        {
            return int.TryParse(rawValue, out var i) ? i : (object?)null;
        }

        if (targetType.IsEnum)
        {
            return Enum.TryParse(targetType, rawValue, ignoreCase: true, out var e) ? e : null;
        }

        return null;
    }
}
