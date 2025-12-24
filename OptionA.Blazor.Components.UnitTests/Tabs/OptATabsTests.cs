using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Components.UnitTests.Tabs;

public class OptATabsTests : BunitContext
{
    private readonly Mock<ITabsDataProvider> _tabsDataProvider;

    public OptATabsTests()
    {
        _tabsDataProvider = new Mock<ITabsDataProvider>();
        _tabsDataProvider.Setup(p => p.ContainerClass).Returns("tabs-container");
        _tabsDataProvider.Setup(p => p.HeaderClass).Returns("tabs-header");
        _tabsDataProvider.Setup(p => p.ActiveTabClass).Returns("active-tab");
        _tabsDataProvider.Setup(p => p.TabClass).Returns("tab");
        _tabsDataProvider.Setup(p => p.TabItemClass).Returns("tab-item");
        
        Services.AddSingleton(_tabsDataProvider.Object);
    }

    [Fact]
    public void OptATabsRendersCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptATabs>();

        // Assert
        var menu = cut.Find("menu");
        Assert.NotNull(menu);
    }

    [Fact]
    public void OptATabsRendersChildContent()
    {
        // Arrange & Act
        var cut = Render<OptATabs>(parameters => parameters
            .AddChildContent("<div>Test Content</div>"));

        // Assert
        Assert.Contains("Test Content", cut.Markup);
    }
}
