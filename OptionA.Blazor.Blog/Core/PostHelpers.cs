using System.Collections.Generic;

namespace OptionA.Blazor.Blog.Core;

/// <summary>
/// Helper methods for constructing <see cref="Post"/> instances from Markdown-sourced content.
/// </summary>
public static class PostHelpers
{
    /// <summary>
    /// Creates a <see cref="Post"/> from optional <paramref name="md"/> metadata and a pre-parsed
    /// list of <paramref name="content"/> items.
    /// </summary>
    public static Post Create(DocumentMetadata? md, IReadOnlyList<IContent> content)
    {
        var post = new Post();
        if (md is not null)
        {
            post.Title = md.Title ?? string.Empty;
            post.Subtitle = md.Subtitle;
            post.Date = md.Date ?? default;
            post.Tags.AddRange(md.Tags);
        }

        post.Content.AddRange(content);
        return post;
    }

    /// <summary>
    /// Strips any front-matter from <paramref name="markdown"/>, parses the body using
    /// <paramref name="parser"/>, and constructs a <see cref="Post"/> from the result.
    /// </summary>
    public static Post FromMetadataAndContent(DocumentMetadata? md, string markdown, IMarkdownDocumentParser parser)
    {
        var (_, body) = DocumentMetadata.ParseFromMarkdown(markdown);
        var content = parser.Parse(body);
        return Create(md, content);
    }
}
