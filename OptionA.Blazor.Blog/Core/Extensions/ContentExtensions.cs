using OptionA.Blazor.Blog.Struct;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Extensions for content and icontent
    /// </summary>
    public static class ContentExtensions
    {
        /// <summary>
        /// Gets the attributes for the given content
        /// </summary>
        /// <param name="content"></param>
        /// <param name="dataProvider"></param>
        /// <returns></returns>
        public static Dictionary<string, object?>? GetAttributes(this IContent content, IBlogDataProvider dataProvider)
        {
            var defaultClasses = dataProvider.DefaultClassesForType(content.Type);
            if (content.AdditionalClasses.Any() || defaultClasses.Any() || content.RemovedClasses.Any())
            {
                var classes = content.AdditionalClasses
                    .Concat(defaultClasses)
                    .Except(content.RemovedClasses)
                    .Distinct();
                content.Attributes["class"] = string.Join(' ', classes);
            }

            if (content is LinkContent link)
            {

                content.Attributes["href"] = link.Href;
                if (!string.IsNullOrEmpty(link.Target))
                {
                    content.Attributes["target"] = link.Target;
                }
            }

            return content.Attributes;
        }
    }
}
