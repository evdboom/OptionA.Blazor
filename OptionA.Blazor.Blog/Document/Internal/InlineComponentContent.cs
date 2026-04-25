namespace OptionA.Blazor.Blog.Document.Internal;

/// <summary>
/// Represents a literal <c>&lt;OptA*&gt;</c> tag parsed from a Markdown document.
/// Holds the resolved component type (when whitelisted) or a warning message (when not).
/// </summary>
/// <remarks>
/// Although this type lives in the <c>Document.Internal</c> namespace it must be public
/// because it is used as a parameter type on the public <see cref="OptADocumentComponent"/> component.
/// It is an implementation detail of the document-parsing pipeline and is not intended for direct
/// consumption by library consumers.
/// </remarks>
public sealed class InlineComponentContent : Content
{
    /// <inheritdoc/>
    public override ContentType Type => ContentType.InlineComponent;

    /// <inheritdoc/>
    public override bool IsInvalid => false;

    /// <summary>
    /// The raw tag name extracted from the Markdown source (e.g. <c>OptAButton</c>).
    /// </summary>
    public string TagName { get; init; } = string.Empty;

    /// <summary>
    /// The .NET component type found in the document-component registry.
    /// <see langword="null"/> when the tag name is not whitelisted.
    /// </summary>
    public Type? ComponentType { get; init; }

    /// <summary>
    /// Raw attribute values as parsed from the HTML tag.
    /// A <see langword="null"/> value indicates a boolean-shorthand attribute (e.g. <c>Disabled</c>).
    /// </summary>
    public IReadOnlyDictionary<string, string?> RawAttributes { get; init; }
        = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Human-readable warning message set when the tag is not whitelisted or has no registry.
    /// <see langword="null"/> when the component was resolved successfully.
    /// </summary>
    public string? WarningMessage { get; init; }
}
