using OptionA.Blazor.Playground;

namespace OptionA.Blazor.Blog.Document.Internal;

/// <summary>
/// A concrete <see cref="PlaygroundDescriptorBase"/> that clones an existing descriptor and
/// applies YAML parameter overrides from a Markdown directive. Used internally by the document
/// parsing pipeline; never exposed as part of the public API.
/// </summary>
internal sealed class DirectivePlaygroundDescriptor : PlaygroundDescriptorBase
{
    private readonly Type _componentType;

    /// <inheritdoc/>
    public override Type ComponentType => _componentType;

    internal DirectivePlaygroundDescriptor(
        PlaygroundDescriptorBase source,
        IReadOnlyDictionary<string, string> overrides)
    {
        _componentType = source.ComponentType;
        Title = source.Title;
        Description = source.Description;
        StaticContent = source.StaticContent;
        Parameters = ApplyOverrides(source.Parameters, overrides);
    }

    private static IList<PlaygroundParameterDescriptor> ApplyOverrides(
        IList<PlaygroundParameterDescriptor> parameters,
        IReadOnlyDictionary<string, string> overrides)
    {
        if (overrides.Count == 0)
        {
            return parameters;
        }

        return parameters.Select(p =>
        {
            if (overrides.TryGetValue(p.Name, out var overrideValue))
            {
                return new PlaygroundParameterDescriptor
                {
                    Name = p.Name,
                    DisplayName = p.DisplayName,
                    Description = p.Description,
                    EditorType = p.EditorType,
                    ValueType = p.ValueType,
                    DefaultValue = ConvertValue(overrideValue, p.ValueType),
                    AllowedValues = p.AllowedValues,
                    DisplayFormat = p.DisplayFormat,
                    Group = p.Group,
                    Order = p.Order,
                };
            }

            return p;
        }).ToList();
    }

    private static object? ConvertValue(string raw, Type targetType)
    {
        if (targetType == typeof(string))
        {
            return raw;
        }

        if (targetType == typeof(bool) && bool.TryParse(raw, out var boolValue))
        {
            return boolValue;
        }

        if (targetType == typeof(int) && int.TryParse(raw, out var intValue))
        {
            return intValue;
        }

        if (targetType.IsEnum && Enum.TryParse(targetType, raw, ignoreCase: true, out var enumValue))
        {
            return enumValue;
        }

        // Fallback: preserve the string representation
        return raw;
    }
}
