using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Blog.UnitTests.Document;

/// <summary>
/// Tests for the internal <see cref="IMarkdownDocumentParser"/> implementation.
/// We test it directly so we do not need bUnit for unit-level cases.
/// </summary>
public class MarkdownDocumentParserTests
{
    private readonly IMarkdownDocumentParser _parser = new MarkdownDocumentParserAccessor();

    [Fact]
    public void Parse_NullInput_ReturnsEmptyList()
    {
        var result = _parser.Parse(null);
        Assert.Empty(result);
    }

    [Fact]
    public void Parse_WhitespaceInput_ReturnsEmptyList()
    {
        var result = _parser.Parse("   ");
        Assert.Empty(result);
    }

    [Fact]
    public void Parse_Heading1_ReturnsHeaderContentSizeOne()
    {
        var result = _parser.Parse("# Hello World");
        var header = Assert.Single(result);
        var h = Assert.IsType<HeaderContent>(header);
        Assert.Equal(HeaderSize.One, h.Size);
        Assert.Equal("Hello World", h.Content);
    }

    [Fact]
    public void Parse_Heading2_ReturnsHeaderContentSizeTwo()
    {
        var result = _parser.Parse("## Subtitle");
        var header = Assert.Single(result);
        var h = Assert.IsType<HeaderContent>(header);
        Assert.Equal(HeaderSize.Two, h.Size);
        Assert.Equal("Subtitle", h.Content);
    }

    [Fact]
    public void Parse_Heading6_ReturnsHeaderContentSizeSix()
    {
        var result = _parser.Parse("###### Tiny");
        var header = Assert.Single(result);
        var h = Assert.IsType<HeaderContent>(header);
        Assert.Equal(HeaderSize.Six, h.Size);
        Assert.Equal("Tiny", h.Content);
    }

    [Fact]
    public void Parse_Paragraph_ReturnsParagraphContent()
    {
        var result = _parser.Parse("Hello paragraph.");
        var item = Assert.Single(result);
        var p = Assert.IsType<ParagraphContent>(item);
        Assert.Equal("Hello paragraph.", p.Content);
    }

    [Fact]
    public void Parse_FencedCodeBlock_CSharp_ReturnsCodeContentWithLanguage()
    {
        var md = "```csharp\nvar x = 1;\n```";
        var result = _parser.Parse(md);
        var item = Assert.Single(result);
        var code = Assert.IsType<CodeContent>(item);
        Assert.Equal(CodeLanguage.CSharp, code.Language);
        Assert.Contains("var x = 1;", code.Code);
    }

    [Fact]
    public void Parse_FencedCodeBlock_Javascript_ReturnsCodeContentWithLanguage()
    {
        var md = "```js\nconsole.log('hi');\n```";
        var result = _parser.Parse(md);
        var item = Assert.Single(result);
        var code = Assert.IsType<CodeContent>(item);
        Assert.Equal(CodeLanguage.Javascript, code.Language);
    }

    [Fact]
    public void Parse_FencedCodeBlock_NoLanguage_ReturnsCodeContentOtherLanguage()
    {
        var md = "```\nsome code\n```";
        var result = _parser.Parse(md);
        var item = Assert.Single(result);
        var code = Assert.IsType<CodeContent>(item);
        Assert.Equal(CodeLanguage.Other, code.Language);
    }

    [Fact]
    public void Parse_FencedCodeBlock_UnknownLanguage_ReturnsCodeContentOtherWithHint()
    {
        var md = "```kotlin\nfun main() {}\n```";
        var result = _parser.Parse(md);
        var item = Assert.Single(result);
        var code = Assert.IsType<CodeContent>(item);
        Assert.Equal(CodeLanguage.Other, code.Language);
        Assert.Equal("kotlin", code.OtherLanguage);
    }

