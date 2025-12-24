using OptionA.Blazor.Blog.Struct;

namespace OptionA.Blazor.Blog.UnitTests.Struct;

public class BlogDataProviderTests
{
    [Fact]
    public void BlogDataProvider_HasDefaultValues()
    {
        // Act
        var provider = new BlogDataProvider();

        // Assert
        Assert.NotNull(provider);
        Assert.Equal(HeaderSize.One, provider.PostHeaderSize);
        Assert.Equal(DateDisplayType.LongDate, provider.PostDateDisplay);
    }

    [Fact]
    public void BlogDataProvider_DefaultClassesForType_ReturnsEmptyList()
    {
        // Arrange
        var provider = new BlogDataProvider();

        // Act
        var classes = provider.DefaultClassesForType(ContentType.Text);

        // Assert
        Assert.NotNull(classes);
        Assert.Empty(classes);
    }
}
