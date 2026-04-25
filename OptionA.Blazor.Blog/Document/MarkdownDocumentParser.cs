using Markdig;
using Markdig.Syntax;
using OptionA.Blazor.Blog.Document.Internal;
using OptionA.Blazor.Playground;

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
    private readonly IPlaygroundDescriptorResolver? _resolver;

    public MarkdownDocumentParser()
        : this(new BlockConverter(new InlineMarkdownSerializer()), null)
    {
    }

    public MarkdownDocumentParser(IPlaygroundDescriptorResolver? resolver)
        : this(new BlockConverter(new InlineMarkdownSerializer()), resolver)
    {
    }

    public MarkdownDocumentParser(IPlaygroundDescriptorResolver? resolver, IDocumentComponentRegistry? registry)
        : this(new BlockConverter(new InlineMarkdownSerializer(), registry), resolver)
    {
    }

    internal MarkdownDocumentParser(BlockConverter blockConverter)
        : this(blockConverter, null)
    {
    }

    internal MarkdownDocumentParser(BlockConverter blockConverter, IPlaygroundDescriptorResolver? resolver)
    {
        _blockConverter = blockConverter;
        _resolver = resolver;
    }

    /// <inheritdoc/>
    public IReadOnlyList<IContent> Parse(string? markdown)
    {
        if (string.IsNullOrWhiteSpace(markdown))
        {
            return [];
        }

        var preprocessed = PlaygroundDirectivePreprocessor.Process(markdown);
        var document = Markdown.Parse(preprocessed, Pipeline);
        var items = _blockConverter.ConvertBlocks(document).ToList();

        ResolvePlaygroundDirectives(items);

        return items;
    }

    private void ResolvePlaygroundDirectives(List<IContent> items)
    {
        foreach (var item in items)
        {
            if (item is not PlaygroundDirectiveContent directive)
            {
                continue;
            }

            if (directive.DirectiveId is null)
            {
                directive.ErrorMessage = "Playground directive is missing an id attribute.";
                continue;
            }

            if (_resolver is null)
            {
                directive.ErrorMessage =
                    $"No playground resolver is registered. Cannot resolve playground id \"{directive.DirectiveId}\".";
                continue;
            }

            var resolved = _resolver.Resolve(directive.DirectiveId, null);

            if (resolved is null)
            {
                directive.ErrorMessage = $"Unknown playground id: \"{directive.DirectiveId}\".";
            }
            else
            {
                directive.ResolvedDescriptor = directive.ParameterOverrides.Count > 0
                    ? new DirectivePlaygroundDescriptor(resolved, directive.ParameterOverrides)
                    : resolved;
            }
        }
    }
}
