namespace OptionA.Blazor.Blog;

/// <summary>
/// COntent for an image
/// </summary>
public class ImageContent : Content
{
    /// <inheritdoc/>
    public override ContentType Type => ContentType.Image;
    /// <summary>
    /// Image source
    /// </summary>
    public string Source { get; set; } = string.Empty;
    /// <summary>
    /// Title for the image, will be set to source if not provided
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// Alt for the image (if it fails to load), will be set to title if not provided
    /// </summary>
    public string? Alternative { get; set; }
    ///<inheritdoc/>
    public override Dictionary<string, object?> Attributes
    {
        get
        {
            var result = new Dictionary<string, object?>
            {
                ["src"] = Source,
            };
            result["title"] = Title ?? Source;
            result["alt"] = Alternative ?? Title ?? Source;

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
