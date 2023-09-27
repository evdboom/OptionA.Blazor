using System.Text.Json.Serialization;

namespace OptionA.Blazor.Storage
{
    /// <summary>
    /// Storage locations of the browser storage
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StorageLocation
    {
        /// <summary>
        /// Local is persistent over browser sessions
        /// </summary>
        Local,
        /// <summary>
        /// Session storage is linked to current browser session
        /// </summary>
        Session
    }
}
