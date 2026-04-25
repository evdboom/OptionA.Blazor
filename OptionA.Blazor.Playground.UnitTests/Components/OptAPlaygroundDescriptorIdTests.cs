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

        // Register the registry + resolver defaults. AddOptionAPlayground uses TryAdd
        // so it will not override the mock data provider above.
        Services.AddOptionAPlayground();
    }

    private void BuildAndRegisterRegistry(Action<IServiceCollection> configure)
    {
        var sc = new ServiceCollection();
        configure(sc);
        var registry = sc.BuildServiceProvider().GetRequiredService<IPlaygroundRegistry>();

        // Replace the default empty registry (added by AddOptionAPlayground in constructor)
        // with the test-specific one so the resolver picks it up on first resolution.
        Services.AddSingleton(registry);
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
    public void OptAPlayground_DescriptorId_UnknownId_FallsBackToDirectDescriptor()
    {
        // Arrange
        var directDescriptor = new PlaygroundDescriptor<TestComponent>
        {
            Title = "Direct",
            Parameters = [new PlaygroundParameterDescriptor { Name = "Text", DefaultValue = "fallback" }]
        };
        BuildAndRegisterRegistry(sc => sc.AddPlayground("known", new PlaygroundDescriptor<TestComponent>()));

        // Act
        var cut = Render<OptAPlayground>(parameters => parameters
            .Add(p => p.DescriptorId, "does-not-exist")
            .Add(p => p.Descriptor, directDescriptor));

        // Assert
        var previewParams = cut.FindComponent<OptAPlaygroundPreview>().Instance.CurrentParameters;
        Assert.True(previewParams.ContainsKey("Text"));
        Assert.Equal("fallback", previewParams["Text"]);
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
    public void OptAPlayground_DescriptorId_EmptyRegistry_FallsBackToDirectDescriptor()
    {
        // Arrange: registry is present (registered by AddOptionAPlayground) but has no matching id
        var descriptor = new PlaygroundDescriptor<TestComponent>
        {
            Title = "Direct",
            Parameters = [new PlaygroundParameterDescriptor { Name = "Text", DefaultValue = "world" }]
        };

        // Act
        var cut = Render<OptAPlayground>(parameters => parameters
            .Add(p => p.DescriptorId, "any-id")
            .Add(p => p.Descriptor, descriptor));

        // Assert: id is not in the registry → falls back to Descriptor
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
