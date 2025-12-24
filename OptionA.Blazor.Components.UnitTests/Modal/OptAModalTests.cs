using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Components.UnitTests.Modal;

public class OptAModalTests : BunitContext
{
    private readonly Mock<IModalDataProvider> _modalDataProvider;

    public OptAModalTests()
    {
        _modalDataProvider = new Mock<IModalDataProvider>();
        _modalDataProvider.Setup(p => p.DialogClass).Returns("modal-dialog");
        _modalDataProvider.Setup(p => p.SectionClass).Returns("modal-section");
        _modalDataProvider.Setup(p => p.HeaderClass).Returns("modal-header");
        _modalDataProvider.Setup(p => p.ContentClass).Returns("modal-content");
        _modalDataProvider.Setup(p => p.FooterClass).Returns("modal-footer");
        _modalDataProvider.Setup(p => p.CloseButtonClass).Returns("close-btn");
        _modalDataProvider.Setup(p => p.CloseButtonContent).Returns("X");
        _modalDataProvider.Setup(p => p.OutlineClass).Returns("outline");
        _modalDataProvider.Setup(p => p.Draggable).Returns(false);
        _modalDataProvider.Setup(p => p.DragMode).Returns(DragMode.Direct);
        
        Services.AddSingleton(_modalDataProvider.Object);
    }

    [Fact]
    public void OptAModalDoesNotRenderWhenNotShown()
    {
        // Arrange & Act
        var cut = Render<OptAModal>();

        // Assert
        Assert.Throws<Bunit.ElementNotFoundException>(() => cut.Find("dialog"));
    }

    [Fact]
    public void OptAModalRendersHeaderWhenProvided()
    {
        // Arrange & Act
        var cut = Render<OptAModal>(parameters => parameters
            .Add(p => p.Header, "<h1>Test Header</h1>"));

        // Assert (Modal should render when Header is provided)
        Assert.Contains("Test Header", cut.Markup);
    }

    [Fact]
    public void OptAModalRendersContentWhenProvided()
    {
        // Arrange & Act
        var cut = Render<OptAModal>(parameters => parameters
            .Add(p => p.Content, "<div>Test Content</div>"));

        // Assert
        Assert.Contains("Test Content", cut.Markup);
    }

    [Fact]
    public void OptAModalRendersFooterWhenProvided()
    {
        // Arrange & Act
        var cut = Render<OptAModal>(parameters => parameters
            .Add(p => p.Footer, "<div>Test Footer</div>"));

        // Assert
        Assert.Contains("Test Footer", cut.Markup);
    }
}
