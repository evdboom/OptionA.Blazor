using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Components.UnitTests.Buttons;

public class OptAButtonTests : BunitContext
{
    private readonly Mock<IButtonDataProvider> _buttonDataProvider;

    public OptAButtonTests()
    {
        _buttonDataProvider = new Mock<IButtonDataProvider>();
        Services.AddSingleton(_buttonDataProvider.Object);
    }

    [Fact]
    public void OptAButtonRendersCorrectlyWithIconAndName()
    {
        // Arrange
        _buttonDataProvider.Setup(p => p.GetIconClass(It.IsAny<ActionType>(), It.IsAny<string>())).Returns("test-icon");
        var cut = Render<OptAButton>(parameters => parameters
            .Add(p => p.ButtonType, ButtonTypes.Icon | ButtonTypes.Name)
            .Add(p => p.Name, "Test Button")
            .Add(p => p.ActionType, ActionType.Add));

        // Assert
        Assert.Contains(cut.FindAll("i"), i => i.ClassList.Contains("test-icon"));
        Assert.Contains(cut.FindAll("span"), span => span.TextContent == "Test Button");
    }

    [Fact]
    public void OptAButtonRendersCorrectlyWithOnlyIcon()
    {
        // Arrange
        _buttonDataProvider.Setup(p => p.GetIconClass(It.IsAny<ActionType>(), It.IsAny<string>())).Returns("test-icon");
        var cut = Render<OptAButton>(parameters => parameters
            .Add(p => p.ButtonType, ButtonTypes.Icon)
            .Add(p => p.ActionType, ActionType.Add));

        // Assert
        Assert.Contains(cut.FindAll("i"), i => i.ClassList.Contains("test-icon"));
        Assert.Empty(cut.FindAll("span"));
    }

    [Fact]
    public void OptAButtonRendersCorrectlyWithOnlyName()
    {
        // Arrange
        var cut = Render<OptAButton>(parameters => parameters
            .Add(p => p.ButtonType, ButtonTypes.Name)
            .Add(p => p.Name, "Test Button"));

        // Assert
        Assert.Contains(cut.FindAll("span"), span => span.TextContent == "Test Button");
        Assert.Empty(cut.FindAll("i"));
    }

    [Fact]
    public void OptAButtonInvokesClickActionWhenClicked()
    {
        // Arrange
        var clickActionMock = new Mock<Func<MouseEventArgs, Task>>();
        var cut = Render<OptAButton>(parameters => parameters
            .Add(p => p.ClickAction, clickActionMock.Object));

        // Act
        cut.Find("button").Click();

        // Assert
        clickActionMock.Verify(f => f(It.IsAny<MouseEventArgs>()), Times.Once);
    }
}

