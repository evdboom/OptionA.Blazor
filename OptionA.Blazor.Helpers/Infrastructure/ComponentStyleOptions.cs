using OptionA.Blazor.Helpers.Contracts;

namespace OptionA.Blazor.Helpers.Infrastructure;

/// <summary>
/// Configuration for component styling.
/// </summary>
public class ComponentStyleOptions
{
    /// <summary>
    /// Per-element style overrides.
    /// </summary>
    public Dictionary<ComponentElementType, ComponentElementProperties> ComponentStyles { get; set; } = new();
}
