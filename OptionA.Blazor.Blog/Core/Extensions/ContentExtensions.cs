using OptionA.Blazor.Blog.Code.Parsers;
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

        /// <summary>
        /// Get the normal name for the given codelanguage
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string ToDisplayLanguage(this CodeLanguage language)
        {
            return language switch
            {
                CodeLanguage.CSharp => "C#",
                CodeLanguage.Html => "HTML",
                _ => $"{language}"
            };
        }

        /// <summary>
        /// returns the value for the attribute to set
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToAttribute(this CodeType type)
        {
            // currently all attribute match enum names, might change to switch later
            return $"{type}".ToLowerInvariant();
        }

        /// <summary>
        /// returns a string for the given date in the selected format.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string ToDateFormat(this DateDisplayType type, DateTime date)
        {
            return type switch
            {
                DateDisplayType.Date => $"{date:d}",
                DateDisplayType.DateTime => $"{date:g}",
                DateDisplayType.Time => $"{date:t}",
                DateDisplayType.UsableDate => $"{date:yyyyMMdd}",
                DateDisplayType.UsableDateTime => $"{date:u}",
                DateDisplayType.LongDate => $"{date:D}",
                DateDisplayType.LongDateTime => $"{date:f}",
                DateDisplayType.Month => $"{date:MMMM}",
                DateDisplayType.Year => $"{date:yyyy}",
                DateDisplayType.YearMonth => $"{date:Y}",
                _ => string.Empty
            };
        }
    }
}
