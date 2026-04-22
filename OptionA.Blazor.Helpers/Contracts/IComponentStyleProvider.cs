using System.Diagnostics.CodeAnalysis;
using OptionA.Blazor.Blog;

namespace OptionA.Blazor.Helpers.Contracts;

/// <summary>
/// Provides attributes and content for helper components.
/// </summary>
public interface IComponentStyleProvider
{
    /// <summary>
    /// Gets the configured properties for a given element type.
    /// </summary>
    bool TryGetProperties(ComponentElementType type, [NotNullWhen(true)] out ComponentElementProperties? properties);

    /// <summary>
    /// Gets the HTML attributes for a given element type.
    /// </summary>
    Dictionary<string, object?> GetAttributes(ComponentElementType type, Dictionary<string, object?>? defaultAttributes = null);

    /// <summary>
    /// Gets the content for a given element type.
    /// </summary>
    IContent? GetContent(ComponentElementType type, string? content);
}
