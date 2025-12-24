using OptionA.Blazor.Blog.Builder;

namespace OptionA.Blazor.Blog.UnitTests.Builder;

public class BlogBuilderDataProviderTests
{
    [Fact]
    public void BlogBuilderDataProvider_CreatesContentForHeader()
    {
        // Arrange
        var provider = new BlogBuilderDataProvider(options => 
        {
            options.DefaultHeaderSize = HeaderSize.Three;
        });

        // Act
        var content = provider.CreateContentForType(ContentType.Header);

        // Assert
        Assert.NotNull(content);
        var header = content as HeaderContent;
        Assert.NotNull(header);
        Assert.Equal(HeaderSize.Three, header.Size);
    }

    [Fact]
    public void BlogBuilderDataProvider_CreatesContentForCode()
    {
        // Arrange
        var provider = new BlogBuilderDataProvider(options => 
        {
            options.DefaultCodeLanguage = CodeLanguage.Html;
        });

        // Act
        var content = provider.CreateContentForType(ContentType.Code);

        // Assert
        Assert.NotNull(content);
        var code = content as CodeContent;
        Assert.NotNull(code);
        Assert.Equal(CodeLanguage.Html, code.Language);
    }

    [Fact]
    public void BlogBuilderDataProvider_CreatesContentForParagraph()
    {
        // Arrange
        var provider = new BlogBuilderDataProvider(null);

        // Act
        var content = provider.CreateContentForType(ContentType.Paragraph);

        // Assert
        Assert.NotNull(content);
        Assert.IsType<ParagraphContent>(content);
    }

    [Fact]
    public void BlogBuilderDataProvider_CreatesContentForImage()
    {
        // Arrange
        var provider = new BlogBuilderDataProvider(null);

        // Act
        var content = provider.CreateContentForType(ContentType.Image);

        // Assert
        Assert.NotNull(content);
        Assert.IsType<ImageContent>(content);
    }

    [Fact]
    public void BlogBuilderDataProvider_CreatesContentForList()
    {
        // Arrange
        var provider = new BlogBuilderDataProvider(null);

        // Act
        var content = provider.CreateContentForType(ContentType.List);

        // Assert
        Assert.NotNull(content);
        Assert.IsType<ListContent>(content);
    }

    [Fact]
    public void BlogBuilderDataProvider_CreatesContentForTable()
    {
        // Arrange
        var provider = new BlogBuilderDataProvider(null);

        // Act
        var content = provider.CreateContentForType(ContentType.Table);

        // Assert
        Assert.NotNull(content);
        Assert.IsType<TableContent>(content);
    }

    [Fact]
    public void BlogBuilderDataProvider_GetAttributes_ReturnsCorrectAttributes()
    {
        // Arrange
        var provider = new BlogBuilderDataProvider(null);

        // Act
        var attributes = provider.GetAttributes(BuilderType.Form);

        // Assert
        Assert.NotNull(attributes);
        Assert.True(attributes.ContainsKey("opta-form"));
        Assert.Equal(true, attributes["opta-form"]);
    }

    [Fact]
    public void BlogBuilderDataProvider_GetContent_WithProperties_ReturnsContent()
    {
        // Arrange
        var provider = new BlogBuilderDataProvider(options =>
        {
            options.PostBuilderOptions = new Dictionary<BuilderType, BuilderTypeProperties>
            {
                [BuilderType.Label] = new BuilderTypeProperties
                {
                    ContentType = ContentType.Inline,
                    Content = "Test Content"
                }
            };
        });

        // Act
        var content = provider.GetContent(BuilderType.Label, null);

        // Assert
        Assert.NotNull(content);
        var inline = content as InlineContent;
        Assert.NotNull(inline);
        Assert.Equal("Test Content", inline.Content);
    }

    [Fact]
    public void BlogBuilderDataProvider_GetContent_WithDefaultContent_ReturnsDefault()
    {
        // Arrange
        var provider = new BlogBuilderDataProvider(null);

        // Act
        var content = provider.GetContent(BuilderType.Label, "Default Text");

        // Assert
        Assert.NotNull(content);
        var inline = content as InlineContent;
        Assert.NotNull(inline);
        Assert.Equal("Default Text", inline.Content);
    }

    [Fact]
    public void BlogBuilderDataProvider_GetContent_WithoutContent_ReturnsNull()
    {
        // Arrange
        var provider = new BlogBuilderDataProvider(null);

        // Act
        var content = provider.GetContent(BuilderType.Label, null);

        // Assert
        Assert.Null(content);
    }

    [Fact]
    public void BlogBuilderDataProvider_TryGetProperties_WithOptions_ReturnsTrue()
    {
        // Arrange
        var props = new BuilderTypeProperties { ContentType = ContentType.Inline };
        var provider = new BlogBuilderDataProvider(options =>
        {
            options.PostBuilderOptions = new Dictionary<BuilderType, BuilderTypeProperties>
            {
                [BuilderType.Label] = props
            };
        });

        // Act
        var result = provider.TryGetProperties(BuilderType.Label, out var properties);

        // Assert
        Assert.True(result);
        Assert.NotNull(properties);
        Assert.Equal(props, properties);
    }

    [Fact]
    public void BlogBuilderDataProvider_TryGetProperties_WithoutOptions_ReturnsFalse()
    {
        // Arrange
        var provider = new BlogBuilderDataProvider(null);

        // Act
        var result = provider.TryGetProperties(BuilderType.Label, out var properties);

        // Assert
        Assert.False(result);
        Assert.Null(properties);
    }
}
