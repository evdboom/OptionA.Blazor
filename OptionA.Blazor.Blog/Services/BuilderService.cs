using System.Runtime.Serialization;
using System.Text.Json;

namespace OptionA.Blazor.Blog.Services
{
    /// <summary>
    /// Service to build posts from json
    /// </summary>
    public class BuilderService : IBuilderService
    {
        /// <summary>
        /// Creates a new post from the given Json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        public Post CreateFromJson(string json)
        {
            var items = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
            if (items == null)
            {
                throw new InvalidCastException("No valid json for post");
            }

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

        private IContent CreateFromJson(Dictionary<string, JsonElement> content)
        {
            if (!content.TryGetValue(nameof(IContent.Type), out var contentType))
            {
                throw new SerializationException("Missing property Type on content");
            }

            var type = JsonSerializer.Deserialize<ContentType>(contentType);

            var result = type switch
            {
                ContentType.Paragraph => new ParagraphContent(),
                ContentType.Text => new TextContent(),
                ContentType.Inline => new InlineContent(),
                ContentType.Quote => new QuoteContent(),
                ContentType.Block => new BlockContent(),
                ContentType.Bold => new BoldContent(),
                ContentType.Italic => new ItalicContent(),
                ContentType.Link => new LinkContent
                {
                    Href = content.TryGetValue(nameof(LinkContent.Href), out var href)
                    ? JsonSerializer.Deserialize<string>(href) ?? string.Empty
                    : string.Empty,
                    Target = content.TryGetValue(nameof(LinkContent.Target), out var target)
                    ? JsonSerializer.Deserialize<string>(target)
                    : null,
                },
                _ => throw new NotSupportedException($"Canoot create postcontent for type {type}")
            };

            if (result is TextContent text)
            {
                text.Content = content.TryGetValue(nameof(TextContent.Content), out var stringContent)
                    ? JsonSerializer.Deserialize<string>(stringContent) ?? string.Empty
                    : string.Empty;
            }

            if (content.TryGetValue(nameof(IContent.AdditionalClasses), out var additional))
            {
                result.AdditionalClasses.AddRange(JsonSerializer.Deserialize<List<string>>(additional) ?? new());
            }
            if (content.TryGetValue(nameof(IContent.RemovedClasses), out var removed))
            {
                result.RemovedClasses.AddRange(JsonSerializer.Deserialize<List<string>>(removed) ?? new());
            }
            if (content.TryGetValue(nameof(IContent.Attributes), out var attributes))
            {
                var values = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(attributes);
                if (values != null)
                {
                    foreach (var value in values)
                    {
                        result.Attributes[value.Key] = value.Value.ValueKind switch
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
            }

            return result;
        }
    }
}
