namespace OptionA.Blazor.Playground;

/// <summary>
/// Default implementation of <see cref="IPlaygroundDescriptorResolver"/> that looks up descriptors
/// in an <see cref="IPlaygroundRegistry"/> and falls back to the directly-supplied descriptor
/// when the id is absent or not registered.
/// </summary>
internal sealed class PlaygroundDescriptorResolver(IPlaygroundRegistry registry) : IPlaygroundDescriptorResolver
{
    /// <inheritdoc/>
    public PlaygroundDescriptorBase? Resolve(string? descriptorId, PlaygroundDescriptorBase? fallback)
    {
        if (descriptorId is not null && registry.TryGet(descriptorId, out var found))
        {
            return found;
        }

        return fallback;
    }
}
