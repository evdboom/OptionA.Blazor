using Microsoft.Extensions.DependencyInjection;
using OptionA.Blazor.Interactive;
using OptionA.Blazor.Interactive.Editors;
using OptionA.Blazor.Interactive.Exporters;
using OptionA.Blazor.Interactive.Interfaces;
using OptionA.Blazor.Playground;

namespace OptionA.Blazor.Playground.UnitTests.Configuration;

public class InteractiveServiceCollectionExtensionsTests
{
    [Fact]
    public void AddOptionABootstrapInteractive_RegistersInteractiveDefaults()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddOptionABootstrapInteractive();
        using var provider = services.BuildServiceProvider();

        var interactive = provider.GetRequiredService<IInteractiveDataProvider>();
        var playground = provider.GetRequiredService<IPlaygroundDataProvider>();

        // Assert
        Assert.Same(interactive, playground);
        Assert.Equal("card", interactive.DefaultInteractiveClass);
        Assert.Equal("card", interactive.DefaultPlaygroundClass);
        Assert.Equal("card-body", interactive.DefaultPreviewClass);
        Assert.Equal("card-body", interactive.DefaultEditorClass);
        Assert.Equal("card-body bg-light", interactive.DefaultCodeClass);
        Assert.Equal("form-label", interactive.DefaultEditorLabelClass);
        Assert.Equal("form-control", interactive.DefaultEditorInputClass);
        Assert.Equal("fw-bold mb-2 mt-3", interactive.DefaultEditorGroupClass);
        Assert.True(interactive.CodeEditingEnabled);
        Assert.Equal(InteractiveEditorKind.TextArea, interactive.PreferredCodeEditor);
        Assert.Equal("razor", interactive.DefaultCodeLanguage);
        Assert.Contains(InteractiveExportFormat.Razor, interactive.EnabledExportFormats);
        Assert.Contains(InteractiveExportFormat.Json, interactive.EnabledExportFormats);
    }
}
