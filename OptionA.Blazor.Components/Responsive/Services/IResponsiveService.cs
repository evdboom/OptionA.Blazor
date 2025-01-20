namespace OptionA.Blazor.Components;

/// <summary>
/// Interface for interacting with the windowsize changed (onresize) event in javascript to .Net
/// </summary>
public interface IResponsiveService
{
    /// <summary>
    /// Event that is invoked every time a dimension is changed
    /// </summary>
    event EventHandler<NamedDimension>? WindowSizeChanged;
    /// <summary>
    /// Event that is invoked every time a dimension threshold is passed (e.g. moving from medium to large)
    /// </summary>
    event EventHandler<string>? DimensionChanged;
    /// <summary>
    /// Returns the current windowsize
    /// </summary>
    /// <returns></returns>
    NamedDimension GetWindowSize();
    /// <summary>
    /// Returns true if the current width of the window larger then the threshold for the given dimension
    /// </summary>
    /// <param name="targetDimension"></param>
    /// <returns></returns>
    bool CurrentWidthEnoughForDimension(string targetDimension);
    /// <summary>
    /// Initializes the responsive service, do this as soon as possible as it will connect with javascript.
    /// </summary>
    /// <returns></returns>
    Task Initialize();
    /// <summary>
    /// Returns all dimensions valid for the current width
    /// </summary>
    /// <returns></returns>
    IEnumerable<string> ValidDimensions();
    /// <summary>
    /// Returns all dimension break points known for this responsive service
    /// </summary>
    /// <returns></returns>
    IEnumerable<(string Name, int Width)> GetAllDimensionBreakPoints();
}
