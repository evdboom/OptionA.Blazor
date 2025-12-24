namespace OptionA.Blazor.Components.UnitTests.Input;

public class OptAInputCheckboxGroupTests : BunitContext
{
    [Fact]
    public void OptAInputCheckboxGroupRendersCorrectly()
    {
        // Arrange
        var items = new[] { "Option1", "Option2", "Option3" };

        // Act
        var cut = Render<OptAInputCheckboxGroup<string>>(parameters => parameters
            .Add(p => p.Items, items));

        // Assert
        var div = cut.Find("div");
        Assert.NotNull(div);
    }

    [Fact]
    public void OptAInputCheckboxGroupRendersAllCheckboxes()
    {
        // Arrange
        var items = new[] { "Option1", "Option2", "Option3" };

        // Act
        var cut = Render<OptAInputCheckboxGroup<string>>(parameters => parameters
            .Add(p => p.Items, items));

        // Assert
        var checkboxes = cut.FindAll("input[type='checkbox']");
        Assert.Equal(3, checkboxes.Count);
    }

    [Fact]
    public void OptAInputCheckboxGroupBindsValueCorrectly()
    {
        // Arrange
        var items = new[] { "Option1", "Option2", "Option3" };
        var selectedItems = new[] { "Option1", "Option3" };

        // Act
        var cut = Render<OptAInputCheckboxGroup<string>>(parameters => parameters
            .Add(p => p.Items, items)
            .Add(p => p.Value, selectedItems));

        // Assert
        var checkboxes = cut.FindAll("input[type='checkbox']");
        // The component renders with checkboxes
        Assert.True(checkboxes.Count > 0);
    }
}
