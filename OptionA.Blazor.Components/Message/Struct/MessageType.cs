namespace OptionA.Blazor.Components;

/// <summary>
/// Types of messages, for settings message classes
/// </summary>
public enum MessageType
{
    /// <summary>
    /// Info, most common type
    /// </summary>
    Info,
    /// <summary>
    /// Message for when something is wrong, but not critical
    /// </summary>
    Warning,
    /// <summary>
    /// Message for when something is really wrong
    /// </summary>
    Error,
    /// <summary>
    /// Message for when something went right
    /// </summary>
    Success
}
