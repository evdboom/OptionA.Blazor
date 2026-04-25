using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Blog.UnitTests.Document;

/// <summary>
/// bUnit tests for the <see cref="OptADocument"/> component.
/// These tests verify that the component renders the correct DOM output for each Markdown node kind.
/// </summary>
public class OptADocumentTests : BunitContext
{
    private readonly Mock<IBlogDataProvider> _blogDataProvider;
    private readonly Mock<IMarkDownParser> _markDownParser;

    public OptADocumentTests()
    {
        _blogDataProvider = new Mock<IBlogDataProvider>();
        _blogDataProvider
            .Setup(x => x.DefaultClassesForType(It.IsAny<ContentType>()))
            .Returns([]);
        _blogDataProvider
            .Setup(x => x.NewLine)
            .Returns("<br/>");
        _markDownParser = new Mock<IMarkDownParser>();
        // Pass through the text unchanged for inline parsing
        _markDownParser
            .Setup(x => x.Parse(It.IsAny<string?>()))
            .Returns((string? text) => string.IsNullOrEmpty(text)
                ? new List<IContent>()
                : new List<IContent> { new TextContent { Content = text } });

        Services.AddSingleton(_blogDataProvider.Object);
        Services.AddSingleton(_markDownParser.Object);
        Services.AddSingleton<IMarkdownDocumentParser, MarkdownDocumentParser>();
        Services.AddSingleton<IEnumerable<ICodeParser>>(Array.Empty<ICodeParser>());
    }

    [Fact]
    public void OptADocument_NullSource_RendersNothing()
    {
        var cut = Render<OptADocument>(p => p.Add(x => x.Source, (string?)null));
        Assert.Empty(cut.Nodes);
    }

    [Fact]
    public void OptADocument_Heading1_RendersH1()
    {
        var cut = Render<OptADocument>(p => p.Add(x => x.Source, "# Hello World"));
        var h1 = cut.Find("h1");
        Assert.NotNull(h1);
        Assert.Equal("Hello World", h1.TextContent);
    }

    [Fact]
    public void OptADocument_Heading2_RendersH2()
    {
        var cut = Render<OptADocument>(p => p.Add(x => x.Source, "## Subtitle"));
        var h2 = cut.Find("h2");
        Assert.NotNull(h2);
    }

    [Fact]
    public void OptADocument_Heading3_RendersH3()
    {
        var cut = Render<OptADocument>(p => p.Add(x => x.Source, "### Section"));
        var h3 = cut.Find("h3");
        Assert.NotNull(h3);
    }

    [Fact]
    public void OptADocument_Paragraph_RendersP()
    {
        var cut = Render<OptADocument>(p => p.Add(x => x.Source, "Hello paragraph."));
        var para = cut.Find("p");
        Assert.NotNull(para);
        Assert.Equal("Hello paragraph.", para.TextContent);
    }

    [Fact]
    public void OptADocument_FencedCodeBlock_RendersPre()
    {
        var md = "```csharp\nvar x = 1;\n```";
        var cut = Render<OptADocument>(p => p.Add(x => x.Source, md));
        var pre = cut.Find("pre");
        Assert.NotNull(pre);
    }

    [Fact]
    public void OptADocument_UnorderedList_RendersUl()
    {
        var md = "- item one\n- item two";
        var cut = Render<OptADocument>(p => p.Add(x => x.Source, md));
        var ul = cut.Find("ul");
        Assert.NotNull(ul);
        var items = cut.FindAll("li");
        Assert.Equal(2, items.Count);
    }

    [Fact]
    public void OptADocument_OrderedList_RendersOl()
    {
        var md = "1. first\n2. second";
        var cut = Render<OptADocument>(p => p.Add(x => x.Source, md));
        var ol = cut.Find("ol");
        Assert.NotNull(ol);
    }

    [Fact]
    public void OptADocument_BlockQuote_RendersFigureBlockquote()
    {
        var md = "> This is a quote.";
        var cut = Render<OptADocument>(p => p.Add(x => x.Source, md));
        var blockquote = cut.Find("blockquote");
        Assert.NotNull(blockquote);
    }

    [Fact]
    public void OptADocument_Table_RendersTable()
    {
        var md = """
            | Name | Age |
            |------|-----|
            | Alice | 30 |
            """;
        var cut = Render<OptADocument>(p => p.Add(x => x.Source, md));
        var table = cut.Find("table");
        Assert.NotNull(table);
    }

    [Fact]
    public void OptADocument_MixedDocument_RendersAllNodeKinds()
    {
        var md = """
            # Title

            A paragraph.

            ```csharp
            var x = 1;
            ```

            - one
            - two
            """;

        var cut = Render<OptADocument>(p => p.Add(x => x.Source, md));
        Assert.NotNull(cut.Find("h1"));
        Assert.NotNull(cut.Find("p"));
        Assert.NotNull(cut.Find("pre"));
        Assert.NotNull(cut.Find("ul"));
    }

