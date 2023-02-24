using System.Text.Json.Serialization;

namespace LandaPacs.Storage.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StorageLocation
    {
        Local,
        Session
    }
}
