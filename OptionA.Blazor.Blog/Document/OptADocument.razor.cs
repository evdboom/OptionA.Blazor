using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Renders a Markdown string using the existing Blog render components.
/// Supports headings, paragraphs, code blocks, lists, block quotes, tables, and images.
/// </summary>
public partial class OptADocument
{
    /// <summary>
    /// Markdown source to render.
    /// </summary>
    [Parameter]
    public string? Source { get; set; }

    [Inject]
    private IMarkdownDocumentParser Parser { get; set; } = null!;

    private IReadOnlyList<IContent>? _content;

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        _content = Parser.Parse(Source);
    }
}
