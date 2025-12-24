using OptionA.Blazor.Blog.Builder;

namespace OptionA.Blazor.Blog.UnitTests.Builder;

public class BuilderTypePropertiesTests
{
    [Fact]
    public void BuilderTypeProperties_CanSetContentType()
    {
        // Arrange
        var props = new BuilderTypeProperties();

        // Act
        props.ContentType = ContentType.Inline;

        // Assert
        Assert.Equal(ContentType.Inline, props.ContentType);
    }

    [Fact]
    public void BuilderTypeProperties_CanSetContent()
    {
        // Arrange
        var props = new BuilderTypeProperties();

        // Act
        props.Content = "Test Content";

        // Assert
        Assert.Equal("Test Content", props.Content);
    }

    [Fact]
    public void BuilderTypeProperties_CanSetContentClass()
    {
        // Arrange
        var props = new BuilderTypeProperties();

        // Act
        props.ContentClass = "test-class";

        // Assert
        Assert.Equal("test-class", props.ContentClass);
    }

    [Fact]
    public void BuilderTypeProperties_CanSetClass()
    {
        // Arrange
        var props = new BuilderTypeProperties();

        // Act
        props.Class = "builder-class";

        // Assert
        Assert.Equal("builder-class", props.Class);
    }

    [Fact]
    public void BuilderTypeProperties_CanSetAdditionalAttributes()
    {
        // Arrange
        var props = new BuilderTypeProperties();
        var attributes = new Dictionary<string, object?> { ["data-test"] = "value" };

        // Act
        props.AdditionalAttributes = attributes;

        // Assert
        Assert.Equal(attributes, props.AdditionalAttributes);
        Assert.Equal("value", props.AdditionalAttributes["data-test"]);
    }
}
