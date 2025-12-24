using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Components.UnitTests.Menu;

public class OptAMenuItemTests : BunitContext
{
    private readonly Mock<IMenuDataProvider> _menuDataProvider;

    public OptAMenuItemTests()
    {
        _menuDataProvider = new Mock<IMenuDataProvider>();
        _menuDataProvider.Setup(p => p.MenuItemClass).Returns("menu-item");
        _menuDataProvider.Setup(p => p.LinkClass).Returns("menu-item-link");
        
        Services.AddSingleton(_menuDataProvider.Object);
    }

    [Fact]
    public void OptAMenuItemRendersCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptAMenuItem>(parameters => parameters
            .Add(p => p.Name, "Test Item"));

        // Assert
        var li = cut.Find("li");
        Assert.NotNull(li);
        Assert.Contains("Test Item", cut.Markup);
    }

    [Fact]
    public void OptAMenuItemRendersLinkWhenNameIsProvided()
    {
        // Arrange & Act
        var cut = Render<OptAMenuItem>(parameters => parameters
            .Add(p => p.Name, "Test Item"));

        // Assert
        var link = cut.Find("a");
        Assert.NotNull(link);
        Assert.Equal("Test Item", link.TextContent);
    }

    [Fact]
    public void OptAMenuItemRendersChildContent()
    {
        // Arrange & Act
        var cut = Render<OptAMenuItem>(parameters => parameters
            .AddChildContent("<span>Custom Content</span>"));

        // Assert
        Assert.Contains("Custom Content", cut.Markup);
    }
}
