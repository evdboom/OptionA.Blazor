using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Playground.UnitTests.Components;

public class OptAPlaygroundDescriptorIdTests : BunitContext
{
    private readonly Mock<IPlaygroundDataProvider> _playgroundDataProvider;

    public OptAPlaygroundDescriptorIdTests()
    {
        _playgroundDataProvider = new Mock<IPlaygroundDataProvider>();
        _playgroundDataProvider.SetupGet(p => p.DefaultInteractiveClass).Returns("interactive-shell");
        _playgroundDataProvider.SetupGet(p => p.DefaultPlaygroundClass).Returns("playground-shell");
        _playgroundDataProvider.SetupGet(p => p.DefaultPreviewClass).Returns("preview-shell");
        _playgroundDataProvider.SetupGet(p => p.DefaultEditorClass).Returns("editor-shell");
        _playgroundDataProvider.SetupGet(p => p.DefaultCodeClass).Returns("code-shell");
        _playgroundDataProvider.SetupGet(p => p.DefaultEditorLabelClass).Returns("label-shell");
        _playgroundDataProvider.SetupGet(p => p.DefaultEditorInputClass).Returns("input-shell");
        _playgroundDataProvider.SetupGet(p => p.DefaultEditorGroupClass).Returns("group-shell");
        _playgroundDataProvider.SetupGet(p => p.DefaultLayout).Returns(PlaygroundLayout.SideBySide);
        _playgroundDataProvider.SetupGet(p => p.CodeEditingEnabled).Returns(true);
        _playgroundDataProvider.SetupGet(p => p.PreferredCodeEditor).Returns(PlaygroundEditorKind.TextArea);
        _playgroundDataProvider.SetupGet(p => p.DefaultCodeLanguage).Returns("razor");
        _playgroundDataProvider.SetupGet(p => p.EnabledExportFormats).Returns([PlaygroundExportFormat.Razor]);

        Services.AddSingleton(_playgroundDataProvider.Object);
    }

    private IPlaygroundRegistry BuildAndRegisterRegistry(Action<IServiceCollection> configure)
    {
        var sc = new ServiceCollection();
        configure(sc);
        var registry = sc.BuildServiceProvider().GetRequiredService<IPlaygroundRegistry>();
        Services.AddSingleton(registry);
        return registry;
    }

    [Fact]
    public void OptAPlayground_DescriptorId_KnownId_ResolvesAndRendersDescriptor()
    {
        // Arrange
        var descriptor = new PlaygroundDescriptor<TestComponent>
        {
            Title = "Registry demo",
            Parameters =
            [
                new PlaygroundParameterDescriptor { Name = "Label", DefaultValue = "Hello" }
            ]
        };
        BuildAndRegisterRegistry(sc => sc.AddPlayground("reg-demo", descriptor));

        // Act
        var cut = Render<OptAPlayground>(parameters => parameters
            .Add(p => p.DescriptorId, "reg-demo"));

        // Assert: preview uses the registered descriptor (seeds CurrentParameters)
        var previewParams = cut.FindComponent<OptAPlaygroundPreview>().Instance.CurrentParameters;
        Assert.True(previewParams.ContainsKey("Label"));
        Assert.Equal("Hello", previewParams["Label"]);
    }

    [Fact]
    public void OptAPlayground_DescriptorId_UnknownId_CascadesEmptyParameters()
    {
        // Arrange
        BuildAndRegisterRegistry(sc => sc.AddPlayground("known", new PlaygroundDescriptor<TestComponent>()));

        // Act
        var cut = Render<OptAPlayground>(parameters => parameters
            .Add(p => p.DescriptorId, "does-not-exist"));

        // Assert: unknown id falls back to null descriptor → empty parameters
        Assert.Empty(cut.FindComponent<OptAPlaygroundPreview>().Instance.CurrentParameters);
    }

    [Fact]
    public void OptAPlayground_DescriptorId_TakesPrecedenceOverDescriptorParameter()
    {
        // Arrange
        var registryDescriptor = new PlaygroundDescriptor<TestComponent>
        {
            Title = "From Registry",
            Parameters = [new PlaygroundParameterDescriptor { Name = "RegistryKey", DefaultValue = "rv" }]
        };
        var directDescriptor = new PlaygroundDescriptor<TestComponent>
        {
            Title = "Direct",
            Parameters = [new PlaygroundParameterDescriptor { Name = "DirectKey", DefaultValue = "dv" }]
        };

        BuildAndRegisterRegistry(sc => sc.AddPlayground("my-id", registryDescriptor));

        // Act
        var cut = Render<OptAPlayground>(parameters => parameters
            .Add(p => p.DescriptorId, "my-id")
            .Add(p => p.Descriptor, directDescriptor));

        // Assert: registry descriptor wins
        var previewParams = cut.FindComponent<OptAPlaygroundPreview>().Instance.CurrentParameters;
        Assert.True(previewParams.ContainsKey("RegistryKey"));
        Assert.False(previewParams.ContainsKey("DirectKey"));
    }

    [Fact]
    public void OptAPlayground_WithoutRegistry_DescriptorIdIgnored_DirectDescriptorUsed()
    {
        // Arrange: no IPlaygroundRegistry registered at all
        var descriptor = new PlaygroundDescriptor<TestComponent>
        {
            Title = "Direct",
            Parameters = [new PlaygroundParameterDescriptor { Name = "Text", DefaultValue = "world" }]
        };

        // Act
        var cut = Render<OptAPlayground>(parameters => parameters
            .Add(p => p.DescriptorId, "any-id")
            .Add(p => p.Descriptor, descriptor));

        // Assert: falls back to Descriptor since registry is absent
        var previewParams = cut.FindComponent<OptAPlaygroundPreview>().Instance.CurrentParameters;
        Assert.True(previewParams.ContainsKey("Text"));
        Assert.Equal("world", previewParams["Text"]);
    }

    public sealed class TestComponent : ComponentBase
    {
        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public string? Text { get; set; }
    }
}
