using System.Collections.Concurrent;

namespace OptionA.Blazor.Playground;

/// <summary>
/// Thread-safe, in-memory implementation of <see cref="IPlaygroundRegistry"/>.
/// </summary>
internal sealed class PlaygroundRegistry : IPlaygroundRegistry
{
    private readonly ConcurrentDictionary<string, PlaygroundDescriptorBase> _entries = new(StringComparer.Ordinal);

    /// <inheritdoc/>
    public void Register(string id, PlaygroundDescriptorBase descriptor)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentNullException.ThrowIfNull(descriptor);

        _entries[id] = descriptor;
    }

    /// <inheritdoc/>
    public bool TryGet(string id, out PlaygroundDescriptorBase? descriptor)
    {
        if (_entries.TryGetValue(id, out var found))
        {
            descriptor = found;
            return true;
        }

        descriptor = null;
        return false;
    }
}