    [Fact]
    public void Parse_UnorderedList_ReturnsListContentUnordered()
    {
        var md = "- item one\n- item two\n- item three";
        var result = _parser.Parse(md);
        var item = Assert.Single(result);
        var list = Assert.IsType<ListContent>(item);
        Assert.Equal(ListType.UnorderedList, list.ListType);
        Assert.Equal(3, list.Items.Count);
        Assert.Equal("item one", list.Items[0]);
        Assert.Equal("item two", list.Items[1]);
        Assert.Equal("item three", list.Items[2]);
    }

    [Fact]
    public void Parse_OrderedList_ReturnsListContentOrdered()
    {
        var md = "1. first\n2. second";
        var result = _parser.Parse(md);
        var item = Assert.Single(result);
        var list = Assert.IsType<ListContent>(item);
        Assert.Equal(ListType.OrderedList, list.ListType);
        Assert.Equal(2, list.Items.Count);
    }

    [Fact]
    public void Parse_BlockQuote_ReturnsQuoteContent()
    {
        var md = "> This is a quote.";
        var result = _parser.Parse(md);
        var item = Assert.Single(result);
        var quote = Assert.IsType<QuoteContent>(item);
        Assert.Equal("This is a quote.", quote.Quote);
    }

    [Fact]
    public void Parse_Table_ReturnsTableContent()
    {
        var md = """
            | Name | Age |
            |------|-----|
            | Alice | 30 |
            | Bob | 25 |
            """;

        var result = _parser.Parse(md);
        var item = Assert.Single(result);
        var table = Assert.IsType<TableContent>(item);
        Assert.Equal(2, table.Headers.Count);
        Assert.Equal("Name", table.Headers[0]);
        Assert.Equal("Age", table.Headers[1]);
        Assert.Equal(2, table.Rows.Count);
    }

    [Fact]
    public void Parse_MixedDocument_ReturnsMultipleContentItems()
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

