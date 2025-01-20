using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Map content to correct components
/// </summary>
public partial class OptAChild
{
    /// <summary>
    /// Content to render
    /// </summary>
    [Parameter]
    public IContent? Content { get; set; }
}

