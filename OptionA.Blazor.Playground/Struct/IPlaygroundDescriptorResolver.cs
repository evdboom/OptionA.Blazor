namespace OptionA.Blazor.Playground;

/// <summary>
/// Resolves a <see cref="PlaygroundDescriptorBase"/> from a registry id or a direct fallback parameter.
/// </summary>
public interface IPlaygroundDescriptorResolver
{
    /// <summary>
    /// Returns the descriptor that should be used to render the playground surface.
    /// </summary>
    /// <param name="descriptorId">
    /// Optional id to look up in the <see cref="IPlaygroundRegistry"/>.
    /// When non-<see langword="null"/> and the id is found, the registry descriptor is returned
    /// and <paramref name="fallback"/> is ignored.
    /// When <see langword="null"/> or not found, <paramref name="fallback"/> is returned instead.
    /// </param>
    /// <param name="fallback">
    /// The directly-supplied descriptor used when <paramref name="descriptorId"/> cannot be resolved.
    /// </param>
    /// <returns>
    /// The resolved <see cref="PlaygroundDescriptorBase"/>, or <see langword="null"/> when neither
    /// source yields a descriptor.
    /// </returns>
    PlaygroundDescriptorBase? Resolve(string? descriptorId, PlaygroundDescriptorBase? fallback);
}
