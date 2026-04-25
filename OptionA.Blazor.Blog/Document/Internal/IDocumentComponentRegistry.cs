namespace OptionA.Blazor.Blog.Document.Internal;

/// <summary>
/// Internal whitelist registry that maps OptA component HTML tag names to their .NET types.
/// Consumers populate it via <c>services.AddDocumentComponent&lt;T&gt;()</c>.
/// </summary>
internal interface IDocumentComponentRegistry
{
    /// <summary>
    /// Tries to find the component <see cref="Type"/> for the given HTML tag name.
    /// </summary>
    /// <param name="tagName">The tag name to look up (e.g. <c>OptAButton</c>).</param>
    /// <param name="componentType">The matched component type, or <see langword="null"/> when not found.</param>
    /// <returns><see langword="true"/> when the tag name is registered.</returns>
    bool TryGetComponentType(string tagName, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out Type? componentType);
}
