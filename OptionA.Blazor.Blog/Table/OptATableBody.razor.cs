using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog;

public partial class OptATableBody
{
    /// <summary>
    /// Content for the component
    /// </summary>
    [Parameter]
    public TableContent? Content { get; set; }
}