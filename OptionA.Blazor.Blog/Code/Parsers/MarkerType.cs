namespace OptionA.Blazor.Blog.Code.Parsers;

/// <summary>
/// Enum for different code markers
/// </summary>
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
    Selection = 1,
    /// <summary>
    /// Marker to tell the following word is a class
    /// </summary>
    Class = 2,
    /// <summary>
    /// Marker to tell the following word is an interface
    /// </summary>
    Interface = 4,
    /// <summary>
    /// Marker to tell the following word is an enum
    /// </summary>
    Enum = 8,
    /// <summary>
    /// Marker to tell the following word is a struct
    /// </summary>
    Struct = 16,
}
