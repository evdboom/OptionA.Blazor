using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Components.UnitTests.Gallery;

public class OptAGalleryTests : BunitContext
{
    private static RenderFragment GalleryImages => builder =>
    {
        AddImage(builder, 0, 1, "first.jpg", "First image", "First image description");
        AddImage(builder, 10, 2, "second.jpg", "Second image", "Second image description", thumbnailUrl: "second-thumb.jpg");
        AddImage(builder, 20, 3, "third.jpg", "Third image", "Third image description");
    };

    private readonly Mock<IGalleryDataProvider> _galleryDataProvider;

    public OptAGalleryTests()
    {
        _galleryDataProvider = new Mock<IGalleryDataProvider>();
        _galleryDataProvider.Setup(p => p.GetGalleryClasses(It.IsAny<GalleryMode>())).Returns("gallery-container");
        _galleryDataProvider.Setup(p => p.GetImageContainerClasses(It.IsAny<GalleryMode>())).Returns("image-container");
        _galleryDataProvider.Setup(p => p.GetThumbnailContainerClasses(It.IsAny<GalleryMode>())).Returns("thumbnail-container");
        _galleryDataProvider.Setup(p => p.GetDefaultThumbnailClasses()).Returns("thumbnail");
        _galleryDataProvider.Setup(p => p.GetDefaultImageClasses()).Returns("image");
        _galleryDataProvider.Setup(p => p.DefaultNextClasses()).Returns("next-btn");
        _galleryDataProvider.Setup(p => p.DefaultPreviousClasses()).Returns("previous-btn");
        _galleryDataProvider.Setup(p => p.DefaultNextIconClasses()).Returns("next-icon");
        _galleryDataProvider.Setup(p => p.DefaultPreviousIconClasses()).Returns("previous-icon");
        _galleryDataProvider.Setup(p => p.ModalCloseButtonClasses()).Returns("modal-close-btn");
        _galleryDataProvider.Setup(p => p.ModalBackgroundClasses()).Returns("modal-background");
        _galleryDataProvider.Setup(p => p.ModalClasses()).Returns("modal");
        _galleryDataProvider.Setup(p => p.ModalCloseButtonText()).Returns("X");
        _galleryDataProvider.Setup(p => p.ModalWrapperClasses()).Returns("modal-wrapper");
        
        Services.AddSingleton(_galleryDataProvider.Object);
        var thumbnailModule = JSInterop.SetupModule("./_content/OptionA.Blazor.Components/Gallery/OptAGalleryThumbnailContainer.razor.js");
        thumbnailModule.SetupVoid("scrollActiveIntoView", _ => true);
    }

    private static void AddImage(
        RenderTreeBuilder builder,
        int sequence,
        int imageNumber,
        string imageUrl,
        string imageText,
        string content,
        string? thumbnailUrl = null)
    {
        builder.OpenComponent<OptAGalleryImage>(sequence);
        builder.AddAttribute(sequence + 1, nameof(OptAGalleryImage.ImageNumber), imageNumber);
        builder.AddAttribute(sequence + 2, nameof(OptAGalleryImage.ImageUrl), imageUrl);
        builder.AddAttribute(sequence + 3, nameof(OptAGalleryImage.ImageText), imageText);
        if (!string.IsNullOrEmpty(thumbnailUrl))
        {
            builder.AddAttribute(sequence + 4, nameof(OptAGalleryImage.ImageThumbnailUrl), thumbnailUrl);
        }
        builder.AddAttribute(sequence + 5, nameof(OptAGalleryImage.ChildContent), (RenderFragment)(contentBuilder =>
        {
            contentBuilder.AddContent(0, content);
        }));
        builder.CloseComponent();
    }

    [Fact]
    public void OptAGalleryRendersSideBySideLayoutWithConfiguredAttributes()
    {
        var cut = Render<OptAGallery>(parameters => parameters
            .Add(p => p.Images, GalleryImages)
            .Add(p => p.Mode, GalleryMode.SideBySide)
            .Add(p => p.AdditionalThumbnailContainerClasses, "thumbnails-shell")
            .Add(p => p.AdditionalImageContainerClasses, "image-shell")
            .Add(p => p.ShowTitleOnThumbnail, true)
            .Add(p => p.ThumbnailContainerPercentage, 30)
            .Add(p => p.MaxThumbnailContainerHeight, "200px"));

        cut.WaitForAssertion(() =>
        {
            var gallery = cut.Find("div[opta-gallery]");
            var thumbnailContainer = cut.Find("div[opta-gallery-thumbnail-container]");
            var imageContainer = cut.Find("div[opta-gallery-images-container]");
            var thumbnails = cut.FindAll("figure[opta-index]");

            Assert.Contains("gallery-container", gallery.ClassList);
            Assert.Equal("side-by-side", thumbnailContainer.GetAttribute("gallery-mode"));
            Assert.Contains("thumbnails-shell", thumbnailContainer.ClassList);
            Assert.Contains("flex: 0 0 30%;", thumbnailContainer.GetAttribute("style"));
            Assert.Contains("max-height:200px;", thumbnailContainer.GetAttribute("style"));
            Assert.Contains("overflow:auto;", thumbnailContainer.GetAttribute("style"));
            Assert.Contains("image-shell", imageContainer.ClassList);
            Assert.Equal(3, thumbnails.Count);
            Assert.Contains(cut.FindAll("figcaption[opta-thumbnail-title]"), caption => caption.TextContent == "First image");
            Assert.Contains("First image description", cut.Markup);
        });
    }

