using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace OptionA.Blazor.Blog.UnitTests.Parts;

public class OptACodeTests : BunitContext
{
    private readonly Mock<IBlogDataProvider> _blogDataProvider;
    private readonly Mock<ICodeParser> _parser;
    private readonly Mock<IMarkDownParser> _markDownParser;

    public OptACodeTests()
    {
        _blogDataProvider = new Mock<IBlogDataProvider>();
        _blogDataProvider
            .Setup(x => x.DefaultClassesForType(It.IsAny<ContentType>()))
            .Returns([]);
        _parser = new Mock<ICodeParser>();
        _markDownParser = new Mock<IMarkDownParser>();
        Services.AddSingleton(_blogDataProvider.Object);
        Services.AddSingleton(_parser.Object);
        Services.AddSingleton(_markDownParser.Object);

    }

    [Fact]
    public void OptACodeRendersCorrectly()
    {
        // Arrange
        _parser
            .Setup(x => x.Language)
            .Returns(CodeLanguage.CSharp);
        _parser
            .Setup(x => x.Parse(It.IsAny<string>(), It.IsAny<string>()))
            .Returns([new TextContent { Content = "public class Test" }]);
        var content = new CodeContent
        {
            Code = "public class Test",
            Language = CodeLanguage.CSharp,
        };

        var x = content.AdditionalClasses.Count;
        var y = content.RemovedClasses.Count;

        var cut = Render<OptACode>(parameters => parameters
            .Add(p => p.Content, content));
        // Assert
        cut.Find("pre").MarkupMatches("<pre><code>public class Test</code></pre>");
    }
}