using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace OptionA.Blazor.Playground.UnitTests.Configuration;

public class AddPlaygroundExtensionTests
{
    [Fact]
    public void AddPlayground_RegistersIPlaygroundRegistry_AsResolvable()
    {
        // Arrange
        var services = new ServiceCollection();
        var descriptor = new PlaygroundDescriptor<TestComponent> { Title = "demo" };

        // Act
        services.AddPlayground("demo-id", descriptor);
        using var provider = services.BuildServiceProvider();
        var registry = provider.GetService<IPlaygroundRegistry>();

        // Assert
        Assert.NotNull(registry);
    }

    [Fact]
    public void AddPlayground_SingleCall_DescriptorIsResolvableById()
    {
        // Arrange
        var services = new ServiceCollection();
        var descriptor = new PlaygroundDescriptor<TestComponent> { Title = "demo" };

        // Act
        services.AddPlayground("demo-id", descriptor);
        using var provider = services.BuildServiceProvider();
        var registry = provider.GetRequiredService<IPlaygroundRegistry>();
        var found = registry.TryGet("demo-id", out var result);

        // Assert
        Assert.True(found);
        Assert.Same(descriptor, result);
    }

    [Fact]
    public void AddPlayground_MultipleCalls_AllDescriptorsResolvable()
    {
        // Arrange
        var services = new ServiceCollection();
        var d1 = new PlaygroundDescriptor<TestComponent> { Title = "first" };
        var d2 = new PlaygroundDescriptor<TestComponent> { Title = "second" };

        // Act
        services.AddPlayground("id-1", d1);
        services.AddPlayground("id-2", d2);
        using var provider = services.BuildServiceProvider();
        var registry = provider.GetRequiredService<IPlaygroundRegistry>();

        // Assert
        Assert.True(registry.TryGet("id-1", out var r1));
        Assert.True(registry.TryGet("id-2", out var r2));
        Assert.Same(d1, r1);
        Assert.Same(d2, r2);
    }

    [Fact]
    public void AddPlayground_MultipleCalls_ReturnSameSingletonRegistry()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddPlayground("id-1", new PlaygroundDescriptor<TestComponent> { Title = "a" });
        services.AddPlayground("id-2", new PlaygroundDescriptor<TestComponent> { Title = "b" });
        using var provider = services.BuildServiceProvider();

        // Assert: exactly one IPlaygroundRegistry registration.
        Assert.Single(services, sd => sd.ServiceType == typeof(IPlaygroundRegistry));
    }

    [Fact]
    public void AddPlayground_PreRegisteredRegistryInstance_ReusesAndAugmentsSameRegistry()
    {
        // Arrange
        var services = new ServiceCollection();
        var existingRegistry = new TestPlaygroundRegistry();
        var descriptor = new PlaygroundDescriptor<TestComponent> { Title = "demo" };
        services.AddSingleton<IPlaygroundRegistry>(existingRegistry);

        // Act
        services.AddPlayground("demo-id", descriptor);
        using var provider = services.BuildServiceProvider();
        var resolvedRegistry = provider.GetRequiredService<IPlaygroundRegistry>();
        var found = resolvedRegistry.TryGet("demo-id", out var result);

        // Assert
        Assert.Same(existingRegistry, resolvedRegistry);
        Assert.True(found);
        Assert.Same(descriptor, result);
    }

    [Fact]
    public void AddPlayground_NullOrWhitespaceId_Throws()
    {
        // Arrange
        var services = new ServiceCollection();
        var descriptor = new PlaygroundDescriptor<TestComponent> { Title = "demo" };

        // Act / Assert
        Assert.Throws<ArgumentException>(() => services.AddPlayground("", descriptor));
        Assert.Throws<ArgumentException>(() => services.AddPlayground("   ", descriptor));
    }

    [Fact]
    public void AddPlayground_NullDescriptor_Throws()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act / Assert
        Assert.Throws<ArgumentNullException>(() => services.AddPlayground("id", null!));
    }

    public sealed class TestComponent : ComponentBase
    {
        [Parameter]
        public string? Text { get; set; }
    }

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
}
