using System.Diagnostics.CodeAnalysis;

namespace OptionA.Blazor.Blog.Builder
{
    /// <summary>
    /// Implementation of the <see cref="IBlogBuilderDataProvider"/>
    /// </summary>
    public class BlogBuilderDataProvider : IBlogBuilderDataProvider
    {
        private readonly OptaBlogBuilderOptions _options;
       
        /// <summary>
        /// Creates the Dataprovider with the given options
        /// </summary>
        /// <param name="configuration"></param>
        public BlogBuilderDataProvider(Action<OptaBlogBuilderOptions>? configuration)
        {
            _options = new();
            configuration?.Invoke(_options);
        }

        /// <inheritdoc/>
        public IContent CreateContentForType(ContentType contentType)
        {
            return contentType switch
            {
                ContentType.Paragraph => new ParagraphContent(),
                ContentType.Header => new HeaderContent
                {
                    Size = _options.DefaultHeaderSize ?? HeaderSize.Two
                },
                ContentType.Code => new CodeContent
                {
                    Language = _options.DefaultCodeLanguage ?? CodeLanguage.CSharp
                },
                ContentType.Image => new ImageContent(),
                ContentType.Quote => new QuoteContent(),
                _ => throw new NotSupportedException($"Contenttype {contentType} is not support as individual blogpart")
            };
        }
        
        /// <inheritdoc/>
        public Dictionary<string, object?> GetAttributes(BuilderType type, Dictionary<string, object?>? defaultAttributes = null)
        {
            var result = defaultAttributes ?? new();

            if (TryGetProperties(type, out var attributes)) 
            {
                if (attributes.AdditionalAttributes is not null)
                {
                    foreach(var attribute in attributes.AdditionalAttributes) 
                    {
                        result[attribute.Key] = attribute.Value;
                    }
                }
                if (attributes.Class is not null)
                {
                    result["class"] = attributes.Class;
                }
            }

            return result;
        }

        /// <inheritdoc/>
        public IContent? GetContent(BuilderType type, string? defaultContent)
        {
            if (TryGetProperties(type, out var properties))
            {
                IContent result = properties.ContentType switch
                {
                    ContentType.Inline => new InlineContent(),
                    ContentType.Block => new BlockContent(),
                    _ => new InlineContent()
                };

                if (result is TextContent text)
                {
                    if (properties.Content is not null)
                    {
                        text.Content = properties.Content;
                    }
                    else if (defaultContent is not null)
                    {
                        text.Content = defaultContent;
                    }
                    else
                    {
                        return null;
                    }
                }

                return result;
            }
           
            if (defaultContent is not null)
            {
                return new InlineContent
                {
                    Content = defaultContent
                };
            }

            return null;
        }
        
        /// <inheritdoc/>
        public bool TryGetProperties(BuilderType type, [NotNullWhen(true)] out BuilderTypeProperties? properties)
        {
            if (_options.PostBuilderOptions is null)
            {
                properties = null;
                return false;
            }

            return _options.PostBuilderOptions.TryGetValue(type, out properties);
        }
    }
}
