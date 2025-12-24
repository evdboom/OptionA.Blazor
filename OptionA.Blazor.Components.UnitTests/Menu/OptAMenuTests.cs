using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Components.UnitTests.Menu;

public class OptAMenuTests : BunitContext
{
    private readonly Mock<IMenuDataProvider> _menuDataProvider;

    public OptAMenuTests()
    {
        _menuDataProvider = new Mock<IMenuDataProvider>();
        _menuDataProvider.Setup(p => p.MenuClass).Returns("menu-class");
        _menuDataProvider.Setup(p => p.MenuContainerClass).Returns("menu-container");
        _menuDataProvider.Setup(p => p.MenuItemClass).Returns("menu-item");
        _menuDataProvider.Setup(p => p.GroupClass).Returns("menu-group");
        _menuDataProvider.Setup(p => p.DividerClass).Returns("menu-divider");
        _menuDataProvider.Setup(p => p.LinkClass).Returns("menu-link");
        _menuDataProvider.Setup(p => p.ActiveClass).Returns("menu-active");
        _menuDataProvider.Setup(p => p.DropdownClass).Returns("menu-dropdown");
        _menuDataProvider.Setup(p => p.OpenGroupOnMouseOver).Returns(false);
        _menuDataProvider.Setup(p => p.GroupCloseTime).Returns(0);
        
        Services.AddSingleton(_menuDataProvider.Object);
    }

    [Fact]
    public void OptAMenuRendersCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptAMenu>();

        // Assert
        var nav = cut.Find("nav");
        var menu = cut.Find("menu[opta-menu]");
        Assert.NotNull(nav);
        Assert.NotNull(menu);
    }

    [Fact]
    public void OptAMenuRendersChildContent()
    {
        // Arrange & Act
        var cut = Render<OptAMenu>(parameters => parameters
            .AddChildContent("<li>Menu Item</li>"));

        // Assert
        Assert.Contains("Menu Item", cut.Markup);
    }

    [Fact]
    public void OptAMenuSetsVerticalAttributeWhenOrientationIsVertical()
    {
        // Arrange & Act
        var cut = Render<OptAMenu>(parameters => parameters
            .Add(p => p.Orientation, Orientation.Vertical));

        // Assert
        var menu = cut.Find("menu[opta-menu]");
        Assert.True(menu.HasAttribute("vertical"));
    }

    [Fact]
    public void OptAMenuDoesNotSetVerticalAttributeWhenOrientationIsHorizontal()
    {
        // Arrange & Act
        var cut = Render<OptAMenu>(parameters => parameters
            .Add(p => p.Orientation, Orientation.Horizontal));

        // Assert
        var menu = cut.Find("menu[opta-menu]");
        Assert.False(menu.HasAttribute("vertical"));
    }
}
