using System.Diagnostics.CodeAnalysis;

namespace OptionA.Blazor.Blog.Text.Parser;

/// <summary>
/// Markdown bold part
/// </summary>
public class LinkMarker : MarkerDefinition
{
    /// <inheritdoc/>
    public override int Priority => 30;
    /// <inheritdoc/>
    public override string Starter => "[";
    /// <inheritdoc/>
    public override string Ender => ")";
    /// <inheritdoc/>
    public override MarkerType Type => MarkerType.Link;
    /// <inheritdoc/>
    public override bool IsValidForMarker(string input, [NotNullWhen(true)] out string? content)
    {
        if (string.IsNullOrEmpty(input) || input.Length < 4)
        {
            content = null;
            return false;
        }
        else if (input.StartsWith(Starter))
        {                
            var end = input[Starter.Length..].IndexOf(Ender);
            var split = input[Starter.Length..].IndexOf("](");

            if (end > 0 && split > 0 && end > split)
            {
                var endIndex = end + Starter.Length;                    
                if (input[endIndex - 1] != '\\')
                {
                    content = input[Starter.Length..endIndex];
                    return true;
                }
            }
        }

        content = null;
        return false;
    }

    /// <inheritdoc/>
    public override IContent CreateContent(string content)
    {
        var parts = content.Split("](");
        var target = parts[1].StartsWith("/")
            ? "_self"
            : "_blank";
        return new LinkContent
        {
            Content = parts[0],
            Href = parts[1],
            Target = target
        };
    }
}
