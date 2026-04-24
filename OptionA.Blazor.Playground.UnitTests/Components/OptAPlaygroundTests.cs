using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Playground.UnitTests.Components;

public class OptAPlaygroundTests : BunitContext
{
    private readonly Mock<IPlaygroundDataProvider> _playgroundDataProvider;

    public OptAPlaygroundTests()
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
        _playgroundDataProvider.SetupGet(p => p.EnabledExportFormats).Returns([PlaygroundExportFormat.Razor, PlaygroundExportFormat.Json]);

        Services.AddSingleton(_playgroundDataProvider.Object);
    }

    [Fact]
    public void OptAPlayground_RendersChildComponentsAndUsesProviderLayoutByDefault()
    {
        // Arrange
        var descriptor = CreateDescriptor();

        // Act
        var cut = Render<OptAPlayground>(parameters => parameters
            .Add(p => p.Descriptor, descriptor));

        // Assert
        var container = cut.Find("div[opta-playground]");
        Assert.False(container.HasAttribute("stacked"));
        Assert.Single(cut.FindComponents<OptAPlaygroundPreview>());
        Assert.Single(cut.FindComponents<OptAPlaygroundEditor>());
        Assert.Single(cut.FindComponents<OptAPlaygroundCode>());
        Assert.NotNull(cut.Find("pre[opta-playground-code]"));
    }

    [Fact]
    public void OptAPlayground_UsesExplicitLayoutWhenProvided()
    {
        // Arrange
        var descriptor = CreateDescriptor();

        // Act
        var cut = Render<OptAPlayground>(parameters => parameters
            .Add(p => p.Descriptor, descriptor)
            .Add(p => p.Layout, PlaygroundLayout.Stacked));

        // Assert
        var container = cut.Find("div[opta-playground]");
        Assert.True(container.HasAttribute("stacked"));
    }

    [Fact]
    public void OptAPlayground_EmitsInteractiveMetadataAttributes()
    {
        // Arrange
        var descriptor = CreateDescriptor();

        // Act
        var cut = Render<OptAPlayground>(parameters => parameters
            .Add(p => p.Descriptor, descriptor));

        // Assert
        var container = cut.Find("div[opta-playground]");
        Assert.Equal("true", container.GetAttribute("code-editing-enabled"));
        Assert.Equal("textarea", container.GetAttribute("preferred-code-editor"));
        Assert.Equal("razor", container.GetAttribute("default-code-language"));
        Assert.Equal("razor,json", container.GetAttribute("export-formats"));
    }

    [Fact]
    public async Task OptAPlayground_SeedsAndUpdatesCascadedParameterDictionary()
    {
        // Arrange
        var descriptor = CreateDescriptor();
        var cut = Render<OptAPlayground>(parameters => parameters
            .Add(p => p.Descriptor, descriptor));
        var editor = cut.FindComponent<OptAPlaygroundEditor>();

        // Act
        await cut.InvokeAsync(() => editor.Instance.ValueChanged.InvokeAsync(("Title", "Updated title")));

        // Assert
        Assert.Equal("Updated title", cut.FindComponent<OptAPlaygroundPreview>().Instance.CurrentParameters["Title"]);
        Assert.Equal("Updated title", cut.FindComponent<OptAPlaygroundCode>().Instance.CurrentParameters["Title"]);
    }

    [Fact]
    public void OptAPlayground_WithNullDescriptor_CascadesEmptyParameterDictionary()
    {
        // Act
        var cut = Render<OptAPlayground>();

        // Assert
        Assert.Empty(cut.FindComponent<OptAPlaygroundPreview>().Instance.CurrentParameters);
        Assert.Empty(cut.FindComponent<OptAPlaygroundEditor>().Instance.CurrentParameters);
        Assert.Empty(cut.FindComponent<OptAPlaygroundCode>().Instance.CurrentParameters);
    }

    [Fact]
    public async Task OptAPlayground_WhenDescriptorChanges_ResetsCascadedParametersToNewDefaults()
    {
        // Arrange
        var initialDescriptor = CreateDescriptor();
        var replacementDescriptor = new PlaygroundDescriptor<TestPlaygroundComponent>
        {
            Title = "Replacement playground",
            Parameters =
            [
                new PlaygroundParameterDescriptor
                {
                    Name = "Count",
                    DefaultValue = 3
                }
            ]
        };

        var host = Render<PlaygroundHost>(parameters => parameters
            .Add(p => p.Descriptor, initialDescriptor));
        var playground = host.FindComponent<OptAPlayground>();
        var editor = playground.FindComponent<OptAPlaygroundEditor>();
        await host.InvokeAsync(() => editor.Instance.ValueChanged.InvokeAsync(("Title", "Updated title")));

        // Act
        host.Instance.Descriptor = replacementDescriptor;
        host.Render();

        // Assert
        playground = host.FindComponent<OptAPlayground>();
        var previewParameters = playground.FindComponent<OptAPlaygroundPreview>().Instance.CurrentParameters;
        var editorParameters = playground.FindComponent<OptAPlaygroundEditor>().Instance.CurrentParameters;
        var codeParameters = playground.FindComponent<OptAPlaygroundCode>().Instance.CurrentParameters;

        Assert.False(previewParameters.ContainsKey("Title"));
        Assert.Equal(3, previewParameters["Count"]);
        Assert.False(editorParameters.ContainsKey("Title"));
        Assert.Equal(3, editorParameters["Count"]);
        Assert.False(codeParameters.ContainsKey("Title"));
        Assert.Equal(3, codeParameters["Count"]);
    }

    private static PlaygroundDescriptorBase CreateDescriptor()
    {
        return new PlaygroundDescriptor<TestPlaygroundComponent>
        {
            Title = "Playground",
            Parameters =
            [
                new PlaygroundParameterDescriptor
                {
                    Name = "Title",
                    DefaultValue = "Initial title"
                }
            ]
        };
    }

    public sealed class TestPlaygroundComponent : ComponentBase
    {
        [Parameter]
        public string? Title { get; set; }
    }

    public sealed class PlaygroundHost : ComponentBase
    {
        [Parameter]
        public PlaygroundDescriptorBase? Descriptor { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<OptAPlayground>(0);
            builder.AddAttribute(1, nameof(OptAPlayground.Descriptor), Descriptor);
            builder.CloseComponent();
        }
    }
}