    [Fact]
    public void OptADocument_SourceChangedToHeading2_RendersH2()
    {
        // Verify that different source inputs render the correct heading levels
        var cut1 = Render<OptADocument>(p => p.Add(x => x.Source, "# Initial"));
        Assert.NotNull(cut1.Find("h1"));

        var cut2 = Render<OptADocument>(p => p.Add(x => x.Source, "## Updated"));
        Assert.NotNull(cut2.Find("h2"));
        Assert.Empty(cut2.FindAll("h1"));
    }

    [Fact]
#pragma warning disable BL0005
    public void OptADocument_SourceUpdatedOnExistingComponent_ReplacesRenderedContent()
    {
        var cut = Render<OptADocument>(parameters => parameters.Add(x => x.Source, "# Initial"));
        Assert.NotNull(cut.Find("h1"));

        cut.Instance.Source = "## Updated";
        cut.Render();

        Assert.Empty(cut.FindAll("h1"));
        var heading = cut.Find("h2");
        Assert.Equal("Updated", heading.TextContent);
    }
#pragma warning restore BL0005

    [Fact]
#pragma warning disable BL0005
    public void OptADocument_SourceClearedOnExistingComponent_RendersNothing()
    {
        var cut = Render<OptADocument>(parameters => parameters.Add(x => x.Source, "# Initial"));
        Assert.NotNull(cut.Find("h1"));

        cut.Instance.Source = "   ";
        cut.Render();

        Assert.Equal(string.Empty, cut.Markup);
    }
#pragma warning restore BL0005

    [Fact]
    public void OptADocument_StandaloneImage_RendersImg()
    {
        var md = "![demo](/img/demo.png)";
        var cut = Render<OptADocument>(p => p.Add(x => x.Source, md));
        var img = cut.Find("img");
        Assert.NotNull(img);
        Assert.Equal("/img/demo.png", img.GetAttribute("src"));
        Assert.Equal("demo", img.GetAttribute("alt"));
    }

    [Fact]
    public void OptADocument_StandaloneImageWithTitle_RendersImgWithTitleAndAlt()
    {
        var md = "![alt text](/img/demo.png \"My Title\")";
        var cut = Render<OptADocument>(p => p.Add(x => x.Source, md));
        var img = cut.Find("img");
        Assert.NotNull(img);
        Assert.Equal("/img/demo.png", img.GetAttribute("src"));
        Assert.Equal("alt text", img.GetAttribute("alt"));
        Assert.Equal("My Title", img.GetAttribute("title"));
    }

    [Fact]
    public void OptADocument_StandaloneImageWithEmptyAlt_RendersImgWithEmptyAlt()
    {
        var md = "![](/img/no-alt.png)";
        var cut = Render<OptADocument>(p => p.Add(x => x.Source, md));
        var img = cut.Find("img");

        Assert.NotNull(img);
        Assert.Equal("/img/no-alt.png", img.GetAttribute("src"));
        Assert.Equal(string.Empty, img.GetAttribute("alt"));
    }

    [Fact]
    public void OptADocument_InlineImageInParagraph_RendersParagraphWithoutStandaloneImg()
    {
        var md = "See this image: ![logo](/img/logo.png) for reference.";
        var cut = Render<OptADocument>(p => p.Add(x => x.Source, md));
        var paragraph = cut.Find("p");

        Assert.NotNull(paragraph);
        Assert.Empty(cut.FindAll("img"));
        Assert.Equal("See this image: ![logo](/img/logo.png) for reference.", paragraph.TextContent);
    }

    [Fact]
    public void OptADocument_MixedDocumentWithImage_RendersImgAndHeadingAndParagraph()
    {
        var md = """
            # Title

            ![demo](/img/demo.png)

            A paragraph after the image.
            """;

        var cut = Render<OptADocument>(p => p.Add(x => x.Source, md));
        Assert.NotNull(cut.Find("h1"));
        Assert.NotNull(cut.Find("img"));
        Assert.NotNull(cut.Find("p"));
    }

    [Fact]
    public void OptADocument_DocumentationExample_UsesOnlyShippedSyntaxAndRendersSupportedNodes()
    {
        var examplePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "docs", "examples", "buttons.md"));
        var markdown = File.ReadAllText(examplePath);

        Assert.DoesNotContain("::: playground", markdown, StringComparison.Ordinal);
        Assert.False(markdown.StartsWith("---", StringComparison.Ordinal));

        var cut = Render<OptADocument>(parameters => parameters.Add(x => x.Source, markdown));

        Assert.NotNull(cut.Find("h1"));
        Assert.NotNull(cut.Find("blockquote"));
        Assert.NotNull(cut.Find("ul"));
        Assert.NotNull(cut.Find("pre"));
        Assert.NotNull(cut.Find("img"));
        Assert.NotNull(cut.Find("table"));
    }
}
