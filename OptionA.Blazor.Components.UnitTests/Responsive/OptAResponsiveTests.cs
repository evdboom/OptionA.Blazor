using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Components.UnitTests.Responsive;

public class OptAResponsiveTests : BunitContext
{
    private readonly Mock<IResponsiveService> _responsiveService;

    public OptAResponsiveTests()
    {
        _responsiveService = new Mock<IResponsiveService>();
        _responsiveService.Setup(s => s.Initialize()).Returns(Task.CompletedTask);
        _responsiveService.Setup(s => s.GetWindowSize()).Returns(new NamedDimension { Name = "Desktop", Width = 1920, Height = 1080 });
        _responsiveService.Setup(s => s.ValidDimensions()).Returns(new[] { "Mobile", "Tablet", "Desktop" });
        _responsiveService.Setup(s => s.GetAllDimensionBreakPoints()).Returns(new[] { ("Mobile", 768), ("Tablet", 1024), ("Desktop", 1920) });
        
        Services.AddSingleton(_responsiveService.Object);
    }

    [Fact]
    public async Task OptAResponsiveInitializesService()
    {
        // Arrange & Act
        var cut = Render<OptAResponsive>();
        await Task.Delay(100); // Give initialization time to complete

        // Assert
        _responsiveService.Verify(s => s.Initialize(), Times.Once);
    }

    [Fact]
    public void OptAResponsiveRendersChildContent()
    {
        // Arrange & Act
        var cut = Render<OptAResponsive>(parameters => parameters
            .AddChildContent("<div>Responsive Content</div>"));

        // Assert
        Assert.Contains("Responsive Content", cut.Markup);
    }

    [Fact]
    public async Task OptAResponsiveGetsWindowSize()
    {
        // Arrange & Act
        var cut = Render<OptAResponsive>();
        await Task.Delay(100); // Give initialization time to complete

        // Assert
        _responsiveService.Verify(s => s.GetWindowSize(), Times.AtLeastOnce);
    }
}
