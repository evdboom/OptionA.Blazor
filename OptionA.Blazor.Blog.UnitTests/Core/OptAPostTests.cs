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

    [Fact]
    public void OptAPostRendersFromParametersWithoutPost()
    {
        // Arrange
        var content = new List<IContent> { new TextContent { Content = "Parameter Body" } };

        // Act
        var cut = Render<OptAPost>(parameters => parameters
            .Add(p => p.Title, "Parameter Title")
            .Add(p => p.Subtitle, "Parameter Subtitle")
            .Add(p => p.Date, new DateTime(2024, 2, 1))
            .Add(p => p.Tags, new[] { "tag-a", "tag-b" })
            .Add(p => p.Content, content));

        // Assert
        Assert.Contains("Parameter Title", cut.Markup);
        Assert.Contains("Parameter Subtitle", cut.Markup);
        Assert.Contains("tag-a", cut.Markup);
        Assert.Contains("tag-b", cut.Markup);
        Assert.Contains("Parameter Body", cut.Markup);
    }

    [Fact]
    public void OptAPostRendersChildContentWhenProvided()
    {
        // Act
        var cut = Render<OptAPost>(parameters => parameters
            .Add(p => p.Title, "My docs")
            .AddChildContent("<div class='custom-content'>Nice header</div>"));

        // Assert
        Assert.Contains("My docs", cut.Markup);
        Assert.Contains("custom-content", cut.Markup);
        Assert.Contains("Nice header", cut.Markup);
    }

    [Fact]
    public void OptAPostAppliesConfiguredClassesAndRendersBody()
    {
        // Arrange
        _blogDataProvider
            .Setup(x => x.PostHeaderSize)
            .Returns(HeaderSize.Two);
        _blogDataProvider
            .Setup(x => x.PostDateDisplay)
            .Returns(DateDisplayType.UsableDate);
        _blogDataProvider
            .Setup(x => x.HeaderTagContainerClass)
            .Returns("post-tags");
        _blogDataProvider
            .Setup(x => x.PostTitleClass)
            .Returns("post-title");
        _blogDataProvider
            .Setup(x => x.PostDateClass)
            .Returns("post-date");
        _blogDataProvider
            .Setup(x => x.PostSubtitleClass)
            .Returns("post-subtitle");
        _blogDataProvider
            .Setup(x => x.TagClass)
            .Returns("post-tag");
        _blogDataProvider
            .Setup(x => x.TagOverviewHref)
            .Returns("/tags");
        _blogDataProvider
            .Setup(x => x.DisplayLineAfterPostHeader)
            .Returns(true);

        var post = new Post
        {
            Title = "Configured Title",
            Subtitle = "Configured Subtitle",
            Date = new DateTime(2024, 2, 3)
        };
        post.Tags.Add("TagOne");
        post.Content.Add(new TextContent { Content = "Configured Body" });

        // Act
        var cut = Render<OptAPost>(parameters => parameters.Add(p => p.Post, post));

        // Assert
        var header = cut.Find("h2");
        Assert.Equal("Configured Title", header.TextContent);
        Assert.Contains("post-title", header.GetAttribute("class"));
        Assert.Contains("20240203", cut.Markup);
        Assert.Contains("post-date", cut.Markup);
        Assert.Contains("Configured Subtitle", cut.Markup);
        Assert.Contains("post-subtitle", cut.Markup);
        Assert.Equal("post-tags", cut.Find("div.post-tags").GetAttribute("class"));
        var tagLink = cut.Find("a");
        Assert.Equal("/tags/tagone", tagLink.GetAttribute("href"));
        Assert.Contains("post-tag", tagLink.GetAttribute("class"));
        Assert.Contains("<hr", cut.Markup);
        Assert.Contains("Configured Body", cut.Markup);
    }
}
