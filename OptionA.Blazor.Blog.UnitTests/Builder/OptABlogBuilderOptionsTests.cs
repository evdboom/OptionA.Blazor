using OptionA.Blazor.Blog.Builder;

namespace OptionA.Blazor.Blog.UnitTests.Builder;

public class OptABlogBuilderOptionsTests
{
    [Fact]
    public void OptABlogBuilderOptions_CanSetDefaultHeaderSize()
    {
        // Arrange
        var options = new OptABlogBuilderOptions();

        // Act
        options.DefaultHeaderSize = HeaderSize.Four;

        // Assert
        Assert.Equal(HeaderSize.Four, options.DefaultHeaderSize);
    }

    [Fact]
    public void OptABlogBuilderOptions_CanSetDefaultCodeLanguage()
    {
        // Arrange
        var options = new OptABlogBuilderOptions();

        // Act
        options.DefaultCodeLanguage = CodeLanguage.Javascript;

        // Assert
        Assert.Equal(CodeLanguage.Javascript, options.DefaultCodeLanguage);
    }

    [Fact]
    public void OptABlogBuilderOptions_CanSetPostBuilderOptions()
    {
        // Arrange
        var options = new OptABlogBuilderOptions();
        var builderProps = new BuilderTypeProperties
        {
            ContentType = ContentType.Block,
            Content = "Test"
        };

        // Act
        options.PostBuilderOptions = new Dictionary<BuilderType, BuilderTypeProperties>
        {
            [BuilderType.Label] = builderProps
        };

        // Assert
        Assert.NotNull(options.PostBuilderOptions);
        Assert.Single(options.PostBuilderOptions);
        Assert.Equal(builderProps, options.PostBuilderOptions[BuilderType.Label]);
    }

    [Fact]
    public void OptABlogBuilderOptions_CanSetComponentButtonOptions()
    {
        // Arrange
        var options = new OptABlogBuilderOptions();
        var buttonProps = new BuilderTypeProperties
        {
            ContentType = ContentType.Icon,
            Content = "icon-class"
        };

        // Act
        options.ComponentButtonOptions = new Dictionary<ContentType, BuilderTypeProperties>
        {
            [ContentType.Header] = buttonProps
        };

        // Assert
        Assert.NotNull(options.ComponentButtonOptions);
        Assert.Single(options.ComponentButtonOptions);
        Assert.Equal(buttonProps, options.ComponentButtonOptions[ContentType.Header]);
    }
}
