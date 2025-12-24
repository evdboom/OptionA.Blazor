namespace OptionA.Blazor.Components.UnitTests.Input;

public class OptAEnumRadioGroupTests : BunitContext
{
    [Fact]
    public void OptAEnumRadioGroupRendersCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptAEnumRadioGroup<TestEnum>>();

        // Assert
        var group = cut.Find("div[opta-enum-radio-group]");
        Assert.NotNull(group);
    }

    [Fact]
    public void OptAEnumRadioGroupRendersAllEnumOptions()
    {
        // Arrange & Act
        var cut = Render<OptAEnumRadioGroup<TestEnum>>();

        // Assert
        var radios = cut.FindAll("input[type='radio']");
        Assert.Equal(3, radios.Count);
    }

    [Fact]
    public void OptAEnumRadioGroupBindsValueCorrectly()
    {
        // Arrange
        var value = TestEnum.Option2;

        // Act
        var cut = Render<OptAEnumRadioGroup<TestEnum>>(parameters => parameters
            .Add(p => p.Value, value));

        // Assert
        var radios = cut.FindAll("input[type='radio']");
        var checkedRadio = radios.FirstOrDefault(r => r.HasAttribute("checked"));
        Assert.NotNull(checkedRadio);
    }
}
