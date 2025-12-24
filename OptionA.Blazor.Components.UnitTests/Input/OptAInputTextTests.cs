namespace OptionA.Blazor.Components.UnitTests.Input;

public class OptAInputTextTests : BunitContext
{
    [Fact]
    public void OptAInputTextRendersCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptAInputText>();

        // Assert
        var input = cut.Find("input[opta-input-text]");
        Assert.NotNull(input);
    }

    [Fact]
    public void OptAInputTextBindsValueCorrectly()
    {
        // Arrange
        var value = "Test Value";

        // Act
        var cut = Render<OptAInputText>(parameters => parameters
            .Add(p => p.Value, value));

        // Assert
        var input = cut.Find("input[opta-input-text]");
        Assert.Equal(value, input.GetAttribute("value"));
    }

    [Fact]
    public void OptAInputTextInvokesValueChangedWhenInputChanges()
    {
        // Arrange
        var valueChanged = false;
        var newValue = string.Empty;

        var cut = Render<OptAInputText>(parameters => parameters
            .Add(p => p.Value, "Initial")
            .Add(p => p.ValueChanged, (string? v) => { valueChanged = true; newValue = v ?? string.Empty; }));

        // Act
        var input = cut.Find("input[opta-input-text]");
        input.Input("New Value");

        // Assert
        Assert.True(valueChanged);
        Assert.Equal("New Value", newValue);
    }
}
