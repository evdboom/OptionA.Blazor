using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System.Net;

namespace OptionA.Blazor.Blog.Document.Internal;

/// <summary>
/// Re-serializes Markdig AST inline nodes back to a Markdown string so the existing
/// <see cref="IMarkDownParser"/> can process bold, italic, links, etc.
/// </summary>
/// <remarks>
/// <para>
/// <strong>Inline link/image titles are intentionally not preserved.</strong>
/// The downstream <see cref="IMarkDownParser"/> (specifically <c>LinkMarker</c>) parses
/// <c>[text](url)</c> syntax only. If a title attribute were included in the serialised
/// form <c>[text](url "title")</c>, the raw title string would be appended to the href
/// and produce an invalid URL. Authors who need a title on a standalone image should use
/// the block-level image syntax (<c>![alt](url "title")</c> on its own paragraph line),
/// which is handled separately and does preserve the title via <see cref="ImageContent"/>.
/// </para>
/// </remarks>
internal sealed class InlineMarkdownSerializer
{
    /// <summary>
    /// Serializes all children of <paramref name="container"/> to a Markdown-compatible string.
    /// Returns <see cref="string.Empty"/> when <paramref name="container"/> is <see langword="null"/>.
    /// </summary>
    internal string Serialize(ContainerInline? container)
    {
        if (container is null)
        {
            return string.Empty;
        }

        var sb = new System.Text.StringBuilder();
        SerializeCore(container, sb);
        return sb.ToString().Trim();
    }

    /// <summary>
    /// Extracts the raw text lines from a <see cref="LeafBlock"/> (e.g. a code block).
    /// Returns <see cref="string.Empty"/> when the block has no lines.
    /// </summary>
    internal string SerializeLeaf(LeafBlock leaf)
    {
        if (leaf.Lines.Count == 0)
        {
            return string.Empty;
        }

        var sb = new System.Text.StringBuilder();
        for (var i = 0; i < leaf.Lines.Count; i++)
        {
            if (i > 0)
            {
                sb.AppendLine();
            }
            sb.Append(leaf.Lines.Lines[i].Slice.ToString());
        }

        return sb.ToString();
    }

    /// <summary>
    /// Extracts and joins the paragraph text from all immediate <see cref="ParagraphBlock"/>
    /// children of a <see cref="ContainerBlock"/> (e.g. a table cell).
    /// </summary>
    internal string SerializeBlockContainer(ContainerBlock container)
    {
        var texts = new List<string>();
        foreach (var block in container)
        {
            if (block is ParagraphBlock para)
            {
                texts.Add(Serialize(para.Inline));
            }
        }
        return string.Join(" ", texts);
    }

    private void SerializeCore(ContainerInline container, System.Text.StringBuilder sb)
    {
        foreach (var inline in container)
        {
            switch (inline)
            {
                case LiteralInline literal:
                    sb.Append(literal.Content.ToString());
                    break;
                case EmphasisInline emphasis:
                    // Re-emit the markdown marker so the existing MarkDownParser handles bold/italic.
                    var marker = emphasis.DelimiterChar == '*' || emphasis.DelimiterChar == '_'
                        ? new string(emphasis.DelimiterChar, emphasis.DelimiterCount)
                        : string.Empty;
                    sb.Append(marker);
                    SerializeCore(emphasis, sb);
                    sb.Append(marker);
                    break;
                case LinkInline link when !link.IsImage:
                    // Title is intentionally dropped — see class-level remarks.
                    sb.Append('[');
                    SerializeCore(link, sb);
                    sb.Append("](");
                    sb.Append(link.Url);
                    sb.Append(')');
                    break;
                case LinkInline img when img.IsImage:
                    // Title is intentionally dropped — see class-level remarks.
                    sb.Append("![");
                    SerializeCore(img, sb);
                    sb.Append("](");
                    sb.Append(img.Url);
                    sb.Append(')');
                    break;
                case CodeInline code:
                    sb.Append('`');
                    sb.Append(code.Content);
                    sb.Append('`');
                    break;
                case LineBreakInline:
                    sb.Append('\n');
                    break;
                case HtmlInline html:
                    // Preserve inline HTML by HTML-encoding it so the HTML remains visible in the rendered output.
                    sb.Append(WebUtility.HtmlEncode(html.Tag));
                    break;
                case ContainerInline nested:
                    SerializeCore(nested, sb);
                    break;
            }
        }
    }
}
