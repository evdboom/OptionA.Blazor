using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Renders a Markdown string using the existing Blog render components.
/// Supports headings, paragraphs, code blocks, lists, block quotes, tables, and images.
/// Also extracts optional YAML front-matter and surfaces it via OnMetadataParsed.
/// </summary>
public partial class OptADocument
{
    /// <summary>
    /// Markdown source to render.
    /// </summary>
    [Parameter]
    public string? Source { get; set; }

    /// <summary>
    /// Optional callback invoked when YAML front-matter is present and parsed.
    /// </summary>        [Parameter]
    public EventCallback<DocumentMetadata> OnMetadataParsed { get; set; }

    [Inject]
    private IMarkdownDocumentParser Parser { get; set; } = null!;

    private IReadOnlyList<IContent>? _content;

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        if (string.IsNullOrWhiteSpace(Source))
        {
            _content = Array.Empty<IContent>();
            return;
        }

        var (metadata, body) = DocumentMetadata.ParseFromMarkdown(Source);
        if (metadata is not null && OnMetadataParsed.HasDelegate)
        {            // fire-and-forget is acceptable in sync lifecycle; caller may handle async if needed                    _ = OnMetadataParsed.InvokeAsync(metadata);        }
        _content = Parser.Parse(body);
    }
}
