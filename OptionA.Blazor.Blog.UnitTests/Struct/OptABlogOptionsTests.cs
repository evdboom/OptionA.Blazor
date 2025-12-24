namespace OptionA.Blazor.Blog.UnitTests.Struct;

public class OptABlogOptionsTests
{
    [Fact]
    public void OptABlogOptions_CanSetProperties()
    {
        // Arrange
        var options = new OptABlogOptions();

        // Act
        options.PostHeaderSize = HeaderSize.Two;
        options.PostDateDisplay = DateDisplayType.DateTime;
        options.PostTitleClass = "title-class";
        options.PostDateClass = "date-class";
        options.PostSubtitleClass = "subtitle-class";
        options.TagClass = "tag-class";
        options.TagOverviewHref = "/tags";

        // Assert
        Assert.Equal(HeaderSize.Two, options.PostHeaderSize);
        Assert.Equal(DateDisplayType.DateTime, options.PostDateDisplay);
        Assert.Equal("title-class", options.PostTitleClass);
        Assert.Equal("date-class", options.PostDateClass);
        Assert.Equal("subtitle-class", options.PostSubtitleClass);
        Assert.Equal("tag-class", options.TagClass);
        Assert.Equal("/tags", options.TagOverviewHref);
    }

    [Fact]
    public void OptABlogOptions_CanSetClassesForContentType()
    {
        // Arrange
        var options = new OptABlogOptions();
        var classes = new List<string> { "class1", "class2" };

        // Act
        options.DefaultClassesPerType = new Dictionary<ContentType, List<string>>
        {
            [ContentType.Header] = classes
        };
        var result = options.DefaultClassesPerType[ContentType.Header];

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains("class1", result);
        Assert.Contains("class2", result);
    }
}
