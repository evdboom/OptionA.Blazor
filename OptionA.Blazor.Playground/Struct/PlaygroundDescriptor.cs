using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Playground;

/// <summary>
/// Describes a component playground for a specific Blazor component type.
/// </summary>
/// <typeparam name="TComponent">The component type rendered by the playground.</typeparam>
public class PlaygroundDescriptor<TComponent>
    : PlaygroundDescriptorBase
    where TComponent : ComponentBase
{
    /// <inheritdoc/>
    public override Type ComponentType => typeof(TComponent);
}
