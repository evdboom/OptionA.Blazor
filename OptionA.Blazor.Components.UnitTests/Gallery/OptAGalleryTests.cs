using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Components.UnitTests.Gallery;

public class OptAGalleryTests : BunitContext
{
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
    }

    [Fact]
    public void OptAGalleryRendersCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptAGallery>();

        // Assert
        var gallery = cut.Find("div[opta-gallery]");
        Assert.NotNull(gallery);
    }

    [Fact]
    public void OptAGallerySetsModeAttribute()
    {
        // Arrange & Act
        var cut = Render<OptAGallery>(parameters => parameters
            .Add(p => p.Mode, GalleryMode.Modal));

        // Assert
        var gallery = cut.Find("div[opta-gallery]");
        Assert.Equal("modal", gallery.GetAttribute("mode"));
    }
}
