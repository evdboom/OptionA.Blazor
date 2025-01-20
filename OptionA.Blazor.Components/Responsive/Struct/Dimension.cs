namespace OptionA.Blazor.Components;

/// <summary>
/// Struct for passing window dimensions between javascript and .Net
/// </summary>
public struct Dimension
{
    /// <summary>
    /// Width for the dimension in pixels
    /// </summary>
    public int Width { get; set; }
    /// <summary>
    /// Height for the dimension in pixels
    /// </summary>
    public int Height { get; set; }
}
