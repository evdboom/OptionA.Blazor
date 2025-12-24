using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Blog.UnitTests.Image;

public class OptAImageTests : BunitContext
{
    private readonly Mock<IBlogDataProvider> _blogDataProvider;

    public OptAImageTests()
    {
        _blogDataProvider = new Mock<IBlogDataProvider>();
        _blogDataProvider
            .Setup(x => x.DefaultClassesForType(It.IsAny<ContentType>()))
            .Returns([]);
        Services.AddSingleton(_blogDataProvider.Object);
    }

    [Fact]
    public void OptAImageRendersCorrectly()
    {
        // Arrange
        var content = new ImageContent
        {
            Source = "/images/test.jpg",
            Alternative = "Test Image"
        };

        // Act
        var cut = Render<OptAImage>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var img = cut.Find("img");
        Assert.Equal("/images/test.jpg", img.GetAttribute("src"));
        Assert.Equal("Test Image", img.GetAttribute("alt"));
    }

    [Fact]
    public void OptAImageRendersWithTitle()
    {
        // Arrange
        var content = new ImageContent
        {
            Source = "/images/test.jpg",
            Title = "Test Title"
        };

        // Act
        var cut = Render<OptAImage>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var img = cut.Find("img");
        Assert.Equal("Test Title", img.GetAttribute("title"));
    }

    [Fact]
    public void OptAImageHandlesNullContent()
    {
        // Act
        var cut = Render<OptAImage>(parameters => parameters.Add(p => p.Content, (ImageContent?)null));

        // Assert
        Assert.NotNull(cut);
    }
}
