using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using OptionA.Blazor.Interactive;
using OptionA.Blazor.Playground;

namespace OptionA.Blazor.Playground.UnitTests.Components;

public class OptAInteractiveTests : BunitContext
{
    public OptAInteractiveTests()
    {
        Services.AddOptionABootstrapInteractive();
    }

    [Fact]
    public void OptAInteractive_RendersThePlaygroundBridgeAndAttributes()
    {
        // Arrange
        var descriptor = CreateDescriptor();

        // Act
        var cut = Render<OptAInteractive>(parameters => parameters
            .Add(p => p.Descriptor, descriptor)
            .Add(p => p.Layout, PlaygroundLayout.Stacked));

        // Assert
        var wrapper = cut.Find("div[opta-interactive]");
        Assert.Contains("card", wrapper.ClassList);
        Assert.Equal("true", wrapper.GetAttribute("code-editing-enabled"));
        Assert.Equal("textarea", wrapper.GetAttribute("preferred-code-editor"));
        Assert.NotNull(cut.FindComponent<OptAPlayground>());
        Assert.True(cut.Find("div[opta-playground]").HasAttribute("stacked"));
    }

    private static PlaygroundDescriptorBase CreateDescriptor()
    {
        return new PlaygroundDescriptor<TestInteractiveComponent>
        {
            Title = "Interactive",
            Parameters =
            [
                new PlaygroundParameterDescriptor
                {
                    Name = nameof(TestInteractiveComponent.Title),
                    DefaultValue = "Initial title"
                }
            ]
        };
    }

    private sealed class TestInteractiveComponent : ComponentBase
    {
        [Parameter]
        public string? Title { get; set; }
    }
}
