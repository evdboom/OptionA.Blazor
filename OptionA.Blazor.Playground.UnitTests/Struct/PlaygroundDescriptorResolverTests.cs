using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace OptionA.Blazor.Playground.UnitTests.Struct;

public class PlaygroundDescriptorResolverTests
{
    [Fact]
    public void AddOptionAPlayground_RegistersDescriptorResolver_AsResolvable()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddOptionAPlayground();
        using var provider = services.BuildServiceProvider();
        var resolver = provider.GetService<IPlaygroundDescriptorResolver>();

        // Assert
        Assert.NotNull(resolver);
    }

    [Fact]
    public void Resolve_KnownId_ReturnsRegistryDescriptor_OverFallback()
    {
        // Arrange
        var services = new ServiceCollection();
        var registryDescriptor = CreateDescriptor("registry");
        var fallbackDescriptor = CreateDescriptor("fallback");

        services.AddPlayground("demo-id", registryDescriptor);
        using var provider = services.BuildServiceProvider();
        var resolver = provider.GetRequiredService<IPlaygroundDescriptorResolver>();

        // Act
        var result = resolver.Resolve("demo-id", fallbackDescriptor);

        // Assert
        Assert.Same(registryDescriptor, result);
    }

    [Fact]
    public void Resolve_UnknownId_ReturnsFallbackDescriptor()
    {
        // Arrange
        var services = new ServiceCollection();
        var fallbackDescriptor = CreateDescriptor("fallback");

        services.AddOptionAPlayground();
        using var provider = services.BuildServiceProvider();
        var resolver = provider.GetRequiredService<IPlaygroundDescriptorResolver>();

        // Act
        var result = resolver.Resolve("missing-id", fallbackDescriptor);

        // Assert
        Assert.Same(fallbackDescriptor, result);
    }

    [Fact]
    public void Resolve_NullId_ReturnsFallbackDescriptor()
    {
        // Arrange
        var services = new ServiceCollection();
        var fallbackDescriptor = CreateDescriptor("fallback");

        services.AddOptionAPlayground();
        using var provider = services.BuildServiceProvider();
        var resolver = provider.GetRequiredService<IPlaygroundDescriptorResolver>();

        // Act
        var result = resolver.Resolve(null, fallbackDescriptor);

        // Assert
        Assert.Same(fallbackDescriptor, result);
    }

    [Fact]
    public void Resolve_UnknownId_WithoutFallback_ReturnsNull()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddOptionAPlayground();
        using var provider = services.BuildServiceProvider();
        var resolver = provider.GetRequiredService<IPlaygroundDescriptorResolver>();

        // Act
        var result = resolver.Resolve("missing-id", null);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void AddOptionAPlayground_WithRegistryRegisteredLater_ResolverUsesLatestRegistry()
    {
        // Arrange
        var services = new ServiceCollection();
        var registry = new TestPlaygroundRegistry();
        var registryDescriptor = CreateDescriptor("registry");
        var fallbackDescriptor = CreateDescriptor("fallback");

        services.AddOptionAPlayground();
        registry.Register("demo-id", registryDescriptor);
        services.AddSingleton<IPlaygroundRegistry>(registry);

        using var provider = services.BuildServiceProvider();
        var resolver = provider.GetRequiredService<IPlaygroundDescriptorResolver>();

        // Act
        var result = resolver.Resolve("demo-id", fallbackDescriptor);

        // Assert
        Assert.Same(registryDescriptor, result);
        Assert.Same(registry, provider.GetRequiredService<IPlaygroundRegistry>());
    }

    private static PlaygroundDescriptorBase CreateDescriptor(string title) =>
        new PlaygroundDescriptor<TestPlaygroundComponent> { Title = title };

    private sealed class TestPlaygroundRegistry : IPlaygroundRegistry
    {
        private readonly Dictionary<string, PlaygroundDescriptorBase> _entries = [];

        public void Register(string id, PlaygroundDescriptorBase descriptor)
        {
            _entries[id] = descriptor;
        }

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

    private sealed class TestPlaygroundComponent : ComponentBase
    {
        [Parameter]
        public string? Text { get; set; }
    }
}
