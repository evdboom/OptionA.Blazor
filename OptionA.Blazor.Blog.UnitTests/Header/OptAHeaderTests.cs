using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Blog.UnitTests.Header;

public class OptAHeaderTests : BunitContext
{
    private readonly Mock<IBlogDataProvider> _blogDataProvider;

    public OptAHeaderTests()
    {
        _blogDataProvider = new Mock<IBlogDataProvider>();
        _blogDataProvider
            .Setup(x => x.DefaultClassesForType(It.IsAny<ContentType>()))
            .Returns([]);
        Services.AddSingleton(_blogDataProvider.Object);
    }

    [Fact]
    public void OptAHeaderRendersH1()
    {
        // Arrange
        var content = new HeaderContent
        {
            Content = "Test Header",
            Size = HeaderSize.One
        };

        // Act
        var cut = Render<OptAHeader>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var header = cut.Find("h1");
        Assert.Contains("Test Header", header.TextContent);
    }

    [Fact]
    public void OptAHeaderRendersH2()
    {
        // Arrange
        var content = new HeaderContent
        {
            Content = "Test Header",
            Size = HeaderSize.Two
        };

        // Act
        var cut = Render<OptAHeader>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var header = cut.Find("h2");
        Assert.Contains("Test Header", header.TextContent);
    }

    [Fact]
    public void OptAHeaderRendersH3()
    {
        // Arrange
        var content = new HeaderContent
        {
            Content = "Test Header",
            Size = HeaderSize.Three
        };

        // Act
        var cut = Render<OptAHeader>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var header = cut.Find("h3");
        Assert.Contains("Test Header", header.TextContent);
    }

    [Fact]
    public void OptAHeaderHandlesNullContent()
    {
        // Act
        var cut = Render<OptAHeader>(parameters => parameters.Add(p => p.Content, (HeaderContent?)null));

        // Assert
        Assert.NotNull(cut);
    }
}
