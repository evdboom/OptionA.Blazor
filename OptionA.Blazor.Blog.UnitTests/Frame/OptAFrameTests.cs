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

    [Fact]
    public void OptAFrameRendersPreviewModeWithSizedPlaceholder()
    {
        // Arrange
        var content = new FrameContent
        {
            Source = "Preview body",
            Width = "320",
            Height = "180",
            PreviewMode = true
        };

        // Act
        var cut = Render<OptAFrame>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var preview = cut.Find("div");
        Assert.Equal("Preview body", preview.TextContent);
        Assert.NotNull(preview.GetAttribute("opta-frame-preview"));
        Assert.Contains("--opta-frame-width: 320px;", preview.GetAttribute("style"));
        Assert.Contains("--opta-frame-height: 180px;", preview.GetAttribute("style"));
    }

    [Fact]
    public void OptAFrameAppliesAttributesToIframeMode()
    {
        // Arrange
        _blogDataProvider
            .Setup(x => x.DefaultClassesForType(ContentType.Frame))
            .Returns(new List<string> { "frame-default" });
        var content = new FrameContent
        {
            Source = "https://example.com",
            Title = "Frame title",
            Width = "640",
            Height = "480",
        };
        content.AdditionalClasses.Add("frame-custom");

        // Act
        var cut = Render<OptAFrame>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var iframe = cut.Find("iframe");
        Assert.Equal("https://example.com", iframe.GetAttribute("src"));
        Assert.Equal("Frame title", iframe.GetAttribute("title"));
        Assert.Equal("640", iframe.GetAttribute("width"));
        Assert.Equal("480", iframe.GetAttribute("height"));
        Assert.Contains("frame-custom", iframe.GetAttribute("class"));
        Assert.Contains("frame-default", iframe.GetAttribute("class"));
    }
}
