using System.Text.Json.Serialization;

namespace OptionA.Blazor.Storage;

/// <summary>
/// Modes for adding migration (indexed)
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MigrationMode
{
    /// <summary>
    /// Add a new value
    /// </summary>
    Add,
    /// <summary>
    /// Remove a value
    /// </summary>
    Remove,
    /// <summary>
    /// Update a value
    /// </summary>
    Update,
    /// <summary>
    /// Clear a value
    /// </summary>
    Clear
}
