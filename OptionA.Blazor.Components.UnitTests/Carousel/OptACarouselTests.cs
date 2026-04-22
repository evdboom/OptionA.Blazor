using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Components.UnitTests.Carousel;

public class OptACarouselTests : BunitContext
{
    private static RenderFragment CarouselSlides => builder =>
    {
        AddSlide(builder, 0, 1, "first.jpg", "First slide", "First slide content");
        AddSlide(builder, 10, 2, "second.jpg", "Second slide", "Second slide content");
        AddSlide(builder, 20, 3, "third.jpg", "Third slide", "Third slide content");
    };

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

    private static void AddSlide(
        RenderTreeBuilder builder,
        int sequence,
        int slideNumber,
        string imageUrl,
        string imageText,
        string content)
    {
        builder.OpenComponent<OptACarouselSlide>(sequence);
        builder.AddAttribute(sequence + 1, nameof(OptACarouselSlide.SlideNumber), slideNumber);
        builder.AddAttribute(sequence + 2, nameof(OptACarouselSlide.ImageUrl), imageUrl);
        builder.AddAttribute(sequence + 3, nameof(OptACarouselSlide.ImageText), imageText);
        builder.AddAttribute(sequence + 4, nameof(OptACarouselSlide.ChildContent), (RenderFragment)(contentBuilder =>
        {
            contentBuilder.AddContent(0, content);
        }));
        builder.CloseComponent();
    }

    [Fact]
    public void OptACarouselRendersSlidesAndAppliesMinimumHeight()
    {
        // Arrange & Act
        var cut = Render<OptACarousel>(parameters => parameters
            .Add(p => p.AutoPlayOnStart, false)
            .Add(p => p.MinimumHeight, 275)
            .Add(p => p.Slides, CarouselSlides));

        // Assert
        cut.WaitForAssertion(() =>
        {
            var carousel = cut.Find("div[opta-carousel]");
            var slides = cut.FindAll("li[opta-carousel-slide]");
            var itemSelectButtons = cut.FindAll("div[opta-carousel-item-select] button");

            Assert.NotNull(carousel);
            Assert.Equal(3, slides.Count);
            Assert.Equal("min-height:275px;", slides[0].GetAttribute("style"));
            Assert.Contains("First slide content", slides[0].InnerHtml);
            Assert.Equal(3, itemSelectButtons.Count);
            Assert.Contains("active", itemSelectButtons[0].ClassList);
            Assert.Contains("inactive", itemSelectButtons[1].ClassList);
            Assert.Contains("Auto Play", cut.Markup);
        });
    }

    [Fact]
    public void OptACarouselHidesOptionalControlsWhenDisabled()
    {
        // Arrange & Act
        var cut = Render<OptACarousel>(parameters => parameters
            .Add(p => p.AutoPlayOnStart, false)
            .Add(p => p.ShowAutoPlay, false)
            .Add(p => p.ShowItemSelect, false)
            .Add(p => p.ShowNextPrevious, false)
            .Add(p => p.Slides, CarouselSlides));

        // Assert
        cut.WaitForAssertion(() =>
        {
            Assert.Equal(3, cut.FindAll("li[opta-carousel-slide]").Count);
            Assert.Empty(cut.FindAll("div[opta-carousel-item-select]"));
            Assert.Empty(cut.FindAll("div[opta-carousel-autoplay]"));
            Assert.Empty(cut.FindAll("button > i.previous-icon"));
            Assert.Empty(cut.FindAll("button > i.next-icon"));
        });
    }

    [Fact]
    public void OptACarouselRendersCustomButtonContentWhenConfigured()
    {
        // Arrange & Act
        var cut = Render<OptACarousel>(parameters => parameters
            .Add(p => p.AutoPlayOnStart, false)
            .Add(p => p.ItemSelectIsIcon, false)
            .Add(p => p.NextPreviousIsIcon, false)
            .Add(p => p.ItemSelectContent, (RenderFragment)(builder => builder.AddContent(0, "Select slide")))
            .Add(p => p.PreviousContent, (RenderFragment)(builder => builder.AddContent(0, "Previous slide")))
            .Add(p => p.NextContent, (RenderFragment)(builder => builder.AddContent(0, "Next slide")))
            .Add(p => p.Slides, CarouselSlides));

        // Assert
        cut.WaitForAssertion(() =>
        {
            var itemSelectButtons = cut.FindAll("div[opta-carousel-item-select] button");
            Assert.All(itemSelectButtons, button => Assert.Equal("Select slide", button.TextContent.Trim()));
            Assert.Contains(cut.FindAll("button"), button => button.TextContent.Contains("Previous slide"));
            Assert.Contains(cut.FindAll("button"), button => button.TextContent.Contains("Next slide"));
        });
    }

    [Fact]
    public void OptACarouselSelectsSlideWhenItemButtonIsClicked()
    {
        // Arrange
        var cut = Render<OptACarousel>(parameters => parameters
            .Add(p => p.AutoPlayOnStart, false)
            .Add(p => p.Slides, CarouselSlides));

        cut.WaitForAssertion(() => Assert.Equal(3, cut.FindAll("div[opta-carousel-item-select] button").Count));

        var itemSelectButtons = cut.FindAll("div[opta-carousel-item-select] button");

        // Act
        itemSelectButtons[1].Click();

        // Assert
        cut.WaitForAssertion(() =>
        {
            itemSelectButtons = cut.FindAll("div[opta-carousel-item-select] button");
            Assert.Contains("inactive", itemSelectButtons[0].ClassList);
            Assert.Contains("active", itemSelectButtons[1].ClassList);
            Assert.Contains("inactive", itemSelectButtons[2].ClassList);
        });
    }

    [Fact]
    public void OptACarouselSelectsPreviousSlideWhenPreviousButtonIsClicked()
    {
        // Arrange
        var cut = Render<OptACarousel>(parameters => parameters
            .Add(p => p.AutoPlayOnStart, false)
            .Add(p => p.Slides, CarouselSlides));

        cut.WaitForAssertion(() => Assert.Equal(3, cut.FindAll("div[opta-carousel-item-select] button").Count));
        var previousButton = cut.Find("i.previous-icon").ParentElement!;

        // Act
        previousButton.Click();

        // Assert
        cut.WaitForAssertion(() =>
        {
            var itemSelectButtons = cut.FindAll("div[opta-carousel-item-select] button");
            Assert.Contains("inactive", itemSelectButtons[0].ClassList);
            Assert.Contains("inactive", itemSelectButtons[1].ClassList);
            Assert.Contains("active", itemSelectButtons[2].ClassList);
        });
    }

    [Fact]
    public void OptACarouselAdvancesWhenNextButtonIsClicked()
    {
        // Arrange
        var cut = Render<OptACarousel>(parameters => parameters
            .Add(p => p.AutoPlayOnStart, false)
            .Add(p => p.Slides, CarouselSlides));

        cut.WaitForAssertion(() => Assert.Equal(3, cut.FindAll("div[opta-carousel-item-select] button").Count));
        var nextButton = cut.Find("i.next-icon").ParentElement!;

        // Act
        nextButton.Click();

        // Assert
        cut.WaitForAssertion(() =>
        {
            var itemSelectButtons = cut.FindAll("div[opta-carousel-item-select] button");
            Assert.Contains("inactive", itemSelectButtons[0].ClassList);
            Assert.Contains("active", itemSelectButtons[1].ClassList);
            Assert.Contains("inactive", itemSelectButtons[2].ClassList);
        });
    }
}
