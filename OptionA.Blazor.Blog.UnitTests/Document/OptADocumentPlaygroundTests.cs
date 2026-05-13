using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OptionA.Blazor.Blog.Document.Internal;
using OptionA.Blazor.Playground;

namespace OptionA.Blazor.Blog.UnitTests.Document;

/// <summary>
/// bUnit tests for playground directive rendering in <see cref="OptAChild"/>.
/// Covers the resolved descriptor (happy path), error message display, and null content.
/// </summary>
public class OptADocumentPlaygroundTests : BunitContext
{
    public OptADocumentPlaygroundTests()
    {
        // OptAPlayground requires these two services when a descriptor is resolved.
        var dataProvider = new Mock<IPlaygroundDataProvider>();
        dataProvider.Setup(d => d.DefaultLayout).Returns(PlaygroundLayout.SideBySide);
        dataProvider.Setup(d => d.CodeEditingEnabled).Returns(false);
        dataProvider.Setup(d => d.PreferredCodeEditor).Returns(PlaygroundEditorKind.TextArea);
        dataProvider.Setup(d => d.DefaultCodeLanguage).Returns((string?)null);
        dataProvider.Setup(d => d.EnabledExportFormats).Returns(Array.Empty<PlaygroundExportFormat>().ToList().AsReadOnly());
        dataProvider.Setup(d => d.DefaultPlaygroundClass).Returns((string?)null);

        var resolver = new Mock<IPlaygroundDescriptorResolver>();
        resolver
            .Setup(r => r.Resolve(It.IsAny<string?>(), It.IsAny<PlaygroundDescriptorBase?>()))
            .Returns((string? id, PlaygroundDescriptorBase? fallback) => fallback);

        Services.AddSingleton(dataProvider.Object);
        Services.AddSingleton(resolver.Object);
    }

    // ------------------------------------------------------------------
    // Happy path: resolved descriptor renders OptAPlayground
    // ------------------------------------------------------------------

    [Fact]
    public void OptADocumentPlayground_ResolvedDescriptor_RendersPlayground()
    {
        var descriptor = new PlaygroundDescriptor<FakeComponent> { Title = "Demo" };
        var content = new PlaygroundDirectiveContent
        {
            DirectiveId = "demo",
            ResolvedDescriptor = descriptor,
        };

        var cut = Render<OptAChild>(p => p.Add(x => x.Content, content));

        // OptAPlayground emits a div with the opta-playground attribute
        var container = cut.Find("[opta-playground]");
        Assert.NotNull(container);
    }

    [Fact]
    public void OptADocumentPlayground_ResolvedDescriptor_DoesNotRenderErrorDiv()
    {
        var descriptor = new PlaygroundDescriptor<FakeComponent> { Title = "Demo" };
        var content = new PlaygroundDirectiveContent
        {
            DirectiveId = "demo",
            ResolvedDescriptor = descriptor,
        };

        var cut = Render<OptAChild>(p => p.Add(x => x.Content, content));

        Assert.Empty(cut.FindAll(".opta-playground-error"));
    }

    // ------------------------------------------------------------------
    // Error path: unknown id or no resolver renders visible error block
    // ------------------------------------------------------------------

    [Fact]
    public void OptADocumentPlayground_ErrorMessage_RendersErrorDiv()
    {
        var content = new PlaygroundDirectiveContent
        {
            DirectiveId = "unknown-id",
            ErrorMessage = "Unknown playground id: \"unknown-id\".",
        };

        var cut = Render<OptAChild>(p => p.Add(x => x.Content, content));

        var errorDiv = cut.Find(".opta-playground-error");
        Assert.NotNull(errorDiv);
        Assert.Equal("alert", errorDiv.GetAttribute("role"));
        Assert.Contains("unknown-id", errorDiv.TextContent, StringComparison.Ordinal);
    }

    [Fact]
    public void OptADocumentPlayground_ErrorMessage_DoesNotRenderPlayground()
    {
        var content = new PlaygroundDirectiveContent
        {
            DirectiveId = "unknown-id",
            ErrorMessage = "Unknown playground id: \"unknown-id\".",
        };

        var cut = Render<OptAChild>(p => p.Add(x => x.Content, content));

        Assert.Empty(cut.FindAll("[opta-playground]"));
    }

    // ------------------------------------------------------------------
    // Null content: renders nothing
    // ------------------------------------------------------------------

    [Fact]
    public void OptADocumentPlayground_NullContent_RendersNothing()
    {
        var cut = Render<OptAChild>(p => p.Add(x => x.Content, (PlaygroundDirectiveContent?)null));

        Assert.Empty(cut.Nodes);
    }

    // ------------------------------------------------------------------
    // Neither descriptor nor error: renders nothing
    // ------------------------------------------------------------------

    [Fact]
    public void OptADocumentPlayground_NeitherDescriptorNorError_RendersNothing()
    {
        // Content with no descriptor and no error message — render produces no output.
        var content = new PlaygroundDirectiveContent
        {
            DirectiveId = "pending",
        };

        var cut = Render<OptAChild>(p => p.Add(x => x.Content, content));

        Assert.Empty(cut.FindAll("[opta-playground]"));
        Assert.Empty(cut.FindAll(".opta-playground-error"));
    }

    private sealed class FakeComponent : ComponentBase { }
}
