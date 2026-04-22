using System.Diagnostics.CodeAnalysis;
using OptionA.Blazor.Blog;
using OptionA.Blazor.Helpers.Contracts;

namespace OptionA.Blazor.Helpers.Infrastructure;

/// <summary>
/// Default implementation of <see cref="IComponentStyleProvider"/>.
/// </summary>
public class ComponentStyleProvider : IComponentStyleProvider
{
    private readonly ComponentStyleOptions _options;

    /// <summary>
    /// Creates a provider with the given configuration.
    /// </summary>
    /// <param name="configuration">Optional configuration action.</param>
    public ComponentStyleProvider(Action<ComponentStyleOptions>? configuration = null)
    {
        _options = new();
        configuration?.Invoke(_options);
    }

    /// <inheritdoc/>
    public Dictionary<string, object?> GetAttributes(ComponentElementType type, Dictionary<string, object?>? defaultAttributes = null)
    {
        var result = defaultAttributes ?? new Dictionary<string, object?>();
        result[$"opta-{type}".ToLowerInvariant()] = true;

        if (TryGetProperties(type, out var properties))
        {
            if (properties.AdditionalAttributes is not null)
            {
                foreach (var attribute in properties.AdditionalAttributes)
                {
                    result[attribute.Key] = attribute.Value;
                }
            }

            if (properties.Class is not null)
            {
                result["class"] = properties.Class;
            }
        }

        return result;
    }

    /// <inheritdoc/>
    public IContent? GetContent(ComponentElementType type, string? content)
    {
        if (!TryGetProperties(type, out var properties))
        {
            return content is not null
                ? new InlineContent { Content = content }
                : null;
        }

        IContent? result = properties.Content is not null || content is not null
            ? new InlineContent
            {
                Content = properties.Content ?? content ?? string.Empty
            }
            : null;

        if (result is not null && properties.ContentClass is not null)
        {
            result.AdditionalClasses.Add(properties.ContentClass);
        }

        return result;
    }

    /// <inheritdoc/>
    public bool TryGetProperties(ComponentElementType type, [NotNullWhen(true)] out ComponentElementProperties? properties)
    {
        if (_options.ComponentStyles.TryGetValue(type, out properties))
        {
            return true;
        }

        properties = null;
        return false;
    }
}
