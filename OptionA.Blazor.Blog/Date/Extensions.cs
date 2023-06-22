namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Extensions for the Date classes
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Create a new datebuilder from any <see cref="IParentBuilder"/>
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static DateBuilder<Parent> CreateDate<Parent>(this Parent parent) where Parent : IParentBuilder
        {
            return new DateBuilder<Parent>(parent);
        }

        /// <summary>
        /// Adds a date to the parent builder, using the <see cref="DateDisplayType.Date"/> format
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Parent AddDate<Parent>(this Parent parent, DateTime date) where Parent : IParentBuilder
        {
            return AddDate(parent, date, DateDisplayType.Date);
        }

        /// <summary>
        /// Adds a date to the parent builder
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="date"></param>
        /// <param name="display"></param>
        /// <returns></returns>
        public static Parent AddDate<Parent>(this Parent parent, DateTime date, DateDisplayType display) where Parent : IParentBuilder
        {
            return CreateDate(parent)
                .ForDate(date)
                .WithDisplayType(display)
                .Build();
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
                _ => throw new ArgumentException($"Unknown DisplayType {type}")
            };
        }
    }
}