        var result = _parser.Parse(md);
        Assert.Equal(4, result.Count);
        Assert.IsType<HeaderContent>(result[0]);
        Assert.IsType<ParagraphContent>(result[1]);
        Assert.IsType<CodeContent>(result[2]);
        Assert.IsType<ListContent>(result[3]);
    }

    [Fact]
    public void Parse_ThematicBreak_IsSkipped()
    {
        var md = "Before\n\n---\n\nAfter";
        var result = _parser.Parse(md);
        // Thematic break is not mapped to any content type; only two paragraphs expected
        Assert.Equal(2, result.Count);
        Assert.All(result, r => Assert.IsType<ParagraphContent>(r));
    }

    [Fact]
    public void Parse_StandaloneImage_ReturnsImageContent()
    {
        var md = "![demo](/img/demo.png)";
        var result = _parser.Parse(md);
        var item = Assert.Single(result);
        var image = Assert.IsType<ImageContent>(item);
        Assert.Equal("/img/demo.png", image.Source);
        Assert.Equal("demo", image.Alternative);
    }

    [Fact]
    public void Parse_StandaloneImageWithTitle_ReturnsImageContentWithTitle()
    {
        var md = "![alt text](/img/demo.png \"My Title\")";
        var result = _parser.Parse(md);
        var item = Assert.Single(result);
        var image = Assert.IsType<ImageContent>(item);
        Assert.Equal("/img/demo.png", image.Source);
        Assert.Equal("alt text", image.Alternative);
        Assert.Equal("My Title", image.Title);
    }

    [Fact]
    public void Parse_StandaloneImageEmptyAlt_ReturnsImageContentWithEmptyAlternative()
    {
        var md = "![](/img/no-alt.png)";
        var result = _parser.Parse(md);
        var item = Assert.Single(result);
        var image = Assert.IsType<ImageContent>(item);
        Assert.Equal("/img/no-alt.png", image.Source);
        Assert.Equal(string.Empty, image.Alternative);
    }

    [Fact]
    public void Parse_InlineImageInParagraph_ReturnsParagraphContent()
    {
        // An image embedded in text should NOT produce ImageContent — it remains a paragraph
        var md = "See this image: ![logo](/img/logo.png) for reference.";
        var result = _parser.Parse(md);
        var item = Assert.Single(result);
        Assert.IsType<ParagraphContent>(item);
    }

    [Fact]
    public void Parse_MixedDocumentWithImage_ReturnsImageContentAtCorrectPosition()
    {
        var md = """
            # Title

            ![demo](/img/demo.png)

            A paragraph after the image.
            """;

        var result = _parser.Parse(md);
        Assert.Equal(3, result.Count);
        Assert.IsType<HeaderContent>(result[0]);
        Assert.IsType<ImageContent>(result[1]);
        Assert.IsType<ParagraphContent>(result[2]);
    }

    // ------------------------------------------------------------------
    // Inline link / image title behaviour (explicit regression lock)
    // ------------------------------------------------------------------

    /// <summary>
    /// When an inline link has a title attribute, the title is intentionally dropped
    /// during re-serialisation because the downstream <see cref="IMarkDownParser"/>
    /// (LinkMarker) only understands <c>[text](url)</c> syntax.  Including the title
    /// would corrupt the URL by appending <c> "title"</c> to the href.
    /// This test locks that behaviour so any future change is deliberate.
    /// </summary>
    [Fact]
    public void Parse_InlineLinkWithTitle_TitleIsDroppedFromSerializedText()
    {
        // A link with a title attribute: [text](url "My Title")
        var md = "[Visit us](https://example.com \"My Title\")";
        var result = _parser.Parse(md);

        var item = Assert.Single(result);
        var para = Assert.IsType<ParagraphContent>(item);

        // The re-serialised form must not contain the title string
        Assert.DoesNotContain("My Title", para.Content, StringComparison.Ordinal);
        // The href must be the bare URL, not URL + title
        Assert.Contains("https://example.com", para.Content, StringComparison.Ordinal);
        Assert.DoesNotContain("\"", para.Content, StringComparison.Ordinal);
    }

    /// <summary>
    /// When an inline image appears inside a paragraph (not as the sole content),
    /// the title attribute is intentionally dropped during re-serialisation,
    /// for the same reasons as inline links.
    /// </summary>
    [Fact]
    public void Parse_InlineImageWithTitle_TitleIsDroppedFromSerializedText()
    {
        // An inline image (mixed with surrounding text, so it stays as a paragraph)
        var md = "See ![logo](/img/logo.png \"Logo Title\") here.";
        var result = _parser.Parse(md);

        var item = Assert.Single(result);
        var para = Assert.IsType<ParagraphContent>(item);

        // Title must not appear in the re-serialised content
        Assert.DoesNotContain("Logo Title", para.Content, StringComparison.Ordinal);
        Assert.DoesNotContain("\"", para.Content, StringComparison.Ordinal);
        // URL must still be present
        Assert.Contains("/img/logo.png", para.Content, StringComparison.Ordinal);
    }

    /// <summary>
    /// A standalone image (sole content of a paragraph) is promoted to
    /// <see cref="ImageContent"/>. The title attribute IS preserved here because it
    /// is extracted directly from the Markdig AST, not via re-serialisation.
    /// </summary>
    [Fact]
    public void Parse_StandaloneImageWithTitle_TitleIsPreservedInImageContent()
    {
        var md = "![hero](/img/hero.png \"Hero image\")";
        var result = _parser.Parse(md);

        var item = Assert.Single(result);
        var image = Assert.IsType<ImageContent>(item);
        Assert.Equal("/img/hero.png", image.Source);
        Assert.Equal("hero", image.Alternative);
        Assert.Equal("Hero image", image.Title);
    }
}

/// <summary>
/// Exposes the internal <see cref="MarkdownDocumentParser"/> for unit testing.
/// </summary>
file sealed class MarkdownDocumentParserAccessor : IMarkdownDocumentParser
{
    private readonly MarkdownDocumentParser _inner = new();

    public IReadOnlyList<IContent> Parse(string? markdown) => _inner.Parse(markdown);
}
