using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Body for a table
/// </summary>
public partial class OptATableBody
{
    /// <summary>
    /// Content for the component
    /// </summary>
    [Parameter]
    public TableContent? Content { get; set; }
}