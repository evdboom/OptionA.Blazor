using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Components.UnitTests.Menu;

public class OptAMenuDividerTests : BunitContext
{
    private readonly Mock<IMenuDataProvider> _menuDataProvider;

    public OptAMenuDividerTests()
    {
        _menuDataProvider = new Mock<IMenuDataProvider>();
        _menuDataProvider.Setup(p => p.DividerClass).Returns("menu-divider");
        
        Services.AddSingleton(_menuDataProvider.Object);
    }

    [Fact]
    public void OptAMenuDividerRendersCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptAMenuDivider>();

        // Assert
        var li = cut.Find("li[opta-menu-divider]");
        var hr = cut.Find("hr");
        Assert.NotNull(li);
        Assert.NotNull(hr);
    }
}
