using Microsoft.Extensions.DependencyInjection;

namespace OptionA.Blazor.Playground.UnitTests.Configuration;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddOptionABootstrapPlayground_PrefillsBootstrapFriendlyDefaults()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddOptionABootstrapPlayground();
        using var provider = services.BuildServiceProvider();
        var dataProvider = provider.GetRequiredService<IPlaygroundDataProvider>();

        // Assert
        Assert.Equal("card", dataProvider.DefaultPlaygroundClass);
        Assert.Equal("card-body", dataProvider.DefaultPreviewClass);
        Assert.Equal("card-body", dataProvider.DefaultEditorClass);
        Assert.Equal("card-body bg-light", dataProvider.DefaultCodeClass);
        Assert.Equal("form-label", dataProvider.DefaultEditorLabelClass);
        Assert.Equal("form-control", dataProvider.DefaultEditorInputClass);
        Assert.Equal("fw-bold mb-2mt-3", dataProvider.DefaultEditorGroupClass);
    }
}
