namespace OptionA.Blazor.Components;

/// <summary>
/// Options for the repsonsive service and responsive component
/// </summary>
public class ResponsiveOptions
{
    /// <summary>
    /// Size threshold (lowest should be on 0) for each wanted trigger dimension
    /// </summary>
    public Dictionary<int, string>? Sizes { get; set; }
}
