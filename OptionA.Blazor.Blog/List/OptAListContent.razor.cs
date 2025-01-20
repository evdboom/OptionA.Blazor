using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Content for list items
/// </summary>
public partial class OptAListContent
{
    /// <summary>
    /// Content for the component
    /// </summary>
    [Parameter]
    public ListContent? Content { get; set; }

    private IEnumerable<IContent>? _content;

    /// <inheritdoc/>
    protected override async Task OnParametersSetAsync()
    {
        if (Content is null)
        {
            return;
        }

        _content = Content.Items
                .Where(item => !string.IsNullOrEmpty(item))
                .Select(item => new InlineContent { Content = item });

        await InvokeAsync(StateHasChanged);
    }
}