namespace OptionA.Blazor.Blog.UnitTests.Core;

public class ContentClassesTests
{
    [Fact]
    public void TextContent_HasCorrectType()
    {
        // Arrange & Act
        var content = new TextContent();

        // Assert
        Assert.Equal(ContentType.Text, content.Type);
    }

    [Fact]
    public void ParagraphContent_HasCorrectType()
    {
        // Arrange & Act
        var content = new ParagraphContent();

        // Assert
        Assert.Equal(ContentType.Paragraph, content.Type);
    }

    [Fact]
    public void BlockContent_HasCorrectType()
    {
        // Arrange & Act
        var content = new BlockContent();

        // Assert
        Assert.Equal(ContentType.Block, content.Type);
    }

    [Fact]
    public void InlineContent_HasCorrectType()
    {
        // Arrange & Act
        var content = new InlineContent();

        // Assert
        Assert.Equal(ContentType.Inline, content.Type);
    }

    [Fact]
    public void BoldContent_HasCorrectType()
    {
        // Arrange & Act
        var content = new BoldContent();

        // Assert
        Assert.Equal(ContentType.Bold, content.Type);
    }

    [Fact]
    public void ItalicContent_HasCorrectType()
    {
        // Arrange & Act
        var content = new ItalicContent();

        // Assert
        Assert.Equal(ContentType.Italic, content.Type);
    }

    [Fact]
    public void LinkContent_HasCorrectType()
    {
        // Arrange & Act
        var content = new LinkContent();

        // Assert
        Assert.Equal(ContentType.Link, content.Type);
    }

    [Fact]
    public void HeaderContent_HasCorrectType()
    {
        // Arrange & Act
        var content = new HeaderContent();

        // Assert
        Assert.Equal(ContentType.Header, content.Type);
    }

    [Fact]
    public void CodeContent_HasCorrectType()
    {
        // Arrange & Act
        var content = new CodeContent();

        // Assert
        Assert.Equal(ContentType.Code, content.Type);
    }

    [Fact]
    public void QuoteContent_HasCorrectType()
    {
        // Arrange & Act
        var content = new QuoteContent();

        // Assert
        Assert.Equal(ContentType.Quote, content.Type);
    }

    [Fact]
    public void ImageContent_HasCorrectType()
    {
        // Arrange & Act
        var content = new ImageContent();

        // Assert
        Assert.Equal(ContentType.Image, content.Type);
    }

    [Fact]
    public void FrameContent_HasCorrectType()
    {
        // Arrange & Act
        var content = new FrameContent();

        // Assert
        Assert.Equal(ContentType.Frame, content.Type);
    }

    [Fact]
    public void ListContent_HasCorrectType()
    {
        // Arrange & Act
        var content = new ListContent();

        // Assert
        Assert.Equal(ContentType.List, content.Type);
    }

    [Fact]
    public void TableContent_HasCorrectType()
    {
        // Arrange & Act
        var content = new TableContent();

        // Assert
        Assert.Equal(ContentType.Table, content.Type);
    }

    [Fact]
    public void IconContent_HasCorrectType()
    {
        // Arrange & Act
        var content = new IconContent();

        // Assert
        Assert.Equal(ContentType.Icon, content.Type);
    }

    [Fact]
    public void Content_CanAddAdditionalClasses()
    {
        // Arrange
        var content = new TextContent();

        // Act
        content.AdditionalClasses.Add("test-class");
        content.AdditionalClasses.Add("another-class");

        // Assert
        Assert.Equal(2, content.AdditionalClasses.Count);
        Assert.Contains("test-class", content.AdditionalClasses);
        Assert.Contains("another-class", content.AdditionalClasses);
    }

    [Fact]
    public void Content_CanAddRemovedClasses()
    {
        // Arrange
        var content = new TextContent();

        // Act
        content.RemovedClasses.Add("remove-class");

        // Assert
        Assert.Single(content.RemovedClasses);
        Assert.Contains("remove-class", content.RemovedClasses);
    }

    [Fact]
    public void Content_CanAddAttributes()
    {
        // Arrange
        var content = new TextContent();

        // Act
        content.Attributes["data-test"] = "test-value";
        content.Attributes["aria-label"] = "Test Label";

        // Assert
        Assert.Equal(2, content.Attributes.Count);
        Assert.Equal("test-value", content.Attributes["data-test"]);
        Assert.Equal("Test Label", content.Attributes["aria-label"]);
    }

    [Fact]
    public void TextContent_IsInvalidWhenEmpty()
    {
        // Arrange
        var content = new TextContent { Content = "" };

        // Assert
        Assert.True(content.IsInvalid);
    }

    [Fact]
    public void TextContent_IsValidWhenHasContent()
    {
        // Arrange
        var content = new TextContent { Content = "Test" };

        // Assert
        Assert.False(content.IsInvalid);
    }

    [Fact]
    public void ImageContent_IsInvalidWhenSourceEmpty()
    {
        // Arrange
        var content = new ImageContent { Source = "" };

        // Assert
        Assert.True(content.IsInvalid);
    }

    [Fact]
    public void ImageContent_IsValidWhenHasSource()
    {
        // Arrange
        var content = new ImageContent { Source = "/image.jpg" };

        // Assert
        Assert.False(content.IsInvalid);
    }

    [Fact]
    public void Post_CanAddTags()
    {
        // Arrange
        var post = new Post();

        // Act
        post.Tags.Add("tag1");
        post.Tags.Add("tag2");

        // Assert
        Assert.Equal(2, post.Tags.Count);
        Assert.Contains("tag1", post.Tags);
        Assert.Contains("tag2", post.Tags);
    }

    [Fact]
    public void Post_CanAddContent()
    {
        // Arrange
        var post = new Post();

        // Act
        post.Content.Add(new TextContent { Content = "Test" });
        post.Content.Add(new HeaderContent { Content = "Header", Size = HeaderSize.One });

        // Assert
        Assert.Equal(2, post.Content.Count);
    }
}
