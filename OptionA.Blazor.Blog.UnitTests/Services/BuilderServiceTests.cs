using OptionA.Blazor.Blog.Services;

namespace OptionA.Blazor.Blog.UnitTests.Services;

public class BuilderServiceTests
{
    private readonly IBuilderService _service;

    public BuilderServiceTests()
    {
        _service = new BuilderService();
    }

    [Fact]
    public void ToJson_CreatesValidJson()
    {
        // Arrange
        var post = new Post
        {
            Title = "Test Post",
            Date = new DateTime(2024, 1, 1),
            Subtitle = "Test Subtitle"
        };
        post.Tags.Add("tag1");
        post.Content.Add(new TextContent { Content = "Test content" });

        // Act
        var json = _service.ToJson(post);

        // Assert
        Assert.NotEmpty(json);
        Assert.Contains("Test Post", json);
        Assert.Contains("tag1", json);
    }

    [Fact]
    public void CreateFromJson_ReconstructsPost()
    {
        // Arrange
        var originalPost = new Post
        {
            Title = "Test Post",
            Date = new DateTime(2024, 1, 1),
            Subtitle = "Test Subtitle"
        };
        originalPost.Tags.Add("tag1");
        originalPost.Content.Add(new TextContent { Content = "Test content" });
        var json = _service.ToJson(originalPost);

        // Act
        var reconstructedPost = _service.CreateFromJson(json);

        // Assert
        Assert.Equal(originalPost.Title, reconstructedPost.Title);
        Assert.Equal(originalPost.Date, reconstructedPost.Date);
        Assert.Equal(originalPost.Subtitle, reconstructedPost.Subtitle);
        Assert.Equal(originalPost.Tags.Count, reconstructedPost.Tags.Count);
        Assert.Equal(originalPost.Content.Count, reconstructedPost.Content.Count);
    }

    [Fact]
    public void ToJson_HandlesHeaderContent()
    {
        // Arrange
        var post = new Post
        {
            Title = "Test",
            Date = DateTime.Now
        };
        post.Content.Add(new HeaderContent { Content = "Header", Size = HeaderSize.Two });
        
        // Act
        var json = _service.ToJson(post);
        var reconstructed = _service.CreateFromJson(json);

        // Assert
        Assert.Single(reconstructed.Content);
        var header = reconstructed.Content[0] as HeaderContent;
        Assert.NotNull(header);
        Assert.Equal("Header", header.Content);
        Assert.Equal(HeaderSize.Two, header.Size);
    }

    [Fact]
    public void ToJson_HandlesCodeContent()
    {
        // Arrange
        var post = new Post
        {
            Title = "Test",
            Date = DateTime.Now
        };
        post.Content.Add(new CodeContent { Code = "var x = 1;", Language = CodeLanguage.CSharp });
        
        // Act
        var json = _service.ToJson(post);
        var reconstructed = _service.CreateFromJson(json);

        // Assert
        Assert.Single(reconstructed.Content);
        var code = reconstructed.Content[0] as CodeContent;
        Assert.NotNull(code);
        Assert.Equal("var x = 1;", code.Code);
        Assert.Equal(CodeLanguage.CSharp, code.Language);
    }

    [Fact]
    public void ToJson_HandlesQuoteContent()
    {
        // Arrange
        var post = new Post
        {
            Title = "Test",
            Date = DateTime.Now
        };
        post.Content.Add(new QuoteContent { Quote = "Test quote", Source = "Author" });
        
        // Act
        var json = _service.ToJson(post);
        var reconstructed = _service.CreateFromJson(json);

        // Assert
        Assert.Single(reconstructed.Content);
        var quote = reconstructed.Content[0] as QuoteContent;
        Assert.NotNull(quote);
        Assert.Equal("Test quote", quote.Quote);
        Assert.Equal("Author", quote.Source);
    }

    [Fact]
    public void ToJson_HandlesImageContent()
    {
        // Arrange
        var post = new Post
        {
            Title = "Test",
            Date = DateTime.Now
        };
        post.Content.Add(new ImageContent { Source = "/image.jpg", Alternative = "Alt text", Title = "Image Title" });
        
        // Act
        var json = _service.ToJson(post);
        var reconstructed = _service.CreateFromJson(json);

        // Assert
        Assert.Single(reconstructed.Content);
        var image = reconstructed.Content[0] as ImageContent;
        Assert.NotNull(image);
        Assert.Equal("/image.jpg", image.Source);
        Assert.Equal("Alt text", image.Alternative);
        Assert.Equal("Image Title", image.Title);
    }

    [Fact]
    public void ToJson_HandlesListContent()
    {
        // Arrange
        var post = new Post
        {
            Title = "Test",
            Date = DateTime.Now
        };
        var list = new ListContent { ListType = ListType.OrderedList };
        list.Items.Add("Item 1");
        list.Items.Add("Item 2");
        post.Content.Add(list);
        
        // Act
        var json = _service.ToJson(post);
        var reconstructed = _service.CreateFromJson(json);

        // Assert
        Assert.Single(reconstructed.Content);
        var reconstructedList = reconstructed.Content[0] as ListContent;
        Assert.NotNull(reconstructedList);
        Assert.Equal(ListType.OrderedList, reconstructedList.ListType);
        Assert.Equal(2, reconstructedList.Items.Count);
    }

    [Fact]
    public void ToJson_HandlesTableContent()
    {
        // Arrange
        var post = new Post
        {
            Title = "Test",
            Date = DateTime.Now
        };
        var table = new TableContent();
        table.Headers.Add("Col1");
        table.Rows.Add(new List<string> { "Val1", "Val2" });
        table.Footer.Add("Footer");
        post.Content.Add(table);
        
        // Act
        var json = _service.ToJson(post);
        var reconstructed = _service.CreateFromJson(json);

        // Assert
        Assert.Single(reconstructed.Content);
        var reconstructedTable = reconstructed.Content[0] as TableContent;
        Assert.NotNull(reconstructedTable);
        Assert.Single(reconstructedTable.Headers);
        Assert.Single(reconstructedTable.Rows);
        Assert.Single(reconstructedTable.Footer);
    }
}
