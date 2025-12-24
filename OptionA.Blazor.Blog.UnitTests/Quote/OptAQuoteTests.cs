using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Blog.UnitTests.Quote;

public class OptAQuoteTests : BunitContext
{
    private readonly Mock<IMarkDownParser> _markDownParser;
    private readonly Mock<IBlogDataProvider> _blogDataProvider;

    public OptAQuoteTests()
    {
        _markDownParser = new Mock<IMarkDownParser>();
        _markDownParser
            .Setup(x => x.Parse(It.IsAny<string?>()))
            .Returns((string? text) => string.IsNullOrEmpty(text) 
                ? new List<IContent>() 
                : new List<IContent> { new TextContent { Content = text } });
        _blogDataProvider = new Mock<IBlogDataProvider>();
        _blogDataProvider
            .Setup(x => x.DefaultClassesForType(It.IsAny<ContentType>()))
            .Returns([]);
        Services.AddSingleton(_markDownParser.Object);
        Services.AddSingleton(_blogDataProvider.Object);
    }

    [Fact]
    public void OptAQuoteRendersCorrectly()
    {
        // Arrange
        var content = new QuoteContent
        {
            Quote = "This is a test quote",
            Source = "Test Author"
        };

        // Act
        var cut = Render<OptAQuote>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var blockquote = cut.Find("blockquote");
        Assert.NotNull(blockquote);
        Assert.Contains("This is a test quote", cut.Markup);
        Assert.Contains("Test Author", cut.Markup);
    }

    [Fact]
    public void OptAQuoteRendersWithSourceUrl()
    {
        // Arrange
        var content = new QuoteContent
        {
            Quote = "Test quote",
            Source = "Author",
            SourceUrl = "https://example.com"
        };

        // Act
        var cut = Render<OptAQuote>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var blockquote = cut.Find("blockquote");
        Assert.Equal("https://example.com", blockquote.GetAttribute("cite"));
    }

    [Fact]
    public void OptAQuoteHandlesNullContent()
    {
        // Act
        var cut = Render<OptAQuote>(parameters => parameters.Add(p => p.Content, (QuoteContent?)null));

        // Assert
        Assert.NotNull(cut);
    }

    [Fact]
    public void OptAQuoteRendersOnlyQuoteWithoutSource()
    {
        // Arrange
        var content = new QuoteContent
        {
            Quote = "Quote without source"
        };

        // Act
        var cut = Render<OptAQuote>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var blockquote = cut.Find("blockquote");
        Assert.NotNull(blockquote);
        Assert.Contains("Quote without source", cut.Markup);
    }
}
