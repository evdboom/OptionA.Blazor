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
        
        // Setup JS Interop for the module
        JSInterop.SetupModule("./_content/OptionA.Blazor.Components/Modal/OptAModal.razor.js");
    }

    [Fact]
    public void OptAModalCanBeConstructed()
    {
        // Arrange & Act
        var cut = Render<OptAModal>();

        // Assert - The modal component can be instantiated
        Assert.NotNull(cut);
    }

    [Fact]
    public void OptAModalAcceptsHeaderParameter()
    {
        // Arrange & Act
        var cut = Render<OptAModal>(parameters => parameters
            .Add(p => p.Header, "<h1>Test Header</h1>"));

        // Assert - Modal component accepts Header parameter
        Assert.NotNull(cut);
    }

    [Fact]
    public void OptAModalAcceptsContentParameter()
    {
        // Arrange & Act
        var cut = Render<OptAModal>(parameters => parameters
            .Add(p => p.Content, "<div>Test Content</div>"));

        // Assert - Modal component accepts Content parameter
        Assert.NotNull(cut);
    }

    [Fact]
    public void OptAModalAcceptsFooterParameter()
    {
        // Arrange & Act
        var cut = Render<OptAModal>(parameters => parameters
            .Add(p => p.Footer, "<div>Test Footer</div>"));

        // Assert - Modal component accepts Footer parameter
        Assert.NotNull(cut);
    }
}
