using System.Text.Json.Serialization;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Enum to determine how to render the icon
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum IconMode
    {
        /// <summary>
        /// Render from class
        /// </summary>
        IconClass,
        /// <summary>
        /// Render from paths
        /// </summary>
        Path
    }
}
