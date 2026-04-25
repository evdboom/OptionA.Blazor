using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace OptionA.Blazor.Playground.UnitTests.Struct;

public class PlaygroundRegistryTests
{
    private static IPlaygroundRegistry BuildRegistry(Action<IServiceCollection>? configure = null)
    {
        var services = new ServiceCollection();
        configure?.Invoke(services);
        var provider = services.BuildServiceProvider();
        return provider.GetRequiredService<IPlaygroundRegistry>();
    }

    [Fact]
    public void Register_And_TryGet_ReturnsSameDescriptor()
    {
        // Arrange
        var descriptor = CreateDescriptor("first");
        var services = new ServiceCollection();
        services.AddPlayground("my-component", descriptor);
        var registry = services.BuildServiceProvider().GetRequiredService<IPlaygroundRegistry>();

        // Act
        var found = registry.TryGet("my-component", out var result);

        // Assert
        Assert.True(found);
        Assert.Same(descriptor, result);
    }

    [Fact]
    public void TryGet_UnknownId_ReturnsFalseAndNullDescriptor()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddPlayground("known", CreateDescriptor("x"));
        var registry = services.BuildServiceProvider().GetRequiredService<IPlaygroundRegistry>();

        // Act
        var found = registry.TryGet("unknown", out var result);

        // Assert
        Assert.False(found);
        Assert.Null(result);
    }

    [Fact]
    public void Register_SameId_ReplacesDescriptor()
    {
        // Arrange
        var first = CreateDescriptor("first");
        var second = CreateDescriptor("second");
        var services = new ServiceCollection();
        services.AddPlayground("id", first);
        var registry = services.BuildServiceProvider().GetRequiredService<IPlaygroundRegistry>();

        // Act: replace via Register directly
        registry.Register("id", second);
        registry.TryGet("id", out var result);

        // Assert
        Assert.Same(second, result);
    }

    [Fact]
    public void Register_MultipleIds_EachResolvedIndependently()
    {
        // Arrange
        var a = CreateDescriptor("a");
        var b = CreateDescriptor("b");
        var services = new ServiceCollection();
        services.AddPlayground("id-a", a);
        services.AddPlayground("id-b", b);
        var registry = services.BuildServiceProvider().GetRequiredService<IPlaygroundRegistry>();

        // Assert
        Assert.True(registry.TryGet("id-a", out var resultA));
        Assert.True(registry.TryGet("id-b", out var resultB));
        Assert.Same(a, resultA);
        Assert.Same(b, resultB);
    }

    [Fact]
    public void Register_NullOrWhitespaceId_Throws()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddPlayground("seed", CreateDescriptor("x"));
        var registry = services.BuildServiceProvider().GetRequiredService<IPlaygroundRegistry>();

        // Act / Assert
        Assert.Throws<ArgumentException>(() => registry.Register("", CreateDescriptor("y")));
        Assert.Throws<ArgumentException>(() => registry.Register("   ", CreateDescriptor("y")));
    }

    [Fact]
    public void Register_NullDescriptor_Throws()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddPlayground("seed", CreateDescriptor("x"));
        var registry = services.BuildServiceProvider().GetRequiredService<IPlaygroundRegistry>();

        // Act / Assert
        Assert.Throws<ArgumentNullException>(() => registry.Register("id", null!));
    }

    [Fact]
    public void Register_IsCaseSensitive()
    {
        // Arrange
        var descriptor = CreateDescriptor("x");
        var services = new ServiceCollection();
        services.AddPlayground("MyId", descriptor);
        var registry = services.BuildServiceProvider().GetRequiredService<IPlaygroundRegistry>();

        // Act
        var foundExact = registry.TryGet("MyId", out _);
        var foundLower = registry.TryGet("myid", out _);

        // Assert
        Assert.True(foundExact);
        Assert.False(foundLower);
    }

    private static PlaygroundDescriptorBase CreateDescriptor(string title) =>
        new PlaygroundDescriptor<TestRegistryComponent> { Title = title };

    public sealed class TestRegistryComponent : ComponentBase
    {
        [Parameter]
        public string? Text { get; set; }
    }
}
