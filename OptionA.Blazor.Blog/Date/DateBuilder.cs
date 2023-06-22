namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Builder for building <see cref="Date.Content"/>
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public class DateBuilder<Parent> : ContentBuilderBase<DateBuilder<Parent>, Parent, DateContent>
        where Parent : IParentBuilder
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="parent"></param>
        public DateBuilder(Parent parent) : base(parent)
        {
        }

        /// <summary>
        /// Sets the date of the content
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateBuilder<Parent> ForDate(DateTime date)
        {
            _content.Date = date; 
            return this;
        }

        /// <summary>
        /// sets the <see cref="DateDisplayType"/> of the content
        /// </summary>
        /// <param name="display"></param>
        /// <returns></returns>
        public DateBuilder<Parent> WithDisplayType(DateDisplayType display)
        {
            _content.DisplayType = display;
            return this;
        }

        /// <inheritdoc/>
        protected override DateBuilder<Parent> This()
        {
            return this;
        }
    }
}
