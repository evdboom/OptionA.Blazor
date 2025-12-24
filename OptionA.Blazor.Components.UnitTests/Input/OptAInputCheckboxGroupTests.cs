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
        var group = cut.Find("div[opta-input-checkbox-group]");
        Assert.NotNull(group);
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
        var checkedBoxes = checkboxes.Where(cb => cb.HasAttribute("checked")).ToList();
        Assert.Equal(2, checkedBoxes.Count);
    }
}
