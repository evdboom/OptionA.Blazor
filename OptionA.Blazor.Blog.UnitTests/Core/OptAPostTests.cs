using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Blog.UnitTests.Core;

public class OptAPostTests : BunitContext
{
    private readonly Mock<IBlogDataProvider> _blogDataProvider;
    private readonly Mock<IMarkDownParser> _markDownParser;

    public OptAPostTests()
    {
        _blogDataProvider = new Mock<IBlogDataProvider>();
        _blogDataProvider
            .Setup(x => x.DefaultClassesForType(It.IsAny<ContentType>()))
            .Returns([]);
        _blogDataProvider
            .Setup(x => x.PostHeaderSize)
            .Returns(HeaderSize.One);
        _blogDataProvider
            .Setup(x => x.PostDateDisplay)
            .Returns(DateDisplayType.Date);
        _markDownParser = new Mock<IMarkDownParser>();
        _markDownParser
            .Setup(x => x.Parse(It.IsAny<string?>()))
            .Returns((string? text) => string.IsNullOrEmpty(text) 
                ? new List<IContent>() 
                : new List<IContent> { new TextContent { Content = text } });
        Services.AddSingleton(_blogDataProvider.Object);
        Services.AddSingleton(_markDownParser.Object);
    }

    [Fact]
    public void OptAPostRendersWithTitle()
    {
        // Arrange
        var post = new Post
        {
            Title = "Test Title",
            Date = new DateTime(2024, 1, 1)
        };

        // Act
        var cut = Render<OptAPost>(parameters => parameters.Add(p => p.Post, post));

        // Assert
        var header = cut.Find("h1");
        Assert.Contains("Test Title", header.TextContent);
    }

    [Fact]
    public void OptAPostRendersWithTags()
    {
        // Arrange
        var post = new Post
        {
            Title = "Test Title",
            Date = new DateTime(2024, 1, 1)
        };
        post.Tags.Add("tag1");
        post.Tags.Add("tag2");

        // Act
        var cut = Render<OptAPost>(parameters => parameters.Add(p => p.Post, post));

        // Assert
        Assert.Contains("tag1", cut.Markup);
        Assert.Contains("tag2", cut.Markup);
    }

    [Fact]
    public void OptAPostRendersWithSubtitle()
    {
        // Arrange
        var post = new Post
        {
            Title = "Test Title",
            Subtitle = "Test Subtitle",
            Date = new DateTime(2024, 1, 1)
        };

        // Act
        var cut = Render<OptAPost>(parameters => parameters.Add(p => p.Post, post));

        // Assert
        Assert.Contains("Test Subtitle", cut.Markup);
    }

    [Fact]
    public void OptAPostRendersWithTagsAsLinks()
    {
        // Arrange
        _blogDataProvider
            .Setup(x => x.TagOverviewHref)
            .Returns("/tags");
        var post = new Post
        {
            Title = "Test Title",
            Date = new DateTime(2024, 1, 1)
        };
        post.Tags.Add("TestTag");

        // Act
        var cut = Render<OptAPost>(parameters => parameters.Add(p => p.Post, post));

        // Assert
        Assert.Contains("/tags/testtag", cut.Markup.ToLower());
    }

    [Fact]
    public void OptAPostRendersNullPostCorrectly()
    {
        // Act
        var cut = Render<OptAPost>(parameters => parameters.Add(p => p.Post, (Post?)null));

        // Assert
        Assert.NotNull(cut);
    }
}
