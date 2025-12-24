using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Components.UnitTests.Menu;

public class OptAMenuGroupTests : BunitContext
{
    private readonly Mock<IMenuDataProvider> _menuDataProvider;

    public OptAMenuGroupTests()
    {
        _menuDataProvider = new Mock<IMenuDataProvider>();
        _menuDataProvider.Setup(p => p.GroupClass).Returns("menu-group");
        _menuDataProvider.Setup(p => p.LinkClass).Returns("menu-group-link");
        _menuDataProvider.Setup(p => p.DropdownClass).Returns("menu-group-dropdown");
        _menuDataProvider.Setup(p => p.OpenGroupOnMouseOver).Returns(false);
        
        Services.AddSingleton(_menuDataProvider.Object);
        
        // Setup JS Interop for the module and methods
        var module = JSInterop.SetupModule("./_content/OptionA.Blazor.Components/Menu/OptAMenuGroup.razor.js");
        module.Setup<int>("getScrollHeight", _ => true).SetResult(100);
    }

    [Fact]
    public void OptAMenuGroupRendersCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptAMenuGroup>(parameters => parameters
            .Add(p => p.Name, "Test Group"));

        // Assert
        var li = cut.Find("li");
        Assert.NotNull(li);
        Assert.Contains("Test Group", cut.Markup);
    }

    [Fact]
    public void OptAMenuGroupRendersDropdownMenu()
    {
        // Arrange & Act
        var cut = Render<OptAMenuGroup>(parameters => parameters
            .Add(p => p.Name, "Test Group")
            .AddChildContent("<li>Group Item</li>"));

        // Assert
        var menu = cut.Find("menu");
        Assert.NotNull(menu);
        Assert.Contains("Group Item", cut.Markup);
    }

    [Fact]
    public void OptAMenuGroupRendersLink()
    {
        // Arrange & Act
        var cut = Render<OptAMenuGroup>(parameters => parameters
            .Add(p => p.Name, "Test Group"));

        // Assert
        var link = cut.Find("a");
        Assert.NotNull(link);
        Assert.Equal("Test Group", link.TextContent);
    }
}
