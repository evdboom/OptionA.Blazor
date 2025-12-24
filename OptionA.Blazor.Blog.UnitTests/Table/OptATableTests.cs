using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Blog.UnitTests.Table;

public class OptATableTests : BunitContext
{
    private readonly Mock<IBlogDataProvider> _blogDataProvider;
    private readonly Mock<IMarkDownParser> _markDownParser;

    public OptATableTests()
    {
        _blogDataProvider = new Mock<IBlogDataProvider>();
        _blogDataProvider
            .Setup(x => x.DefaultClassesForType(It.IsAny<ContentType>()))
            .Returns([]);
        _markDownParser = new Mock<IMarkDownParser>();
        _markDownParser
            .Setup(x => x.Parse(It.IsAny<string?>()))
            .Returns((string? text) => string.IsNullOrEmpty(text) 
                ? new List<IContent>() 
                : new List<IContent> { new TextContent { Content = text } });
        Services.AddSingleton(_blogDataProvider.Object);
        Services.AddSingleton(_markDownParser.Object);
    }

    [Fact]
    public void OptATableRendersCorrectly()
    {
        // Arrange
        var content = new TableContent();
        content.Headers.Add("Column 1");
        content.Headers.Add("Column 2");
        content.Rows.Add(new List<string> { "Row1Col1", "Row1Col2" });
        content.Rows.Add(new List<string> { "Row2Col1", "Row2Col2" });

        // Act
        var cut = Render<OptATable>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var table = cut.Find("table");
        Assert.NotNull(table);
    }

    [Fact]
    public void OptATableRendersHeaders()
    {
        // Arrange
        var content = new TableContent();
        content.Headers.Add("Header 1");
        content.Headers.Add("Header 2");

        // Act
        var cut = Render<OptATable>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var thead = cut.Find("thead");
        Assert.NotNull(thead);
        Assert.Contains("Header 1", cut.Markup);
        Assert.Contains("Header 2", cut.Markup);
    }

    [Fact]
    public void OptATableRendersRows()
    {
        // Arrange
        var content = new TableContent();
        content.Rows.Add(new List<string> { "Cell1", "Cell2" });

        // Act
        var cut = Render<OptATable>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var tbody = cut.Find("tbody");
        Assert.NotNull(tbody);
        Assert.Contains("Cell1", cut.Markup);
        Assert.Contains("Cell2", cut.Markup);
    }

    [Fact]
    public void OptATableRendersFooter()
    {
        // Arrange
        var content = new TableContent();
        content.Footer.Add("Footer 1");
        content.Footer.Add("Footer 2");

        // Act
        var cut = Render<OptATable>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var tfoot = cut.Find("tfoot");
        Assert.NotNull(tfoot);
        Assert.Contains("Footer 1", cut.Markup);
        Assert.Contains("Footer 2", cut.Markup);
    }

    [Fact]
    public void OptATableHandlesNullContent()
    {
        // Act
        var cut = Render<OptATable>(parameters => parameters.Add(p => p.Content, (TableContent?)null));

        // Assert
        Assert.NotNull(cut);
    }
}
