using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Header for a table
/// </summary>
public partial class OptATableHeader
{
    /// <summary>
    /// Content for the component
    /// </summary>
    [Parameter]
    public TableContent? Content { get; set; }
    private IEnumerable<IContent>? _content;

    /// <inheritdoc/>
    protected override async Task OnParametersSetAsync()
    {
        if (Content is null)
        {
            return;
        }

        _content = Content.Headers.Select(item => new InlineContent { Content = item });

        await InvokeAsync(StateHasChanged);
    }
}