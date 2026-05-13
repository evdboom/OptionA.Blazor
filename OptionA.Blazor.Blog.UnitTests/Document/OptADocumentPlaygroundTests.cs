using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OptionA.Blazor.Playground;

namespace OptionA.Blazor.Blog.UnitTests.Document;

/// <summary>
/// bUnit tests for playground directive rendering through <see cref="OptADocument"/>.
/// Covers the resolved descriptor path, visible errors, and null markdown content.
/// </summary>
public class OptADocumentPlaygroundTests : BunitContext
{
    private readonly Mock<IPlaygroundDescriptorResolver> _resolver = new();

    public OptADocumentPlaygroundTests()
    {
        Services.AddOptionABlog();

        var dataProvider = new Mock<IPlaygroundDataProvider>();
        dataProvider.Setup(d => d.DefaultLayout).Returns(PlaygroundLayout.SideBySide);
        dataProvider.Setup(d => d.CodeEditingEnabled).Returns(false);
        dataProvider.Setup(d => d.PreferredCodeEditor).Returns(PlaygroundEditorKind.TextArea);
        dataProvider.Setup(d => d.DefaultCodeLanguage).Returns((string?)null);
        dataProvider.Setup(d => d.EnabledExportFormats).Returns(Array.Empty<PlaygroundExportFormat>().ToList().AsReadOnly());
        dataProvider.Setup(d => d.DefaultPlaygroundClass).Returns((string?)null);

        Services.AddSingleton(dataProvider.Object);
        Services.AddSingleton(_resolver.Object);
    }

    [Fact]
    public void OptADocumentPlayground_ResolvedDescriptor_RendersPlayground()
    {
        _resolver
            .Setup(r => r.Resolve("demo", null))
            .Returns(new PlaygroundDescriptor<OptADocumentPlaygroundPreview> { Title = "Demo" });

        var cut = RenderDocument(
            """
            ::: playground id="demo"
            :::
            """);

        Assert.NotNull(cut.Find("[opta-playground]"));
    }

    [Fact]
    public void OptADocumentPlayground_ResolvedDescriptor_DoesNotRenderErrorDiv()
    {
        _resolver
            .Setup(r => r.Resolve("demo", null))
            .Returns(new PlaygroundDescriptor<OptADocumentPlaygroundPreview> { Title = "Demo" });

        var cut = RenderDocument(
            """
            ::: playground id="demo"
            :::
            """);

        Assert.Empty(cut.FindAll(".opta-playground-error"));
    }

    [Fact]
    public void OptADocumentPlayground_ErrorMessage_RendersErrorDiv()
    {
        var cut = RenderDocument(
            """
            ::: playground id="unknown-id"
            :::
            """);

        var errorDiv = cut.Find(".opta-playground-error");
        Assert.NotNull(errorDiv);
        Assert.Equal("alert", errorDiv.GetAttribute("role"));
        Assert.Contains("unknown-id", errorDiv.TextContent, StringComparison.Ordinal);
    }

    [Fact]
    public void OptADocumentPlayground_ErrorMessage_DoesNotRenderPlayground()
    {
        var cut = RenderDocument(
            """
            ::: playground id="unknown-id"
            :::
            """);

        Assert.Empty(cut.FindAll("[opta-playground]"));
    }

    [Fact]
    public void OptADocumentPlayground_NullContent_RendersNothing()
    {
        var cut = Render<OptADocument>(parameters => parameters.Add(x => x.Source, (string?)null));

        Assert.Empty(cut.Nodes);
    }

    [Fact]
    public void OptADocumentPlayground_InvalidTypedOverride_StillRendersPlayground()
    {
        _resolver
            .Setup(r => r.Resolve("demo", null))
            .Returns(new PlaygroundDescriptor<OptADocumentPlaygroundPreview>
            {
                Title = "Demo",
                Parameters =
                [
                    new() { Name = "Count", ValueType = typeof(int), DefaultValue = 0 },
                ],
            });

        // "notanumber" cannot be converted to int — playground should still render with original default
        var cut = RenderDocument(
            """
            ::: playground id="demo"
            parameters:
              Count: notanumber
            :::
            """);

        Assert.NotNull(cut.Find("[opta-playground]"));
    }

    [Fact]
    public void OptADocumentPlayground_InvalidTypedOverride_RendersWarningDiv()
    {
        _resolver
            .Setup(r => r.Resolve("demo", null))
            .Returns(new PlaygroundDescriptor<OptADocumentPlaygroundPreview>
            {
                Title = "Demo",
                Parameters =
                [
                    new() { Name = "Count", ValueType = typeof(int), DefaultValue = 0 },
                ],
            });

        var cut = RenderDocument(
            """
            ::: playground id="demo"
            parameters:
              Count: notanumber
            :::
            """);

        var warnings = cut.FindAll(".opta-playground-warning");
        Assert.NotEmpty(warnings);
        Assert.All(warnings, w => Assert.Equal("alert", w.GetAttribute("role")));
    }

    [Fact]
    public void OptADocumentPlayground_ValidOverride_DoesNotRenderWarning()
    {
        _resolver
            .Setup(r => r.Resolve("demo", null))
            .Returns(new PlaygroundDescriptor<OptADocumentPlaygroundPreview>
            {
                Title = "Demo",
                Parameters =
                [
                    new() { Name = "Count", ValueType = typeof(int), DefaultValue = 0 },
                ],
            });

        var cut = RenderDocument(
            """
            ::: playground id="demo"
            parameters:
              Count: 42
            :::
            """);

        Assert.Empty(cut.FindAll(".opta-playground-warning"));
    }

    private IRenderedComponent<OptADocument> RenderDocument(string markdown)
        => Render<OptADocument>(parameters => parameters.Add(x => x.Source, markdown));

    private sealed class OptADocumentPlaygroundPreview : ComponentBase { }
}
