using Markdig;
using Markdig.Syntax;
using OptionA.Blazor.Blog.Document.Internal;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Internal Markdig-based implementation of <see cref="IMarkdownDocumentParser"/>.
/// Markdig is an implementation detail and not part of the public API surface.
/// Conversion logic is delegated to focused internal collaborators:
/// <see cref="BlockConverter"/> (block mapping),
/// <see cref="InlineMarkdownSerializer"/> (AST → markdown-marker text), and
/// <see cref="CodeLanguageMapper"/> (fenced-code language mapping).
/// </summary>
internal sealed class MarkdownDocumentParser : IMarkdownDocumentParser
{
    private static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .Build();

    private readonly BlockConverter _blockConverter;

    public MarkdownDocumentParser()
        : this(new BlockConverter(new InlineMarkdownSerializer()))
    {
    }

    internal MarkdownDocumentParser(BlockConverter blockConverter)
    {
        _blockConverter = blockConverter;
    }

    /// <inheritdoc/>
    public IReadOnlyList<IContent> Parse(string? markdown)
    {
        if (string.IsNullOrWhiteSpace(markdown))
        {
            return [];
        }

        var document = Markdown.Parse(markdown, Pipeline);
        return _blockConverter.ConvertBlocks(document).ToList();
    }
}
