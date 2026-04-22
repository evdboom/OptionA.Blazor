using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Blog.UnitTests.Icon;

public class OptAIconTests : BunitContext
{
    private readonly Mock<IBlogDataProvider> _blogDataProvider;

    public OptAIconTests()
    {
        _blogDataProvider = new Mock<IBlogDataProvider>();
        _blogDataProvider
            .Setup(x => x.DefaultClassesForType(It.IsAny<ContentType>()))
            .Returns([]);
        Services.AddSingleton(_blogDataProvider.Object);
    }

    [Fact]
    public void OptAIconRendersWithPaths()
    {
        // Arrange
        var content = new IconContent
        {
            Mode = IconMode.Path,
            Width = "24",
            Height = "24"
        };
        content.Paths.Add("M10 20v-6h4v6h5v-8h3L12 3 2 12h3v8z");

        // Act
        var cut = Render<OptAIcon>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var svg = cut.Find("svg");
        Assert.NotNull(svg);
    }

    [Fact]
    public void OptAIconRendersWithClass()
    {
        // Arrange
        var content = new IconContent
        {
            Mode = IconMode.Class
        };
        content.AdditionalClasses.Add("test-icon-class");

        // Act
        var cut = Render<OptAIcon>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        Assert.Contains("test-icon-class", cut.Markup);
    }

    [Fact]
    public void OptAIconHandlesNullContent()
    {
        // Act
        var cut = Render<OptAIcon>(parameters => parameters.Add(p => p.Content, (IconContent?)null));

        // Assert
        Assert.NotNull(cut);
    }

    [Fact]
    public void OptAIconAppliesClassesInClassMode()
    {
        // Arrange
        _blogDataProvider
            .Setup(x => x.DefaultClassesForType(ContentType.Icon))
            .Returns(new List<string> { "icon-default" });
        var content = new IconContent
        {
            Mode = IconMode.Class
        };
        content.AdditionalClasses.Add("icon-custom");

        // Act
        var cut = Render<OptAIcon>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var icon = cut.Find("i");
        Assert.Contains("icon-custom", icon.GetAttribute("class"));
        Assert.Contains("icon-default", icon.GetAttribute("class"));
    }

    [Fact]
    public void OptAIconRendersSvgAttributesInPathMode()
    {
        // Arrange
        var content = new IconContent
        {
            Mode = IconMode.Path,
            Width = "24",
            Height = "24",
            ViewBoxValues = [0, 0, 24, 24],
        };
        content.Paths.Add("M10 20v-6h4v6h5v-8h3L12 3 2 12h3v8z");
        content.Paths.Add("M12 4l8 8-8 8-8-8z");

        // Act
        var cut = Render<OptAIcon>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var svg = cut.Find("svg");
        Assert.Equal("24", svg.GetAttribute("width"));
        Assert.Equal("24", svg.GetAttribute("height"));
        Assert.Equal("currentColor", svg.GetAttribute("fill"));
        Assert.Equal("0 0 24 24", svg.GetAttribute("viewBox"));
        Assert.Equal(2, svg.QuerySelectorAll("path").Length);
    }
}
