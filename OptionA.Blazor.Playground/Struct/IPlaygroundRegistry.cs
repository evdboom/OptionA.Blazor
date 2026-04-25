namespace OptionA.Blazor.Playground;

/// <summary>
/// Registry that maps string identifiers to <see cref="PlaygroundDescriptorBase"/> instances.
/// </summary>
public interface IPlaygroundRegistry
{
    /// <summary>
    /// Registers a descriptor under the given identifier.
    /// If the id is already registered, the existing entry is replaced.
    /// </summary>
    /// <param name="id">The unique identifier for the descriptor.</param>
    /// <param name="descriptor">The descriptor to register.</param>
    void Register(string id, PlaygroundDescriptorBase descriptor);

    /// <summary>
    /// Attempts to retrieve the descriptor registered under <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The identifier to look up.</param>
    /// <param name="descriptor">
    /// When this method returns <see langword="true"/>, contains the registered descriptor;
    /// otherwise <see langword="null"/>.
    /// </param>
    /// <returns><see langword="true"/> if a descriptor was found; otherwise <see langword="false"/>.</returns>
    bool TryGet(string id, out PlaygroundDescriptorBase? descriptor);
}
