using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Components.UnitTests.Carousel;

public class OptACarouselSlideTests : BunitContext
{
    private readonly Mock<ICarouselDataProvider> _carouselDataProvider;

    public OptACarouselSlideTests()
    {
        _carouselDataProvider = new Mock<ICarouselDataProvider>();
        _carouselDataProvider.Setup(p => p.DefaultItemSelectClasses()).Returns("");
        _carouselDataProvider.Setup(p => p.DefaultActiveItemSelectClasses()).Returns("active");
        _carouselDataProvider.Setup(p => p.DefaultInactiveItemSelectClasses()).Returns("inactive");
        _carouselDataProvider.Setup(p => p.DefaultAutoPlayClasses()).Returns("");
        _carouselDataProvider.Setup(p => p.DefaultNextClasses()).Returns("");
        _carouselDataProvider.Setup(p => p.DefaultPreviousClasses()).Returns("");
        _carouselDataProvider.Setup(p => p.DefaultNextIconClasses()).Returns("next-icon");
        _carouselDataProvider.Setup(p => p.DefaultPreviousIconClasses()).Returns("previous-icon");
        _carouselDataProvider.Setup(p => p.GetAutoPlayText()).Returns("Auto Play");
        _carouselDataProvider.Setup(p => p.AdditionalAttributesItemSelect()).Returns(new Dictionary<string, object?>());
        Services.AddSingleton(_carouselDataProvider.Object);
    }

    [Fact]
    public void OptACarouselSlideRendersCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptACarouselSlide>(parameters => parameters
            .Add(p => p.SlideNumber, 1));

        // Assert
        var li = cut.Find("li[opta-carousel-slide]");
        Assert.NotNull(li);
    }

    [Fact]
    public void OptACarouselSlideRendersImageWhenImageUrlIsProvided()
    {
        // Arrange & Act
        var cut = Render<OptACarouselSlide>(parameters => parameters
            .Add(p => p.SlideNumber, 1)
            .Add(p => p.ImageUrl, "test.jpg")
            .Add(p => p.ImageText, "Test Image"));

        // Assert
        var img = cut.Find("img[opta-carousel-image]");
        Assert.NotNull(img);
        Assert.Equal("test.jpg", img.GetAttribute("src"));
        Assert.Equal("Test Image", img.GetAttribute("alt"));
        Assert.Equal("Test Image", img.GetAttribute("title"));
    }

    [Fact]
    public void OptACarouselSlideDoesNotRenderImageWhenImageUrlIsNotProvided()
    {
        // Arrange & Act
        var cut = Render<OptACarouselSlide>(parameters => parameters
            .Add(p => p.SlideNumber, 1));

        // Assert
        Assert.Throws<Bunit.ElementNotFoundException>(() => cut.Find("img"));
    }

    [Fact]
    public void OptACarouselSlideRendersChildContent()
    {
        // Arrange & Act
        var cut = Render<OptACarouselSlide>(parameters => parameters
            .Add(p => p.SlideNumber, 1)
            .AddChildContent("<div>Test Content</div>"));

        // Assert
        var content = cut.Find("div[opta-carousel-content]");
        Assert.Contains("Test Content", content.InnerHtml);
    }
}
