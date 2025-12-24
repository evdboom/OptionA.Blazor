namespace OptionA.Blazor.Components.UnitTests.Input;

public class OptAInputCheckboxTests : BunitContext
{
    [Fact]
    public void OptAInputCheckboxRendersCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptAInputCheckbox>();

        // Assert
        var checkbox = cut.Find("input[type='checkbox'][opta-input-checkbox]");
        Assert.NotNull(checkbox);
    }

    [Fact]
    public void OptAInputCheckboxBindsValueCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptAInputCheckbox>(parameters => parameters
            .Add(p => p.Value, true));

        // Assert
        var checkbox = cut.Find("input[type='checkbox'][opta-input-checkbox]");
        Assert.True(checkbox.HasAttribute("checked"));
    }

    [Fact]
    public void OptAInputCheckboxRendersLabelWhenDescriptionIsProvided()
    {
        // Arrange & Act
        var cut = Render<OptAInputCheckbox>(parameters => parameters
            .Add(p => p.Description, "Test Label"));

        // Assert
        var label = cut.Find("label");
        Assert.NotNull(label);
        Assert.Contains("Test Label", label.TextContent);
    }

    [Fact]
    public void OptAInputCheckboxInvokesValueChangedWhenClicked()
    {
        // Arrange
        var valueChanged = false;
        var newValue = false;

        var cut = Render<OptAInputCheckbox>(parameters => parameters
            .Add(p => p.Value, false)
            .Add(p => p.ValueChanged, (bool v) => { valueChanged = true; newValue = v; }));

        // Act
        var checkbox = cut.Find("input[type='checkbox'][opta-input-checkbox]");
        checkbox.Change(true);

        // Assert
        Assert.True(valueChanged);
        Assert.True(newValue);
    }
}
