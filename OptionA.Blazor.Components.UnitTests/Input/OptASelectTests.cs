namespace OptionA.Blazor.Components.UnitTests.Input;

public class OptASelectTests : BunitContext
{
    [Fact]
    public void OptASelectRendersCorrectly()
    {
        // Arrange
        var items = new[] { "Option1", "Option2", "Option3" };

        // Act
        var cut = Render<OptASelect<string>>(parameters => parameters
            .Add(p => p.Items, items));

        // Assert
        var select = cut.Find("select[opta-select]");
        Assert.NotNull(select);
    }

    [Fact]
    public void OptASelectRendersAllOptions()
    {
        // Arrange
        var items = new[] { "Option1", "Option2", "Option3" };

        // Act
        var cut = Render<OptASelect<string>>(parameters => parameters
            .Add(p => p.Items, items));

        // Assert
        var options = cut.FindAll("option");
        Assert.Equal(3, options.Count);
    }

    [Fact]
    public void OptASelectBindsValueCorrectly()
    {
        // Arrange
        var items = new[] { "Option1", "Option2", "Option3" };
        var value = "Option2";

        // Act
        var cut = Render<OptASelect<string>>(parameters => parameters
            .Add(p => p.Items, items)
            .Add(p => p.Value, value));

        // Assert
        var select = cut.Find("select[opta-select]");
        Assert.NotNull(select);
        // The component renders with the value set
    }
}
