using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Components.UnitTests.Carousel;

public class OptACarouselTests : BunitContext
{
    private readonly Mock<ICarouselDataProvider> _carouselDataProvider;

    public OptACarouselTests()
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
    public void OptACarouselRendersCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptACarousel>();

        // Assert
        var div = cut.Find("div[opta-carousel]");
        Assert.NotNull(div);
    }

    [Fact]
    public void OptACarouselShowsAutoPlayByDefault()
    {
        // Arrange & Act
        var cut = Render<OptACarousel>();

        // Assert
        var autoPlay = cut.Find("div[opta-carousel-autoplay]");
        Assert.NotNull(autoPlay);
    }

    [Fact]
    public void OptACarouselHidesAutoPlayWhenShowAutoPlayIsFalse()
    {
        // Arrange & Act
        var cut = Render<OptACarousel>(parameters => parameters
            .Add(p => p.ShowAutoPlay, false));

        // Assert
        Assert.Throws<Bunit.ElementNotFoundException>(() => cut.Find("div[opta-carousel-autoplay]"));
    }

    [Fact]
    public void OptACarouselShowsNextPreviousButtonsByDefault()
    {
        // Arrange & Act
        var cut = Render<OptACarousel>();

        // Assert
        var buttons = cut.FindAll("button");
        Assert.True(buttons.Count >= 2);
    }

    [Fact]
    public void OptACarouselHidesNextPreviousButtonsWhenShowNextPreviousIsFalse()
    {
        // Arrange & Act
        var cut = Render<OptACarousel>(parameters => parameters
            .Add(p => p.ShowNextPrevious, false));

        // Assert
        var nextPreviousButtons = cut.FindAll("button").Where(b => !b.ParentElement!.HasAttribute("opta-carousel-autoplay"));
        Assert.Empty(nextPreviousButtons);
    }
}
