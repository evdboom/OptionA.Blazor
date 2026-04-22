namespace OptionA.Blazor.Playground;

/// <summary>
/// Overall options class for playground services.
/// </summary>
public class OptAPlaygroundOptions
{
    /// <summary>
    /// Configuration for playground components.
    /// </summary>
    public Action<PlaygroundOptions>? PlaygroundConfiguration { get; set; }
}
