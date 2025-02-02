using Microsoft.AspNetCore.Components;
using System.Text;

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

    private Dictionary<string, object?> GetPreviewAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["opta-frame-preview"] = true
        };

        if (Content != null)
        {
            var sb = new StringBuilder();
            sb.Append($"--opta-frame-width: {Content.Width}px;");
            sb.Append($"--opta-frame-height: {Content.Height}px;");

            result["style"] = sb.ToString();
        }

        return result;
    }

}
