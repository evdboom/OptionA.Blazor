using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Blog.Document.Internal;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Renders a playground directive parsed from a Markdown document.
/// Shows the interactive <see cref="OptionA.Blazor.Playground.OptAPlayground"/> when
/// the descriptor was resolved, or a visible error block when resolution failed.
/// </summary>
public partial class OptADocumentPlayground
{
    /// <summary>
    /// The parsed playground directive content, carrying either a resolved descriptor
    /// or an error message produced during the parse stage.
    /// </summary>
    [Parameter]
    public PlaygroundDirectiveContent? Content { get; set; }
}
