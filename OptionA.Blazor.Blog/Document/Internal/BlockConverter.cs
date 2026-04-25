using Markdig.Extensions.Tables;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace OptionA.Blazor.Blog.Document.Internal;

/// <summary>
/// Converts Markdig <see cref="Block"/> nodes into <see cref="IContent"/> objects
/// suitable for rendering with the existing Blog render components.
/// </summary>
internal sealed class BlockConverter
{
    private readonly InlineMarkdownSerializer _serializer;

    internal BlockConverter(InlineMarkdownSerializer serializer)
    {
        _serializer = serializer;
    }

    /// <summary>
    /// Converts all child blocks in the given <paramref name="container"/> to content items,
    /// skipping any block kinds that are not mapped to a content type.
    /// </summary>
    internal IEnumerable<IContent> ConvertBlocks(ContainerBlock container)
    {
        foreach (var block in container)
        {
            var content = ConvertBlock(block);
            if (content is not null)
            {
                yield return content;
            }
        }
    }

    private IContent? ConvertBlock(Block block)
    {
        return block switch
        {
            HeadingBlock heading => ConvertHeading(heading),
            FencedCodeBlock fenced => ConvertFencedCode(fenced),
            CodeBlock code => ConvertCode(code),
            QuoteBlock quote => ConvertQuote(quote),
            ListBlock list => ConvertList(list),
            Table table => ConvertTable(table),
            ParagraphBlock paragraph => ConvertParagraph(paragraph),
            ThematicBreakBlock => null, // <hr/> – not mapped to a content type yet
            _ => null,
        };
    }

    private HeaderContent ConvertHeading(HeadingBlock heading)
    {
        var size = heading.Level switch
        {
            1 => HeaderSize.One,
            2 => HeaderSize.Two,
            3 => HeaderSize.Three,
            4 => HeaderSize.Four,
            5 => HeaderSize.Five,
            _ => HeaderSize.Six,
        };

        return new HeaderContent
        {
            Content = _serializer.Serialize(heading.Inline),
            Size = size,
        };
    }

    private CodeContent ConvertFencedCode(FencedCodeBlock fenced)
    {
        var info = fenced.Info ?? string.Empty;
        var (language, otherLanguage) = CodeLanguageMapper.Map(info);

        return new CodeContent
        {
            Code = _serializer.SerializeLeaf(fenced),
            Language = language,
            OtherLanguage = otherLanguage,
        };
    }

    private CodeContent ConvertCode(CodeBlock code)
    {
        return new CodeContent
        {
            Code = _serializer.SerializeLeaf(code),
            Language = CodeLanguage.Other,
        };
    }

    private QuoteContent ConvertQuote(QuoteBlock quote)
    {
        var lines = new List<string>();
        foreach (var inner in quote)
        {
            if (inner is ParagraphBlock para)
            {
                lines.Add(_serializer.Serialize(para.Inline));
            }
        }

        return new QuoteContent
        {
            Quote = string.Join(" ", lines),
        };
    }

    private ListContent ConvertList(ListBlock list)
    {
        var listType = list.IsOrdered ? ListType.OrderedList : ListType.UnorderedList;
        var items = new List<string>();

        foreach (var item in list.OfType<ListItemBlock>())
        {
            var text = new List<string>();
            foreach (var child in item)
            {
                if (child is ParagraphBlock para)
                {
                    text.Add(_serializer.Serialize(para.Inline));
                }
            }
            items.Add(string.Join(" ", text));
        }

        return new ListContent
        {
            ListType = listType,
            Items = items,
        };
    }

    private TableContent ConvertTable(Table table)
    {
        var headers = new List<string>();
        var rows = new List<IList<string>>();

        foreach (var row in table.OfType<TableRow>())
        {
            var cells = row.OfType<TableCell>()
                .Select(cell => _serializer.SerializeBlockContainer(cell))
                .ToList();

            if (row.IsHeader)
            {
                headers.AddRange(cells);
            }
            else
            {
                rows.Add(cells);
            }
        }

        return new TableContent
        {
            Headers = headers,
            Rows = rows,
        };
    }

    private IContent ConvertParagraph(ParagraphBlock paragraph)
    {
        // A paragraph that contains only a single image is promoted to a block-level ImageContent.
        if (paragraph.Inline is not null)
        {
            var inlines = paragraph.Inline.ToList();
            if (inlines.Count == 1 && inlines[0] is LinkInline { IsImage: true } img)
            {
                var altSb = new System.Text.StringBuilder();
                AppendInlineText(img, altSb);
                return new ImageContent
                {
                    Source = img.Url ?? string.Empty,
                    Alternative = altSb.ToString(),
                    Title = img.Title,
                };
            }
        }

        return new ParagraphContent
        {
            Content = _serializer.Serialize(paragraph.Inline),
        };
    }

    // Minimal recursive helper used only for extracting alt text from a block-level image.
    private static void AppendInlineText(ContainerInline container, System.Text.StringBuilder sb)
    {
        foreach (var inline in container)
        {
            if (inline is LiteralInline literal)
            {
                sb.Append(literal.Content.ToString());
            }
            else if (inline is ContainerInline nested)
            {
                AppendInlineText(nested, sb);
            }
        }
    }
}
