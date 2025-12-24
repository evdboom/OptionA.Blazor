namespace OptionA.Blazor.Components.UnitTests.Input;

public class OptAInputIntegerTests : BunitContext
{
    [Fact]
    public void OptAInputIntegerRendersCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptAInputInteger>();

        // Assert
        var input = cut.Find("input[opta-input-integer]");
        Assert.NotNull(input);
    }

    [Fact]
    public void OptAInputIntegerBindsValueCorrectly()
    {
        // Arrange
        var value = 42;

        // Act
        var cut = Render<OptAInputInteger>(parameters => parameters
            .Add(p => p.Value, value));

        // Assert
        var input = cut.Find("input[opta-input-integer]");
        Assert.Equal(value.ToString(), input.GetAttribute("value"));
    }

    [Fact]
    public void OptAInputIntegerInvokesValueChangedWhenInputChanges()
    {
        // Arrange
        var valueChanged = false;
        int? newValue = null;

        var cut = Render<OptAInputInteger>(parameters => parameters
            .Add(p => p.Value, 10)
            .Add(p => p.ValueChanged, (int? v) => { valueChanged = true; newValue = v; }));

        // Act
        var input = cut.Find("input[opta-input-integer]");
        input.Input("20");

        // Assert
        Assert.True(valueChanged);
        Assert.Equal(20, newValue);
    }
}
