using System.Text.Json.Serialization;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Enum for different code markers
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [Flags]
    public enum MarkerType
    {
        /// <summary>
        /// No marker
        /// </summary>
        None = 0,
        /// <summary>
        /// Marker to tell the code after should be marked as selected
        /// </summary>
        Selection = 1
    }
}
