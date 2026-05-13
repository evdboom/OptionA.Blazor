using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Document.Internal;

/// <summary>
/// Converts raw string attribute values to the types expected by a component's parameters,
/// using reflection on <see cref="ParameterAttribute"/>-marked properties.
/// Supported coercions: string, bool (including shorthand), int, and enum.
/// </summary>
internal static class ParameterCoercer
{
    /// <summary>
    /// Coerces the raw string attributes into a parameter dictionary suitable for
    /// <see cref="Microsoft.AspNetCore.Components.DynamicComponent"/>.
    /// </summary>
    internal static Dictionary<string, object?> Coerce(
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
