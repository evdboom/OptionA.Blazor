namespace OptionA.Blazor.Components.UnitTests.Input;

public enum TestEnum
{
    Option1,
    Option2,
    Option3
}

public class OptAEnumSelectTests : BunitContext
{
    [Fact]
    public void OptAEnumSelectRendersCorrectly()
    {
        // Arrange & Act
        var cut = Render<OptAEnumSelect<TestEnum>>();

        // Assert
        var select = cut.Find("select[opta-enum-select]");
        Assert.NotNull(select);
    }

    [Fact]
    public void OptAEnumSelectRendersAllEnumOptions()
    {
        // Arrange & Act
        var cut = Render<OptAEnumSelect<TestEnum>>();

        // Assert
        var options = cut.FindAll("option");
        Assert.Equal(3, options.Count);
    }

    [Fact]
    public void OptAEnumSelectBindsValueCorrectly()
    {
        // Arrange
        var value = TestEnum.Option2;

        // Act
        var cut = Render<OptAEnumSelect<TestEnum>>(parameters => parameters
            .Add(p => p.Value, value));

        // Assert
        var select = cut.Find("select[opta-enum-select]");
        Assert.Contains(value.ToString(), select.InnerHtml);
    }
}
