using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Table to display
/// </summary>
public partial class OptATable
{
    /// <summary>
    /// Quote to display
    /// </summary>
    [Parameter]
    public TableContent? Content { get; set; }
    [Inject]
    private IBlogDataProvider DataProvider { get; set; } = null!;
}