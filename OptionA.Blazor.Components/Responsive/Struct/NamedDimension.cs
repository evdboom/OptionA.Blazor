namespace OptionA.Blazor.Components;

/// <summary>
/// Struct for passing window dimensions between javascript and .Net together with a name to categorize
/// </summary>
public struct NamedDimension
{
    /// <summary>
    /// Name of the dimension bandwidth that holds these values
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Width for the dimension in pixels
    /// </summary>
    public int Width { get; set; }
    /// <summary>
    /// Height for the dimension in pixels
    /// </summary>
    public int Height { get; set; }
}
