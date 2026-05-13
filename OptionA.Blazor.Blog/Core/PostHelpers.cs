using System.Collections.Generic;

namespace OptionA.Blazor.Blog.Core;

public static class PostHelpers
{
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

    public static Post FromMetadataAndContent(DocumentMetadata? md, string markdown, IMarkdownDocumentParser parser)
    {
        var (_, body) = DocumentMetadata.ParseFromMarkdown(markdown);
        var content = parser.Parse(body);
        return Create(md, content);
    }
}
