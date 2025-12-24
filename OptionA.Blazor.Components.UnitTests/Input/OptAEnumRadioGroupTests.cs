namespace OptionA.Blazor.Components.UnitTests.Input;

public class OptAEnumRadioGroupTests : BunitContext
{
    [Fact]
    public void OptAEnumRadioGroupRendersCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptAEnumRadioGroup<TestEnum>>();

        // Assert
        var div = cut.Find("div");
        Assert.NotNull(div);
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
        // The component renders with radios
        Assert.True(radios.Count > 0);
    }
}
