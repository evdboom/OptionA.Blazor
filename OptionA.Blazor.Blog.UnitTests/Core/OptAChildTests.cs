using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Blog.UnitTests.Core;

public class OptAChildTests : BunitContext
{
    private readonly Mock<IBlogDataProvider> _blogDataProvider;
    private readonly Mock<IMarkDownParser> _markDownParser;

    public OptAChildTests()
    {
        _blogDataProvider = new Mock<IBlogDataProvider>();
        _blogDataProvider
            .Setup(x => x.DefaultClassesForType(It.IsAny<ContentType>()))
            .Returns([]);
        _markDownParser = new Mock<IMarkDownParser>();
        _markDownParser
            .Setup(x => x.Parse(It.IsAny<string?>()))
            .Returns(new List<IContent>());
        Services.AddSingleton(_blogDataProvider.Object);
        Services.AddSingleton(_markDownParser.Object);
    }

    [Fact]
    public void OptAChildRendersTextContent()
    {
        // Arrange
        var content = new TextContent { Content = "Test Content" };

        // Act
        var cut = Render<OptAChild>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        Assert.Contains("Test Content", cut.Markup);
    }

    [Fact]
    public void OptAChildRendersNullContentCorrectly()
    {
        // Act
        var cut = Render<OptAChild>(parameters => parameters.Add(p => p.Content, (IContent?)null));

        // Assert
        Assert.NotNull(cut);
    }

    [Fact]
    public void OptAChildRendersHeaderContent()
    {
        // Arrange
        var content = new HeaderContent { Content = "Header Text", Size = HeaderSize.Two };

        // Act
        var cut = Render<OptAChild>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var header = cut.Find("h2");
        Assert.Contains("Header Text", header.TextContent);
    }
}
