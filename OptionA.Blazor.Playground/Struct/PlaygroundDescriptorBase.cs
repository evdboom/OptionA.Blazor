using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Playground;

/// <summary>
/// Non-generic contract that describes a component playground definition.
/// </summary>
public abstract class PlaygroundDescriptorBase
{
    /// <summary>
    /// Gets or sets the title displayed for the playground.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the descriptive text for the playground.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets optional static content rendered alongside the component preview.
    /// </summary>
    public RenderFragment? StaticContent { get; set; }

    /// <summary>
    /// Gets or sets the configurable parameters exposed by the playground.
    /// </summary>
    public IList<PlaygroundParameterDescriptor> Parameters { get; set; } = [];

    /// <summary>
    /// Gets the component type represented by this descriptor.
    /// </summary>
    public abstract Type ComponentType { get; }
}
