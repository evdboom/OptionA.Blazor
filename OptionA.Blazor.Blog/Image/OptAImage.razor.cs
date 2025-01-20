using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Image component
/// </summary>
public partial class OptAImage
{
    /// <summary>
    /// Content for the component
    /// </summary>
    [Parameter]
    public ImageContent? Content { get; set; }
    [Inject]
    private IBlogDataProvider DataProvider { get; set; } = null!;
}
