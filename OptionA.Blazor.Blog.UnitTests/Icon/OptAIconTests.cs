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
}
