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

    /// <summary>
    /// Non-fatal errors produced when one or more parameter overrides could not be coerced
    /// to the expected parameter type. These are surfaced as visible warnings in the document.
    /// </summary>
    internal IReadOnlyList<string> OverrideErrors { get; }

    internal DirectivePlaygroundDescriptor(
        PlaygroundDescriptorBase source,
        IReadOnlyDictionary<string, string> overrides)
    {
        _componentType = source.ComponentType;
        Title = source.Title;
        Description = source.Description;
        StaticContent = source.StaticContent;

        var errors = new List<string>();
        Parameters = ApplyOverrides(source.Parameters, overrides, errors);
        OverrideErrors = errors;
    }

    private static IList<PlaygroundParameterDescriptor> ApplyOverrides(
        IList<PlaygroundParameterDescriptor> parameters,
        IReadOnlyDictionary<string, string> overrides,
        List<string> errors)
    {
        if (overrides.Count == 0)
        {
            return parameters;
        }

        return parameters.Select(p =>
        {
            if (overrides.TryGetValue(p.Name, out var overrideValue))
            {
                var (coerced, error) = ConvertValue(overrideValue, p.Name, p.ValueType);
                if (error is not null)
                {
                    errors.Add(error);
                    return p;
                }

                return new PlaygroundParameterDescriptor
                {
                    Name = p.Name,
                    DisplayName = p.DisplayName,
                    Description = p.Description,
                    EditorType = p.EditorType,
                    ValueType = p.ValueType,
                    DefaultValue = coerced,
                    AllowedValues = p.AllowedValues,
                    DisplayFormat = p.DisplayFormat,
                    Group = p.Group,
                    Order = p.Order,
                };
            }

            return p;
        }).ToList();
    }

    private static (object? coerced, string? error) ConvertValue(string raw, string parameterName, Type targetType)
    {
        var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;
        var coerced = TypeCoercionHelper.TryCoerce(underlyingType, raw);

        if (coerced is null && underlyingType != typeof(string))
        {
            return (null,
                $"Parameter \"{parameterName}\": cannot convert override value \"{raw}\" to type \"{underlyingType.Name}\".");
        }

        return (coerced, null);
    }
}
