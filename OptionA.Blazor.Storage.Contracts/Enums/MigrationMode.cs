using System.Text.Json.Serialization;

namespace LandaPacs.Storage.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MigrationMode
    {
        Add,
        Remove,
        Update,
        Clear
    }
}
