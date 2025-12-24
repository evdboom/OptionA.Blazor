using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Components.UnitTests.Buttons;

public class OptAButtonBarTests : BunitContext
{
    private readonly Mock<IButtonDataProvider> _buttonDataProvider;

    public OptAButtonBarTests()
    {
        _buttonDataProvider = new Mock<IButtonDataProvider>();
        _buttonDataProvider.Setup(p => p.DefaultButtonBarClass).Returns("button-bar");
        _buttonDataProvider.Setup(p => p.DefaultButtonGroupClass).Returns("button-group");
        
        Services.AddSingleton(_buttonDataProvider.Object);
    }

    [Fact]
    public void OptAButtonBarRendersCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptAButtonBar>();

        // Assert
        var bar = cut.Find("div[opta-button-bar]");
        Assert.NotNull(bar);
    }

    [Fact]
    public void OptAButtonBarRendersStartButtons()
    {
        // Arrange & Act
        var cut = Render<OptAButtonBar>(parameters => parameters
            .Add(p => p.StartButtons, "<button>Start Button</button>"));

        // Assert
        var group = cut.Find("div[button-alignment='start']");
        Assert.NotNull(group);
        Assert.Contains("Start Button", cut.Markup);
    }

    [Fact]
    public void OptAButtonBarRendersCenterButtons()
    {
        // Arrange & Act
        var cut = Render<OptAButtonBar>(parameters => parameters
            .Add(p => p.CenterButtons, "<button>Center Button</button>"));

        // Assert
        var group = cut.Find("div[button-alignment='center']");
        Assert.NotNull(group);
        Assert.Contains("Center Button", cut.Markup);
    }

    [Fact]
    public void OptAButtonBarRendersEndButtons()
    {
        // Arrange & Act
        var cut = Render<OptAButtonBar>(parameters => parameters
            .Add(p => p.EndButtons, "<button>End Button</button>"));

        // Assert
        var group = cut.Find("div[button-alignment='end']");
        Assert.NotNull(group);
        Assert.Contains("End Button", cut.Markup);
    }

    [Fact]
    public void OptAButtonBarSetsVerticalAttributeWhenOrientationIsVertical()
    {
        // Arrange & Act
        var cut = Render<OptAButtonBar>(parameters => parameters
            .Add(p => p.Orientation, Orientation.Vertical));

        // Assert
        var bar = cut.Find("div[opta-button-bar]");
        Assert.True(bar.HasAttribute("vertical"));
    }

    [Fact]
    public void OptAButtonBarSetsIdWhenProvided()
    {
        // Arrange & Act
        var cut = Render<OptAButtonBar>(parameters => parameters
            .Add(p => p.Id, "test-button-bar"));

        // Assert
        var bar = cut.Find("div[opta-button-bar]");
        Assert.Equal("test-button-bar", bar.GetAttribute("id"));
    }
}
