using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Blog.UnitTests.Text;

public class OptATextTests : BunitContext
{
    private readonly Mock<IBlogDataProvider> _blogDataProvider;
    private readonly Mock<IMarkDownParser> _markDownParser;

    public OptATextTests()
    {
        _blogDataProvider = new Mock<IBlogDataProvider>();
        _blogDataProvider
            .Setup(x => x.DefaultClassesForType(It.IsAny<ContentType>()))
            .Returns([]);
        _markDownParser = new Mock<IMarkDownParser>();
        Services.AddSingleton(_blogDataProvider.Object);
        Services.AddSingleton(_markDownParser.Object);
    }

    [Fact]
    public async Task OptATextRendersParagraph()
    {
        // Arrange
        var content = new ParagraphContent { Content = "Test paragraph" };
        _markDownParser
            .Setup(x => x.Parse(It.IsAny<string?>()))
            .Returns(new List<IContent> { new TextContent { Content = "Test paragraph" } });

        // Act
        var cut = Render<OptAText>(parameters => parameters.Add(p => p.Content, content));
        await Task.Delay(100); // Allow for async parsing

        // Assert
        var paragraph = cut.Find("p");
        Assert.NotNull(paragraph);
    }

    [Fact]
    public async Task OptATextRendersBlock()
    {
        // Arrange
        var content = new BlockContent { Content = "Test block" };
        _markDownParser
            .Setup(x => x.Parse(It.IsAny<string?>()))
            .Returns(new List<IContent> { new TextContent { Content = "Test block" } });

        // Act
        var cut = Render<OptAText>(parameters => parameters.Add(p => p.Content, content));
        await Task.Delay(100); // Allow for async parsing

        // Assert
        var div = cut.Find("div");
        Assert.NotNull(div);
    }

    [Fact]
    public async Task OptATextRendersInline()
    {
        // Arrange
        var content = new InlineContent { Content = "Test inline" };
        _markDownParser
            .Setup(x => x.Parse(It.IsAny<string?>()))
            .Returns(new List<IContent> { new TextContent { Content = "Test inline" } });

        // Act
        var cut = Render<OptAText>(parameters => parameters.Add(p => p.Content, content));
        await Task.Delay(100); // Allow for async parsing

        // Assert
        var span = cut.Find("span");
        Assert.NotNull(span);
    }

    [Fact]
    public void OptATextHandlesNullContent()
    {
        // Arrange
        _markDownParser
            .Setup(x => x.Parse(It.IsAny<string?>()))
            .Returns(new List<IContent>());

        // Act
        var cut = Render<OptAText>(parameters => parameters.Add(p => p.Content, (TextContent?)null));

        // Assert
        Assert.NotNull(cut);
    }
}
