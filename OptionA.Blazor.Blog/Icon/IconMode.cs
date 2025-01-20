using System.Text.Json.Serialization;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Way to display icon
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum IconMode
{
    /// <summary>
    /// Icon is determined by a CSS class
    /// </summary>
    Class,
    /// <summary>
    /// Path for icon is provided in content
    /// </summary>
    Path
}
