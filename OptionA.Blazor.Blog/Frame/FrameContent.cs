namespace OptionA.Blazor.Blog;

/// <summary>
/// Content for the frame component
/// </summary>
public class FrameContent : Content
{
    /// <summary>
    /// Source for the frame
    /// </summary>
    public string Source { get; set; } = string.Empty;
    /// <summary>
    /// Title for the frame
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// Width of the frame
    /// </summary>
    public string? Width { get; set; }
    /// <summary>
    /// Height for the frame
    /// </summary>
    public string? Height { get; set; }      
    /// <summary>
    /// Set to true to display a block of the size of the iframe without trying to load the actual Iframe
    /// </summary>
    public bool PreviewMode { get; set; }
    /// <inheritdoc/>
    public override ContentType Type => ContentType.Frame;
    /// <inheritdoc/>
    public override Dictionary<string, object?> Attributes
    {
        get
        {
            var result = new Dictionary<string, object?>
            {
                ["src"] = Source,
            };
            result["title"] = Title ?? Source;
            if (!string.IsNullOrEmpty(Width))
            {
                result["width"] = Width;
            }
            if (!string.IsNullOrEmpty(Height))
            {
                result["height"] = Height;
            }                

            foreach (var attribute in base.Attributes)
            {
                result[attribute.Key] = attribute.Value;
            }
            return result;
        }
    }

    /// <inheritdoc/>
    public override bool IsInvalid => string.IsNullOrEmpty(Source);
}
