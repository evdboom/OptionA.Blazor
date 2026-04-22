using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Playground.UnitTests.Struct;

public class PlaygroundDescriptorTests
{
    [Fact]
    public void PlaygroundDescriptor_InheritsBaseContract()
    {
        // Arrange
        var descriptor = new PlaygroundDescriptor<TestPlaygroundComponent>
        {
            Title = "Demo",
            Parameters =
            [
                new PlaygroundParameterDescriptor
                {
                    Name = "Text",
                    DefaultValue = "Hello"
                }
            ]
        };

        // Act
        PlaygroundDescriptorBase baseDescriptor = descriptor;

        // Assert
        Assert.Equal(typeof(TestPlaygroundComponent), baseDescriptor.ComponentType);
        Assert.Same(descriptor.Parameters, baseDescriptor.Parameters);
    }

    [Fact]
    public void PlaygroundDescriptor_PreservesConfiguredMetadataThroughBaseContract()
    {
        // Arrange
        RenderFragment staticContent = builder => builder.AddContent(0, "Static content");
        var descriptor = new PlaygroundDescriptor<TestPlaygroundComponent>
        {
            Title = "Demo",
            Description = "Descriptor description",
            StaticContent = staticContent
        };

        // Act
        PlaygroundDescriptorBase baseDescriptor = descriptor;

        // Assert
        Assert.Equal("Demo", baseDescriptor.Title);
        Assert.Equal("Descriptor description", baseDescriptor.Description);
        Assert.Same(staticContent, baseDescriptor.StaticContent);
    }

    [Fact]
    public void PlaygroundDescriptor_DefaultState_UsesBaseDefaults()
    {
        // Arrange
        var descriptor = new PlaygroundDescriptor<TestPlaygroundComponent>();

        // Act
        PlaygroundDescriptorBase baseDescriptor = descriptor;

        // Assert
        Assert.Equal(string.Empty, baseDescriptor.Title);
        Assert.Null(baseDescriptor.Description);
        Assert.Null(baseDescriptor.StaticContent);
        Assert.Empty(baseDescriptor.Parameters);
        Assert.Equal(typeof(TestPlaygroundComponent), baseDescriptor.ComponentType);
    }

    public sealed class TestPlaygroundComponent : ComponentBase
    {
        [Parameter]
        public string? Text { get; set; }
    }
}
