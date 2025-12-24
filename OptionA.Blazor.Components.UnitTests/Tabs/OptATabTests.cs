using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Components.UnitTests.Tabs;

public class OptATabTests : BunitContext
{
    private readonly Mock<ITabsDataProvider> _tabsDataProvider;

    public OptATabTests()
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
    public void OptATabRendersChildContentWhenIsCurrent()
    {
        // Arrange & Act
        var tab = new OptATab { IsCurrent = true };
        var cut = Render<OptATab>(parameters => parameters
            .AddChildContent("<div>Tab Content</div>"));

        // Set IsCurrent manually since we're not testing with a parent
        cut.Instance.IsCurrent = true;
        cut.Render();

        // Assert
        Assert.Contains("Tab Content", cut.Markup);
    }

    [Fact]
    public void OptATabDoesNotRenderChildContentWhenNotCurrent()
    {
        // Arrange & Act
        var cut = Render<OptATab>(parameters => parameters
            .AddChildContent("<div>Tab Content</div>"));

        // Assert (when IsCurrent is false, content should not be rendered)
        // The tab component only renders when IsCurrent is true
        Assert.DoesNotContain("Tab Content", cut.Markup);
    }
}
