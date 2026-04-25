using OptionA.Blazor.Blog.Builder;
using OptionA.Blazor.Blog.Builder.Parts;

namespace OptionA.Blazor.Blog.UnitTests.Builder;

public class BuilderDeprecationTests
{
    private const string ExpectedMessage = "Blog.Builder WYSIWYG editors are deprecated. Use OptionA.Blazor.Blog (OptADocument) with Markdown authoring and OptionA.Blazor.Playground for interactive previews. This package will not receive new features.";

    public static TheoryData<Type> DeprecatedBuilderTypes => new()
    {
        typeof(OptAPostBuilder),
        typeof(OptACodeBuilder),
        typeof(OptAFrameBuilder),
        typeof(OptAHeaderBuilder),
        typeof(OptAImageBuilder),
        typeof(OptAListBuilder),
        typeof(OptAParagraphBuilder),
        typeof(OptAQuoteBuilder),
        typeof(OptATableBuilder),
    };

    [Theory]
    [MemberData(nameof(DeprecatedBuilderTypes))]
    public void BuilderType_ObsoleteMessage_MatchesApprovedReplacementGuidance(Type builderType)
    {
        var attribute = Assert.Single(builderType.GetCustomAttributes(typeof(ObsoleteAttribute), inherit: false));
        var obsoleteAttribute = Assert.IsType<ObsoleteAttribute>(attribute);

        Assert.Equal(ExpectedMessage, obsoleteAttribute.Message);
        Assert.False(obsoleteAttribute.IsError);
    }
}
