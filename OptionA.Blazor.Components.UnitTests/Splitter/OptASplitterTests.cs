using Microsoft.Extensions.DependencyInjection;
using Moq;
using Bunit;

namespace OptionA.Blazor.Components.UnitTests.Splitter;

public class OptASplitterTests : BunitContext
{
    private readonly Mock<ISplitterDataProvider> _splitterDataProvider;

    public OptASplitterTests()
    {
        _splitterDataProvider = new Mock<ISplitterDataProvider>();
        _splitterDataProvider.Setup(p => p.DragMode).Returns(DragMode.Direct);
        _splitterDataProvider.Setup(p => p.DragBarClass).Returns("splitter-bar");
        _splitterDataProvider.Setup(p => p.OutlineClass).Returns("splitter-outline");
        _splitterDataProvider.Setup(p => p.DragBarContent).Returns("");
        
        Services.AddSingleton(_splitterDataProvider.Object);
        
        // Setup JS Interop for the module
        JSInterop.SetupModule("./_content/OptionA.Blazor.Components/Splitter/OptASplitter.razor.js");
    }

    [Fact]
    public void OptASplitterRendersCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptASplitter>();

        // Assert
        var splitter = cut.Find("div[opta-splitter]");
        Assert.NotNull(splitter);
    }

    [Fact]
    public void OptASplitterRendersFirstFragment()
    {
        // Arrange & Act
        var cut = Render<OptASplitter>(parameters => parameters
            .Add(p => p.First, "<div>First Content</div>"));

        // Assert
        Assert.Contains("First Content", cut.Markup);
    }

    [Fact]
    public void OptASplitterRendersSecondFragment()
    {
        // Arrange & Act
        var cut = Render<OptASplitter>(parameters => parameters
            .Add(p => p.Second, "<div>Second Content</div>"));

        // Assert
        Assert.Contains("Second Content", cut.Markup);
    }

    [Fact]
    public void OptASplitterAcceptsOrientationParameter()
    {
        // Arrange & Act
        var cut = Render<OptASplitter>(parameters => parameters
            .Add(p => p.Orientation, Orientation.Vertical));

        // Assert - Splitter component accepts Orientation parameter
        var splitter = cut.Find("div[opta-splitter]");
        Assert.NotNull(splitter);
    }
}
