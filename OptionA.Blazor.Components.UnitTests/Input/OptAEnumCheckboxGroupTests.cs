namespace OptionA.Blazor.Components.UnitTests.Input;

public class OptAEnumCheckboxGroupTests : BunitContext
{
    [Fact]
    public void OptAEnumCheckboxGroupRendersCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptAEnumCheckboxGroup<TestEnum>>();

        // Assert
        var group = cut.Find("div[opta-enum-checkbox-group]");
        Assert.NotNull(group);
    }

    [Fact]
    public void OptAEnumCheckboxGroupRendersAllEnumOptions()
    {
        // Arrange & Act
        var cut = Render<OptAEnumCheckboxGroup<TestEnum>>();

        // Assert
        var checkboxes = cut.FindAll("input[type='checkbox']");
        Assert.Equal(3, checkboxes.Count);
    }

    [Fact]
    public void OptAEnumCheckboxGroupBindsValueCorrectly()
    {
        // Arrange
        var selectedItems = new[] { TestEnum.Option1, TestEnum.Option3 };

        // Act
        var cut = Render<OptAEnumCheckboxGroup<TestEnum>>(parameters => parameters
            .Add(p => p.Value, selectedItems));

        // Assert
        var checkboxes = cut.FindAll("input[type='checkbox']");
        var checkedBoxes = checkboxes.Where(cb => cb.HasAttribute("checked")).ToList();
        Assert.Equal(2, checkedBoxes.Count);
    }
}
