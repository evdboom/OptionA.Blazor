using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Blog.UnitTests.Frame;

public class OptAFrameTests : BunitContext
{
    private readonly Mock<IBlogDataProvider> _blogDataProvider;

    public OptAFrameTests()
    {
        _blogDataProvider = new Mock<IBlogDataProvider>();
        _blogDataProvider
            .Setup(x => x.DefaultClassesForType(It.IsAny<ContentType>()))
            .Returns([]);
        Services.AddSingleton(_blogDataProvider.Object);
    }

    [Fact]
    public void OptAFrameRendersCorrectly()
    {
        // Arrange
        var content = new FrameContent
        {
            Source = "https://example.com",
            Width = "800",
            Height = "600"
        };

        // Act
        var cut = Render<OptAFrame>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var iframe = cut.Find("iframe");
        Assert.Equal("https://example.com", iframe.GetAttribute("src"));
    }

    [Fact]
    public void OptAFrameRendersWithTitle()
    {
        // Arrange
        var content = new FrameContent
        {
            Source = "https://example.com",
            Title = "Test Frame"
        };

        // Act
        var cut = Render<OptAFrame>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var iframe = cut.Find("iframe");
        Assert.Equal("Test Frame", iframe.GetAttribute("title"));
    }

    [Fact]
    public void OptAFrameHandlesNullContent()
    {
        // Act
        var cut = Render<OptAFrame>(parameters => parameters.Add(p => p.Content, (FrameContent?)null));

        // Assert
        Assert.NotNull(cut);
    }
}
