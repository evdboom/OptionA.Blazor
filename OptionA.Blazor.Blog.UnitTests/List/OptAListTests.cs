using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Blog.UnitTests.List;

public class OptAListTests : BunitContext
{
    private readonly Mock<IBlogDataProvider> _blogDataProvider;
    private readonly Mock<IMarkDownParser> _markDownParser;

    public OptAListTests()
    {
        _blogDataProvider = new Mock<IBlogDataProvider>();
        _blogDataProvider
            .Setup(x => x.DefaultClassesForType(It.IsAny<ContentType>()))
            .Returns([]);
        _markDownParser = new Mock<IMarkDownParser>();
        _markDownParser
            .Setup(x => x.Parse(It.IsAny<string?>()))
            .Returns(new List<IContent>());
        Services.AddSingleton(_blogDataProvider.Object);
        Services.AddSingleton(_markDownParser.Object);
    }

    [Fact]
    public void OptAListRendersUnorderedList()
    {
        // Arrange
        var content = new ListContent
        {
            ListType = ListType.UnorderedList
        };
        content.Items.Add("Item 1");
        content.Items.Add("Item 2");

        // Act
        var cut = Render<OptAList>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var ul = cut.Find("ul");
        Assert.NotNull(ul);
        var items = cut.FindAll("li");
        Assert.Equal(2, items.Count);
    }

    [Fact]
    public void OptAListRendersOrderedList()
    {
        // Arrange
        var content = new ListContent
        {
            ListType = ListType.OrderedList
        };
        content.Items.Add("First");
        content.Items.Add("Second");

        // Act
        var cut = Render<OptAList>(parameters => parameters.Add(p => p.Content, content));

        // Assert
        var ol = cut.Find("ol");
        Assert.NotNull(ol);
        var items = cut.FindAll("li");
        Assert.Equal(2, items.Count);
    }

    [Fact]
    public void OptAListHandlesNullContent()
    {
        // Act
        var cut = Render<OptAList>(parameters => parameters.Add(p => p.Content, (ListContent?)null));

        // Assert
        Assert.NotNull(cut);
    }
}
