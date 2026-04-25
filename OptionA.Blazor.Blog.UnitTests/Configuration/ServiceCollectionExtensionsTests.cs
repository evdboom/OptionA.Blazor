using Microsoft.Extensions.DependencyInjection;

namespace OptionA.Blazor.Blog.UnitTests.Configuration;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddOptionABlog_SingletonLifetime_RegistersMarkdownDocumentParserAsSingleton()
    {
        var services = new ServiceCollection();

        services.AddOptionABlog(lifetime: ServiceLifetime.Singleton);

        using var provider = services.BuildServiceProvider();
        var first = provider.GetRequiredService<IMarkdownDocumentParser>();
        var second = provider.GetRequiredService<IMarkdownDocumentParser>();

        Assert.IsType<MarkdownDocumentParser>(first);
        Assert.Same(first, second);
    }

    [Fact]
    public void AddOptionABlog_ScopedLifetime_RegistersMarkdownDocumentParserAsScoped()
    {
        var services = new ServiceCollection();

        services.AddOptionABlog(lifetime: ServiceLifetime.Scoped);

        using var provider = services.BuildServiceProvider();
        using var firstScope = provider.CreateScope();
        using var secondScope = provider.CreateScope();
        var first = firstScope.ServiceProvider.GetRequiredService<IMarkdownDocumentParser>();
        var firstAgain = firstScope.ServiceProvider.GetRequiredService<IMarkdownDocumentParser>();
        var second = secondScope.ServiceProvider.GetRequiredService<IMarkdownDocumentParser>();

        Assert.IsType<MarkdownDocumentParser>(first);
        Assert.Same(first, firstAgain);
        Assert.NotSame(first, second);
    }

    [Fact]
    public void AddOptionABootstrapBlog_RegistersMarkdownDocumentParser()
    {
        var services = new ServiceCollection();

        services.AddOptionABootstrapBlog();

        using var provider = services.BuildServiceProvider();
        var parser = provider.GetRequiredService<IMarkdownDocumentParser>();

        Assert.IsType<MarkdownDocumentParser>(parser);
    }

    [Fact]
    public void AddOptionABlog_TransientLifetime_ThrowsNotSupportedException()
    {
        var services = new ServiceCollection();

        var exception = Assert.Throws<NotSupportedException>(() => services.AddOptionABlog(lifetime: ServiceLifetime.Transient));

        Assert.Equal("Only Singleton and Scoped lifetimes are supported", exception.Message);
    }
}
