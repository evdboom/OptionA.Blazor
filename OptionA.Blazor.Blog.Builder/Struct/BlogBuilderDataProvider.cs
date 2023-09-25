using System.Diagnostics.CodeAnalysis;

namespace OptionA.Blazor.Blog.Builder
{
    /// <summary>
    /// Implementation of the <see cref="IBlogBuilderDataProvider"/>
    /// </summary>
    public class BlogBuilderDataProvider : IBlogBuilderDataProvider
    {
        private readonly OptaBlogBuilderOptions _options;
        private readonly Dictionary<AttributeTypes, Func<BuilderTypeProperties, (string AttributeName, string? Value)>> _attributes = new()
        {
            [AttributeTypes.Class] = (properties) => ("class", properties.Class),
            [AttributeTypes.ContainerClass] = (properties) => ("class", properties.ContainerClass),
            [AttributeTypes.LabelClass] = (properties) => ("class", properties.LabelClass),
            [AttributeTypes.Placeholder] = (properties) => ("placeholder", properties.Placeholder),
            [AttributeTypes.Title] = (properties) => ("title", properties.Title),
            [AttributeTypes.GroupClass] = (properties) => ("class", properties.GroupClass),
            [AttributeTypes.InnerGroupClass] = (properties) => ("class", properties.InnerGroupClass),
            [AttributeTypes.ExtraPropertiesClass] = (properties) => ("class", properties.ExtraPropertiesClass),
        };

        private readonly Dictionary<AttributeTypes, Func<BuilderTypeProperties, (string AttributeName, string? Value)>> _addButtonAttributes = new()
        {
            [AttributeTypes.Class] = (properties) => ("class", properties.AddButton?.Class),
            [AttributeTypes.ContainerClass] = (properties) => ("class", properties.AddButton?.ContainerClass),
            [AttributeTypes.Title] = (properties) => ("title", properties.AddButton?.Title),
        };

        private readonly Dictionary<AttributeTypes, Func<BuilderTypeProperties, (string AttributeName, string? Value)>> _removeButtonAttributes = new()
        {
            [AttributeTypes.Class] = (properties) => ("class", properties.RemoveButton?.Class),
            [AttributeTypes.ContainerClass] = (properties) => ("class", properties.RemoveButton?.ContainerClass),
            [AttributeTypes.Title] = (properties) => ("title", properties.RemoveButton?.Title),
        };

        private readonly Dictionary<AttributeTypes, Func<BuilderTypeProperties, string?>> _content = new()
        {
            [AttributeTypes.Content] = (properties) => properties.Content,
            [AttributeTypes.Label] = (properties) => properties.Label
        };

        private readonly Dictionary<AttributeTypes, Func<BuilderTypeProperties, string?>> _addButtonContent = new()
        {
            [AttributeTypes.Content] = (properties) => properties.AddButton?.Content,
        };

        private readonly Dictionary<AttributeTypes, Func<BuilderTypeProperties, string?>> _removeButtonContent = new()
        {
            [AttributeTypes.Content] = (properties) => properties.RemoveButton?.Content,
        };

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
        public string? FormClass => _options.FormClass;

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

        /// <inheritdoc/>
        public Dictionary<string, object?> GetAttributes(BuilderType type, AttributeTypes attributes, string? id = null, string? forId = null)
        {
            var result = new Dictionary<string, object?>();
            if (!string.IsNullOrEmpty(id))
            {
                result["id"] = id;
            }
            if (!string.IsNullOrEmpty(forId))
            {
                result["for"] = forId;
            }
            if (TryGetProperties(type, out var properties))
            {
                Dictionary<AttributeTypes, Func<BuilderTypeProperties, (string AttributeName, string? Value)>> selector;
                if (attributes.HasFlag(AttributeTypes.AddButton))
                {
                    selector = _addButtonAttributes;
                }
                else if (attributes.HasFlag(AttributeTypes.RemoveButton))
                {
                    selector = _removeButtonAttributes;
                }
                else
                {
                    selector = _attributes;
                }

                foreach (var attribute in selector)
                {
                    if (attributes.HasFlag(attribute.Key))
                    {
                        var (attributeName, value) = attribute.Value(properties);
                        if (result.ContainsKey(attributeName))
                        {
                            result[attributeName] += $" {value}";
                        }
                        else
                        {
                            result[attributeName] = value;
                        }
                    }
                }
            }

            return result;
        }

        /// <inheritdoc/>
        public InlineContent? GetContent(BuilderType type, AttributeTypes attributes, string? defaultValue = null)
        {
            if (TryGetProperties(type, out var properties))
            {
                Dictionary<AttributeTypes, Func<BuilderTypeProperties, string?>> selector;
                if (attributes.HasFlag(AttributeTypes.AddButton))
                {
                    selector = _addButtonContent;
                }
                else if (attributes.HasFlag(AttributeTypes.RemoveButton))
                {
                    selector = _removeButtonContent;
                }
                else
                {
                    selector = _content;
                }

                foreach (var contentType in selector)
                {
                    if (attributes.HasFlag(contentType.Key))
                    {
                        var value = contentType.Value(properties);
                        if (!string.IsNullOrEmpty(value))
                        {
                            return new InlineContent
                            {
                                Content = value
                            };
                        }

                    }
                }
            }

            return !string.IsNullOrEmpty(defaultValue)
                ? new InlineContent
                {
                    Content = defaultValue
                }
                : null;
        }
    }
}
