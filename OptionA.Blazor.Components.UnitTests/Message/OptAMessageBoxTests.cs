using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Components.UnitTests.Message;

public class OptAMessageBoxTests : BunitContext
{
    private readonly Mock<IMessageBoxDataProvider> _messageDataProvider;
    private readonly Mock<IMessageService> _messageService;

    public OptAMessageBoxTests()
    {
        _messageDataProvider = new Mock<IMessageBoxDataProvider>();
        _messageDataProvider.Setup(p => p.ContainerClass).Returns("container");
        _messageDataProvider.Setup(p => p.DefaultZIndex).Returns(1000);
        _messageDataProvider.Setup(p => p.DefaultLocation).Returns(Location.Top | Location.Right);
        _messageDataProvider.Setup(p => p.GetDefaultTimeout(It.IsAny<MessageType>())).Returns(5000);
        _messageDataProvider.Setup(p => p.GetDefaultDismissable(It.IsAny<MessageType>())).Returns(true);
        _messageDataProvider.Setup(p => p.GetMessageClasses(It.IsAny<MessageType>())).Returns("message-class");
        _messageDataProvider.Setup(p => p.GetCloseButtonClasses(It.IsAny<MessageType>())).Returns("close-btn");
        _messageDataProvider.Setup(p => p.CloseButtonContent).Returns("X");
        _messageDataProvider.Setup(p => p.ShowTime).Returns(false);
        _messageDataProvider.Setup(p => p.HeaderClass).Returns("header");
        _messageDataProvider.Setup(p => p.ContentClass).Returns("content");
        _messageDataProvider.Setup(p => p.BodyClass).Returns("body");

        _messageService = new Mock<IMessageService>();
        
        Services.AddSingleton(_messageDataProvider.Object);
        Services.AddSingleton(_messageService.Object);
    }

    [Fact]
    public void OptAMessageBoxRendersCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptAMessageBox>();

        // Assert
        var messageBox = cut.Find("div[opta-message-box]");
        Assert.NotNull(messageBox);
    }

    [Fact]
    public void OptAMessageBoxSetsLocationAttribute()
    {
        // Arrange & Act
        var cut = Render<OptAMessageBox>(parameters => parameters
            .Add(p => p.Location, Location.Bottom | Location.Left));

        // Assert
        var messageBox = cut.Find("div[opta-message-box]");
        Assert.Equal("bottomleft", messageBox.GetAttribute("location"));
    }

    [Fact]
    public void OptAMessageBoxUsesDefaultLocationWhenNotSet()
    {
        // Arrange & Act
        var cut = Render<OptAMessageBox>();

        // Assert
        var messageBox = cut.Find("div[opta-message-box]");
        Assert.Equal("topright", messageBox.GetAttribute("location"));
    }
}
