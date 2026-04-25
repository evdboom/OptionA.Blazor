using System.Diagnostics.CodeAnalysis;

namespace OptionA.Blazor.Blog.Document.Internal;

/// <summary>
/// Default implementation of <see cref="IDocumentComponentRegistry"/>.
/// Populated at application startup via <c>services.AddDocumentComponent&lt;T&gt;()</c>.
/// </summary>
internal sealed class DocumentComponentRegistry : IDocumentComponentRegistry
{
    private readonly Dictionary<string, Type> _map = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Registers a component type under the given tag name.
    /// If the tag name is already registered, the new type replaces the previous one.
    /// </summary>
    internal void Register(string tagName, Type componentType) => _map[tagName] = componentType;

    /// <inheritdoc/>
    public bool TryGetComponentType(string tagName, [NotNullWhen(true)] out Type? componentType)
        => _map.TryGetValue(tagName, out componentType);
}
