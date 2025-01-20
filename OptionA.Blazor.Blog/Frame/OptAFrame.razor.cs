using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Iframe component
/// </summary>
public partial class OptAFrame
{
    /// <summary>
    /// Content for the Iframe
    /// </summary>
    [Parameter]
    public FrameContent? Content { get; set; }
    [Inject]
    private IBlogDataProvider DataProvider { get; set; } = null!;
}
