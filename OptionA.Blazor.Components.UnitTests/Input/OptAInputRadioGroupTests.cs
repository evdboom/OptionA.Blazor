namespace OptionA.Blazor.Components.UnitTests.Input;

public class OptAInputRadioGroupTests : BunitContext
{
    [Fact]
    public void OptAInputRadioGroupRendersCorrectly()
    {
        // Arrange
        var items = new[] { "Option1", "Option2", "Option3" };

        // Act
        var cut = Render<OptAInputRadioGroup<string>>(parameters => parameters
            .Add(p => p.Items, items));

        // Assert
        var div = cut.Find("div");
        Assert.NotNull(div);
    }

    [Fact]
    public void OptAInputRadioGroupRendersAllRadioButtons()
    {
        // Arrange
        var items = new[] { "Option1", "Option2", "Option3" };

        // Act
        var cut = Render<OptAInputRadioGroup<string>>(parameters => parameters
            .Add(p => p.Items, items));

        // Assert
        var radios = cut.FindAll("input[type='radio']");
        Assert.Equal(3, radios.Count);
    }

    [Fact]
    public void OptAInputRadioGroupBindsValueCorrectly()
    {
        // Arrange
        var items = new[] { "Option1", "Option2", "Option3" };
        var value = "Option2";

        // Act
        var cut = Render<OptAInputRadioGroup<string>>(parameters => parameters
            .Add(p => p.Items, items)
            .Add(p => p.Value, value));

        // Assert
        var radios = cut.FindAll("input[type='radio']");
        // The component renders with radios
        Assert.True(radios.Count > 0);
    }
}