    [Fact]
    public void OptAGalleryRendersThumbnailsWithPerRowSizingAndThumbnailUrls()
    {
        var cut = Render<OptAGallery>(parameters => parameters
            .Add(p => p.Images, GalleryImages)
            .Add(p => p.Mode, GalleryMode.Modal)
            .Add(p => p.AdditionalThumbnailContainerClasses, "thumbnails-shell")
            .Add(p => p.ShowTitleOnThumbnail, true)
            .Add(p => p.ThumbnailsPerRow, 4)
            .Add(p => p.ThumbnailsPerRowMargin, 8));

        cut.WaitForAssertion(() =>
        {
            var thumbnailContainer = cut.Find("div[opta-gallery-thumbnail-container]");

            Assert.Contains("thumbnail-container", thumbnailContainer.ClassList);
            Assert.Contains("thumbnails-shell", thumbnailContainer.ClassList);
            Assert.Equal("modal", thumbnailContainer.GetAttribute("gallery-mode"));
            Assert.Equal("second-thumb.jpg", cut.Find("figure[opta-index='1'] img").GetAttribute("src"));
            Assert.Equal("Second image", cut.Find("figure[opta-index='1'] figcaption[opta-thumbnail-title]").TextContent);
            Assert.Contains("flex: 0 0 23%", cut.Find("figure[opta-index='1']").GetAttribute("style"));
        });
    }

    [Fact]
    public void OptAGalleryOpensAndClosesModalWhenSelectingImages()
    {
        var cut = Render<OptAGallery>(parameters => parameters
            .Add(p => p.Images, GalleryImages)
            .Add(p => p.Mode, GalleryMode.Modal)
            .Add(p => p.MaxModalWidth, "80vw"));

        cut.WaitForAssertion(() =>
        {
            var modalWrapper = cut.Find("div[opta-modal-wrapper]");
            Assert.False(modalWrapper.HasAttribute("show"));
            Assert.Equal("max-width:80vw", cut.Find("div[opta-gallery-modal]").GetAttribute("style"));
        });

        cut.Find("figure[opta-index='1']").Click();

        cut.WaitForAssertion(() =>
        {
            var modalWrapper = cut.Find("div[opta-modal-wrapper]");
            Assert.True(modalWrapper.HasAttribute("show"));
            Assert.Equal("second.jpg", cut.Find("img[opta-gallery-image]").GetAttribute("src"));
        });

        cut.Find("button[opta-gallery-close]").Click();

        cut.WaitForAssertion(() =>
        {
            var modalWrapper = cut.Find("div[opta-modal-wrapper]");
            Assert.False(modalWrapper.HasAttribute("show"));
        });
    }

    [Fact]
    public void OptAGallerySelectsPreviousAndNextImagesWhenNavigationButtonsAreClicked()
    {
        var cut = Render<OptAGallery>(parameters => parameters
            .Add(p => p.Images, GalleryImages)
            .Add(p => p.Mode, GalleryMode.SideBySide));

        cut.WaitForAssertion(() => Assert.Equal("first.jpg", cut.Find("img[opta-gallery-image]").GetAttribute("src")));

        cut.Find("i.next-icon").ParentElement!.Click();

        cut.WaitForAssertion(() =>
        {
            Assert.Equal("second.jpg", cut.Find("img[opta-gallery-image]").GetAttribute("src"));
            Assert.True(cut.Find("figure[opta-index='1']").HasAttribute("active"));
        });

        cut.Find("i.previous-icon").ParentElement!.Click();

        cut.WaitForAssertion(() =>
        {
            Assert.Equal("first.jpg", cut.Find("img[opta-gallery-image]").GetAttribute("src"));
            Assert.True(cut.Find("figure[opta-index='0']").HasAttribute("active"));
        });
    }
}
