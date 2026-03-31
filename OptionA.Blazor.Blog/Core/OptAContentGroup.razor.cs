using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Component for grouping multiple components
/// </summary>
public partial class OptAContentGroup
{
    /// <summary>
    /// Content to build child parts from
    /// </summary>
    [Parameter]
    public ContentGroupContent? Content { get; set; }

    /// <summary>
    /// Content to display (fully dynamic) when no content is supplied
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Inject]
    private IBlogDataProvider DataProvider { get; set; } = null!;
}
