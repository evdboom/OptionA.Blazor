using OptionA.Blazor.Blog.Core.Extensions;
using System.Runtime.Serialization;
using System.Text.Json;

namespace OptionA.Blazor.Blog.Services;

/// <summary>
/// Service to build posts from json
/// </summary>
public class BuilderService : IBuilderService
{
    /// <inheritdoc/>
    public string ToJson(Post post, JsonSerializerOptions? options = null)
    {
        var items = new Dictionary<string, object>();
        if (post.Tags.Count > 0)
        {
            items[nameof(post.Tags)] = post.Tags;
        }
        items[nameof(post.Date)] = post.Date;
        items[nameof(post.Title)] = post.Title;
        if (!string.IsNullOrEmpty(post.Subtitle))
        {
            items[nameof(post.Subtitle)] = post.Subtitle;
        }
        items[nameof(post.Content)] = post.Content.Select(ToJsonDictionary);

        return JsonSerializer.Serialize(items, options);
    }

    /// <inheritdoc/>
    public Post CreateFromJson(string json)
    {
        var items = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json) 
            ?? throw new InvalidCastException("No valid json for post");

        var post = new Post();
        if (items.TryGetValue(nameof(Post.Tags), out var tags))
        {
            post.Tags.AddRange(JsonSerializer.Deserialize<string[]>(tags)!);
        }
        if (items.TryGetValue(nameof(Post.Date), out var date))
        {
            post.Date = JsonSerializer.Deserialize<DateTime>(date);
        }
        if (items.TryGetValue(nameof(Post.Title), out var title))
        {
            post.Title = JsonSerializer.Deserialize<string>(title) ?? string.Empty;
        }
        if (items.TryGetValue(nameof(Post.Subtitle), out var subtitle))
        {
            post.Subtitle = JsonSerializer.Deserialize<string>(subtitle);
        }
        if (items.TryGetValue(nameof(Post.Content), out var content))
        {
            var contentList = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(content);
            foreach (var contentItem in contentList!)
            {
                post.Content.Add(CreateFromJson(contentItem));
            }
        }


