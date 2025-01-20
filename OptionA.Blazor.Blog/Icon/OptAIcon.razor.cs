using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Icon component
/// </summary>
public partial class OptAIcon
{
    /// <summary>
    /// Content for the component
    /// </summary>
    [Parameter]
    public IconContent? Content { get; set; }
    [Inject]
    private IBlogDataProvider DataProvider { get; set; } = null!;
}
