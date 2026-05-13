using System;
using System.Collections.Generic;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace OptionA.Blazor.Blog.UnitTests.Document;

public class OptADocumentMetadataTests : BunitContext
{
    [Fact]
    public void DocumentMetadata_ParseFromMarkdown_ParsesFrontMatter()
    {
        var md = """
---
title: "Hello"
subtitle: Sub
date: 2021-01-02
tags: [one, two]
---
Body content here.
""";

        var (meta, body) = DocumentMetadata.ParseFromMarkdown(md);

        Assert.NotNull(meta);
        Assert.Equal("Hello", meta.Title);
        Assert.Equal("Sub", meta.Subtitle);
        Assert.Equal(new DateTime(2021, 1, 2), meta.Date);
        Assert.Equal(2, meta.Tags.Count);
        Assert.Equal("Body content here.", body.Trim());
    }

    [Fact]
    public void PostHelpers_FromMetadataAndContent_UsesParserAndMaps()
    {
        var md = new DocumentMetadata { Title = "T", Subtitle = "S", Date = new DateTime(2020, 1, 1) };
        var markdown = "content body";
        var parsed = new List<IContent> { new TextContent { Content = "x" } };

        var mock = new Mock<IMarkdownDocumentParser>();
        mock.Setup(p => p.Parse(markdown)).Returns(parsed);

        var post = OptionA.Blazor.Blog.Core.PostHelpers.FromMetadataAndContent(md, markdown, mock.Object);

        Assert.Equal("T", post.Title);
        Assert.Equal("S", post.Subtitle);
        Assert.Equal(new DateTime(2020, 1, 1), post.Date);
        Assert.Equal(parsed.Count, post.Content.Count);
        Assert.Same(parsed[0], post.Content[0]);
    }

    [Fact]
    public void PostHelpers_FromMetadataAndContent_WithFrontMatter_ParsesOnlyBody()
    {
        var metadata = new DocumentMetadata { Title = "T" };
        var markdown = """
---
title: T
tags: [one, two]
---
Body here
""";
        var parsed = new List<IContent> { new TextContent { Content = "Body here" } };

        var mock = new Mock<IMarkdownDocumentParser>();
        mock.Setup(p => p.Parse("Body here")).Returns(parsed);

        var post = OptionA.Blazor.Blog.Core.PostHelpers.FromMetadataAndContent(metadata, markdown, mock.Object);

        mock.Verify(p => p.Parse("Body here"), Times.Once);
        Assert.Single(post.Content);
        Assert.Same(parsed[0], post.Content[0]);
    }

    [Fact]
    public void OptADocument_InvokesOnMetadataParsedAndParsesBody()
    {
        var markdown = """
---
title: A
---
Body here
""";

        var mockParser = new Mock<IMarkdownDocumentParser>();
        mockParser.Setup(p => p.Parse("Body here")).Returns(new List<IContent>());
        Services.AddSingleton<IMarkdownDocumentParser>(mockParser.Object);

        var receiver = Render<MetadataReceiver>(parameters => parameters.Add(p => p.SourceVar, markdown));
        var instance = receiver.Instance;

        // Ensure metadata callback was invoked and parser was called with the body (without front-matter)
        Assert.NotNull(instance.Received);
        mockParser.Verify(p => p.Parse("Body here"), Times.Once);
        Assert.Equal("A", instance.Received!.Title);
    }

    [Fact]
    public void OptADocument_AwaitsAsyncOnMetadataParsedBeforeParsingBody()
    {
        var markdown = """
---
title: Async metadata
---
Body here
""";

        var callbackCompleted = false;
        DocumentMetadata? received = null;
        Func<DocumentMetadata, Task> onMetadataParsed = async md =>
        {
            await Task.Yield();
            received = md;
            callbackCompleted = true;
        };

        var mockParser = new Mock<IMarkdownDocumentParser>();
        mockParser
            .Setup(p => p.Parse("Body here"))
            .Callback(() => Assert.True(callbackCompleted))
            .Returns(new List<IContent>());
        Services.AddSingleton<IMarkdownDocumentParser>(mockParser.Object);

        var cut = Render<OptADocument>(parameters => parameters
            .Add(x => x.Source, markdown)
            .Add(x => x.OnMetadataParsed, EventCallback.Factory.Create<DocumentMetadata>(this, onMetadataParsed)));

        cut.WaitForAssertion(() =>
        {
            Assert.True(callbackCompleted);
            Assert.NotNull(received);
            Assert.Equal("Async metadata", received!.Title);
            mockParser.Verify(p => p.Parse("Body here"), Times.Once);
        });
    }

    [Fact]
    public async Task OptADocument_OnMetadataParsedException_PropagatesAndSkipsParsingBody()
    {
        var markdown = """
---
title: Broken callback
---
Body here
""";

        var mockParser = new Mock<IMarkdownDocumentParser>();
        Services.AddSingleton<IMarkdownDocumentParser>(mockParser.Object);
        Func<DocumentMetadata, Task> onMetadataParsed = async _ =>
        {
            await Task.Yield();
            throw new InvalidOperationException("Metadata callback failed.");
        };

        Render<OptADocument>(parameters => parameters
            .Add(x => x.Source, markdown)
            .Add(x => x.OnMetadataParsed, EventCallback.Factory.Create<DocumentMetadata>(this, onMetadataParsed)));

        var exception = await Renderer.UnhandledException;

        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("Metadata callback failed.", exception.Message);
        mockParser.Verify(p => p.Parse(It.IsAny<string>()), Times.Never);
    }

    private class MetadataReceiver : ComponentBase
    {
        public DocumentMetadata? Received { get; private set; }
        [Parameter]
        public string? SourceVar { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<OptADocument>(0);
            builder.AddAttribute(1, "Source", SourceVar);
            builder.AddAttribute(2, "OnMetadataParsed", EventCallback.Factory.Create<DocumentMetadata>(this, (DocumentMetadata md) => { Received = md; }));
            builder.CloseComponent();
        }
    }
}