        return post;
    }

    private Dictionary<string, object> ToJsonDictionary(IContent content)
    {
        var result = new Dictionary<string, object>();
        if (content.AdditionalClasses.Any())
        {
            result[nameof(content.AdditionalClasses)] = content.AdditionalClasses;
        }
        if (content.RemovedClasses.Any())
        {
            result[nameof(content.RemovedClasses)] = content.RemovedClasses;
        }
        if (content.Attributes.Any())
        {
            result[nameof(content)] = content.Attributes;
        }
        result[nameof(content.Type)] = content.Type;

        switch (content.Type)
        {
            case ContentType.Paragraph:
            case ContentType.Text:
            case ContentType.Inline:
            case ContentType.Block:
            case ContentType.Bold:
            case ContentType.Italic:
                result[nameof(TextContent.Content)] = ((TextContent)content).Content;
                break;
            case ContentType.Link:
                var link = (LinkContent)content;
                result[nameof(link.Content)] = link.Content;
                if (!string.IsNullOrEmpty(link.Target))
                {
                    result[nameof(link.Target)] = link.Target;
                }
                if (!string.IsNullOrEmpty(link.Href))
                {
                    result[nameof(link.Href)] = link.Href;
                }
                break;
            case ContentType.Header:
                var header = (HeaderContent)content;
                if (!string.IsNullOrEmpty(header.Content))
                {
                    result[nameof(header.Content)] = header.Content;
                }
                result[nameof(header.Size)] = header.Size;
                break;
            case ContentType.Code:
                var code = (CodeContent)content;
                if (!string.IsNullOrEmpty(code.Code))
                {
                    result[nameof(code.Code)] = code.Code;
                }
                result[nameof(code.Language)] = code.Language;
                break;
            case ContentType.Icon:
                var icon = (IconContent)content;
                if (icon.Paths.Any())
                {
                    result[nameof(icon.Paths)] = icon.Paths;
                }
                if (!string.IsNullOrEmpty(icon.Width))
                {
                    result[nameof(icon.Width)] = icon.Width;
                }
                if (!string.IsNullOrEmpty(icon.Height))
                {
                    result[nameof(icon.Height)] = icon.Height;
                }
                if (icon.ViewBoxValues.Any(value => value != default))
                {
                    result[nameof(icon.ViewBoxValues)] = icon.ViewBoxValues;
                }
                result[nameof(icon.Mode)] = icon.Mode;
                break;
            case ContentType.Quote:
                var quote = (QuoteContent)content;
                if (quote.AdditionalSourceClasses.Count > 0)
                {
                    result[nameof(quote.AdditionalSourceClasses)] = quote.AdditionalSourceClasses;
                }
                if (quote.RemovedSourceClasses.Count > 0)
                {
                    result[nameof(quote.RemovedSourceClasses)] = quote.RemovedSourceClasses;
                }
                if (quote.SourceAttributes.Count > 0)
                {
                    result[nameof(quote.SourceAttributes)] = quote.SourceAttributes;
                }
                if (!string.IsNullOrEmpty(quote.Quote))
                {
                    result[nameof(quote.Quote)] = quote.Quote;
                }
                if (!string.IsNullOrEmpty(quote.Source))
                {
                    result[nameof(quote.Source)] = quote.Source;
                }
                break;
            case ContentType.Image:
                var image = (ImageContent)content;
                result[nameof(image.Source)] = image.Source;
                if (!string.IsNullOrEmpty(image.Title))
                {
                    result[nameof(image.Title)] = image.Title;
                }
                if (!string.IsNullOrEmpty(image.Alternative))
                {
                    result[nameof(image.Alternative)] = image.Alternative;
                }
                break;
            case ContentType.Frame:
                var frame = (FrameContent)content;
                result[nameof(frame.Source)] = frame.Source;
                if (!string.IsNullOrEmpty(frame.Title))
                {
                    result[nameof(frame.Title)] = frame.Title;
                }
                if (!string.IsNullOrEmpty(frame.Height))
                {
                    result[nameof(frame.Height)] = frame.Height;
                }
                if (!string.IsNullOrEmpty(frame.Width))
                {
                    result[nameof(frame.Width)] = frame.Width;
                }
                break;
            case ContentType.List:
                var list = (ListContent)content;
                result[nameof(list.ListType)] = list.ListType;
                if (list.Items.Any())
                {
                    result[nameof(list.Items)] = list.Items;
                }
                break;
            case ContentType.Table:
                var table = (TableContent)content;
                if (table.Headers.Any())
                {
                    result[nameof(table.Headers)] = table.Headers;
                }
                if (table.Rows.Any())
                {
                    result[nameof(table.Rows)] = table.Rows;
                }
                if (table.Footer.Any())
                {
                    result[nameof(table.Footer)] = table.Footer;
                }
                break;
        }


        return result;
    }

    private static IContent CreateFromJson(Dictionary<string, JsonElement> content)
    {
        if (!content.TryGetValue(nameof(IContent.Type), out var contentType))
        {
            throw new SerializationException("Missing property Type on content");
        }

        var type = JsonSerializer.Deserialize<ContentType>(contentType);

        IContent result = type switch
        {
            ContentType.Paragraph => new ParagraphContent(),
            ContentType.Text => new TextContent(),
            ContentType.Inline => new InlineContent(),
            ContentType.Block => new BlockContent(),
            ContentType.Bold => new BoldContent(),
            ContentType.Italic => new ItalicContent(),
            ContentType.Link => new LinkContent
            {
                Href = content.TryGetValue(nameof(LinkContent.Href), out var href)
                    ? JsonSerializer.Deserialize<string>(href) ?? string.Empty
                    : default,
                Target = content.TryGetValue(nameof(LinkContent.Target), out var target)
                    ? JsonSerializer.Deserialize<string>(target)
                    : default,
            },
            ContentType.Code => new CodeContent
            {
                Code = content.TryGetValue(nameof(CodeContent.Code), out var code)
                    ? JsonSerializer.Deserialize<string>(code) ?? string.Empty
                    : string.Empty,
                Language = content.TryGetValue(nameof(CodeContent.Language), out var language)
                    ? JsonSerializer.Deserialize<CodeLanguage>(language)
                    : CodeLanguage.Other
            },
            ContentType.Header => new HeaderContent
            {
                Content = content.TryGetValue(nameof(HeaderContent.Content), out var stringContent)
                    ? JsonSerializer.Deserialize<string>(stringContent) ?? string.Empty
                    : default,
                Size = content.TryGetValue(nameof(HeaderContent.Size), out var size)
                    ? JsonSerializer.Deserialize<HeaderSize>(size)
                    : HeaderSize.One
            },
            ContentType.Icon => new IconContent
            {
                Paths = content.TryGetValue(nameof(IconContent.Paths), out var paths)
                    ? JsonSerializer.Deserialize<List<string>>(paths) ?? []
                    : [],
                Width = content.TryGetValue(nameof(IconContent.Width), out var width)
                    ? JsonSerializer.Deserialize<string>(width) ?? string.Empty
                    : default,
                Height = content.TryGetValue(nameof(IconContent.Height), out var height)
                    ? JsonSerializer.Deserialize<string>(height) ?? string.Empty
                    : default,
                ViewBoxValues = content.TryGetValue(nameof(IconContent.ViewBoxValues), out var viewbox)
                    ? JsonSerializer.Deserialize<int[]>(viewbox) ?? new int[4]
                    : new int[4],
                Mode = content.TryGetValue(nameof(IconContent.Mode), out var mode)
                    ? JsonSerializer.Deserialize<IconMode>(mode)
                    : IconMode.Class,
            },
            ContentType.Quote => new QuoteContent
            {
                Quote = content.TryGetValue(nameof(QuoteContent.Quote), out var quote)
                    ? JsonSerializer.Deserialize<string>(quote) ?? string.Empty
                    : default,
                Source = content.TryGetValue(nameof(QuoteContent.Source), out var source)
                    ? JsonSerializer.Deserialize<string>(source) ?? string.Empty
                    : default,
                AdditionalSourceClasses = content.TryGetValue(nameof(QuoteContent.AdditionalSourceClasses), out var sourceClasses)
                    ? JsonSerializer.Deserialize<List<string>>(sourceClasses) ?? []
                    : [],
                RemovedSourceClasses = content.TryGetValue(nameof(QuoteContent.RemovedSourceClasses), out var removedClasses)
                    ? JsonSerializer.Deserialize<List<string>>(removedClasses) ?? []
                    : [],
                SourceAttributes = content.TryGetValue(nameof(QuoteContent.SourceAttributes), out var sourceAttributes)
                    ? DeserializeAttributes(sourceAttributes)
                    : [],
            },
            ContentType.Image => new ImageContent
            {
                Source = content.TryGetValue(nameof(ImageContent.Source), out var source)
                    ? JsonSerializer.Deserialize<string>(source) ?? string.Empty
                    : string.Empty,
                Title = content.TryGetValue(nameof(ImageContent.Title), out var title)
                    ? JsonSerializer.Deserialize<string>(title) ?? string.Empty
                    : default,
                Alternative = content.TryGetValue(nameof(ImageContent.Alternative), out var alternative)
                    ? JsonSerializer.Deserialize<string>(alternative) ?? string.Empty
                    : default,
            },
            ContentType.Frame => new FrameContent
            {
                Source = content.TryGetValue(nameof(FrameContent.Source), out var source)
                    ? JsonSerializer.Deserialize<string>(source) ?? string.Empty
                    : string.Empty,
                Title = content.TryGetValue(nameof(FrameContent.Title), out var title)
                    ? JsonSerializer.Deserialize<string>(title) ?? string.Empty
                    : default,
                Width = content.TryGetValue(nameof(FrameContent.Width), out var width)
                    ? JsonSerializer.Deserialize<string>(width)
                    : default,
                Height = content.TryGetValue(nameof(FrameContent.Height), out var height)
                    ? JsonSerializer.Deserialize<string>(height)
                    : default,
            },
            ContentType.List => new ListContent
            {
                ListType = content.TryGetValue(nameof(ListContent.ListType), out var listType)
                    ? JsonSerializer.Deserialize<ListType>(listType)
                    : ListType.UnorderedList,
                Items = content.TryGetValue(nameof(ListContent.Items), out var items)
                    ? JsonSerializer.Deserialize<List<string>>(items) ?? []
                    : [],
            },
            ContentType.Table => new TableContent
            {
                Headers = content.TryGetValue(nameof(TableContent.Headers), out var headers)
                    ? JsonSerializer.Deserialize<List<string>>(headers) ?? []
                    : [],
                Rows = content.TryGetValue(nameof(TableContent.Rows), out var rows)
                    ? JsonSerializer.Deserialize<List<IList<string>>>(rows) ?? []
                    : [],
                Footer = content.TryGetValue(nameof(TableContent.Footer), out var footer)
                    ? JsonSerializer.Deserialize<List<string>>(footer) ?? []
                    : [],
            },
            _ => throw new NotSupportedException($"Cannot create postcontent for type {type}")
        };

        if (result is TextContent text)
        {
            text.Content = content.TryGetValue(nameof(TextContent.Content), out var stringContent)
                ? JsonSerializer.Deserialize<string>(stringContent) ?? string.Empty
                : string.Empty;
        }

        if (content.TryGetValue(nameof(IContent.AdditionalClasses), out var additional))
        {
            result.AdditionalClasses.AddRange(JsonSerializer.Deserialize<List<string>>(additional) ?? []);
        }
        if (content.TryGetValue(nameof(IContent.RemovedClasses), out var removed))
        {
            result.RemovedClasses.AddRange(JsonSerializer.Deserialize<List<string>>(removed) ?? []);
        }
        if (content.TryGetValue(nameof(IContent.Attributes), out var attributes))
        {
            var values = DeserializeAttributes(attributes);
            foreach (var attribute in values)
            {
                result.Attributes[attribute.Key] = attribute.Value;
            }
        }

        return result;
    }

    private static Dictionary<string, object?> DeserializeAttributes(JsonElement attributes)
    {
        var result = new Dictionary<string, object?>();
        var values = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(attributes);
        if (values != null)
        {
            foreach (var value in values)
            {
                result[value.Key] = value.Value.ValueKind switch
                {
                    JsonValueKind.String => value.Value.ToString(),
                    JsonValueKind.Number => int.Parse(value.Value.ToString()),
                    JsonValueKind.True => true,
                    JsonValueKind.False => false,
                    JsonValueKind.Null => null,
                    _ => throw new NotSupportedException("Unsupported valuetype")
                };
            }
        }

        return result;
    }
}
