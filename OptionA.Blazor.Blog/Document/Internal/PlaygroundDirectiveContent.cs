using OptionA.Blazor.Playground;

namespace OptionA.Blazor.Blog.Document.Internal;

/// <summary>
/// Represents a <c>::: playground id="..." :::</c> directive parsed from a Markdown document.
/// Holds the resolved descriptor (or error message) produced during the parse stage.
/// </summary>
/// <remarks>
/// This type is an implementation detail of the document-parsing pipeline and is not intended
/// for direct consumption by library consumers.
/// </remarks>
internal sealed class PlaygroundDirectiveContent : Content
{
    /// <inheritdoc/>
    public override ContentType Type => ContentType.Playground;

    /// <inheritdoc/>
    public override bool IsInvalid => false;

    /// <summary>
    /// The id extracted from the directive's opening line.
    /// </summary>
    public string? DirectiveId { get; init; }

    /// <summary>
    /// The resolved descriptor, with any YAML parameter overrides applied.
    /// Null when the id could not be resolved.
    /// </summary>
    public PlaygroundDescriptorBase? ResolvedDescriptor { get; set; }

    /// <summary>
    /// Human-readable error message set when resolution fails (unknown id, no resolver, etc.).
    /// Null when the descriptor was resolved successfully.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Raw parameter overrides parsed from the YAML body of the directive.
    /// </summary>
    public IReadOnlyDictionary<string, string> ParameterOverrides { get; init; }
        = new Dictionary<string, string>(StringComparer.Ordinal);

    /// <summary>
    /// Non-fatal warnings produced when one or more parameter overrides could not be coerced
    /// to the expected parameter type. The playground still renders using original defaults;
    /// these messages are surfaced as visible warnings in the document.
    /// </summary>
    public IReadOnlyList<string> OverrideWarnings { get; set; } = [];
}
