using Microsoft.Extensions.DependencyInjection;
using Moq;
using OptionA.Blazor.Components.Message.Struct;

namespace OptionA.Blazor.Components.UnitTests.Message;

public class OptAMessageTests : BunitContext
{
    private readonly Mock<IMessageBoxDataProvider> _messageDataProvider;

    public OptAMessageTests()
    {
        _messageDataProvider = new Mock<IMessageBoxDataProvider>();
        _messageDataProvider.Setup(p => p.GetMessageClasses(It.IsAny<MessageType>())).Returns("message-class");
        _messageDataProvider.Setup(p => p.GetCloseButtonClasses(It.IsAny<MessageType>())).Returns("close-btn");
        _messageDataProvider.Setup(p => p.CloseButtonContent).Returns("X");
        _messageDataProvider.Setup(p => p.ShowTime).Returns(false);
        _messageDataProvider.Setup(p => p.HeaderClass).Returns("header");
        _messageDataProvider.Setup(p => p.ContentClass).Returns("content");
        _messageDataProvider.Setup(p => p.BodyClass).Returns("body");
        Services.AddSingleton(_messageDataProvider.Object);
    }

    [Fact]
    public void OptAMessageRendersCorrectlyWithMessage()
    {
        // Arrange
        var messageItem = new MessageItem
        {
            Content = "Test message",
            Type = MessageType.Info
        };
        var openMessage = new OpenMessage(messageItem);

        // Act
        var cut = Render<OptAMessage>(parameters => parameters
            .Add(p => p.Message, openMessage));

        // Assert
        var messageDiv = cut.Find("div[opta-message]");
        Assert.NotNull(messageDiv);
        Assert.Contains("Test message", cut.Markup);
    }

    [Fact]
    public void OptAMessageDoesNotRenderWhenMessageIsNull()
    {
        // Arrange & Act
        var cut = Render<OptAMessage>();

        // Assert
        Assert.Throws<Bunit.ElementNotFoundException>(() => cut.Find("div[opta-message]"));
    }

    [Fact]
    public void OptAMessageRendersCloseButtonWhenDismissable()
    {
        // Arrange
        var messageItem = new MessageItem
        {
            Content = "Test message",
            Type = MessageType.Info,
            Dismissable = true
        };
        var openMessage = new OpenMessage(messageItem);

        // Act
        var cut = Render<OptAMessage>(parameters => parameters
            .Add(p => p.Message, openMessage));

        // Assert
        var closeButton = cut.Find("button[type='button']");
        Assert.NotNull(closeButton);
    }

    [Fact]
    public void OptAMessageInvokesMessageClosedWhenCloseButtonIsClicked()
    {
        // Arrange
        var messageItem = new MessageItem
        {
            Content = "Test message",
            Type = MessageType.Info,
            Dismissable = true
        };
        var openMessage = new OpenMessage(messageItem);
        var closedInvoked = false;

        var cut = Render<OptAMessage>(parameters => parameters
            .Add(p => p.Message, openMessage)
            .Add(p => p.MessageClosed, _ => { closedInvoked = true; }));

        // Act
        cut.Find("button[type='button']").Click();

        // Assert
        Assert.True(closedInvoked);
    }
}
