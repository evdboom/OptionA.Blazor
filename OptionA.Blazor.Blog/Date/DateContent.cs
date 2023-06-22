namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for <see cref="Date"/> component, inherits from <see cref="BlockContent"/>
    /// </summary>
    public class DateContent : BlockContent
    {
        /// <summary>
        /// Date to display
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Display type
        /// </summary>
        public DateDisplayType DisplayType { get; set; }
        /// <inheritdoc/>
        public override ComponentType Type => ComponentType.Date;
        /// <summary>
        /// Display the date in the correct format, cannot be set for this component
        /// </summary>
        public override string Text
        {
            get => DisplayType.ToDateFormat(Date);
            set => throw new InvalidOperationException("Cannot set Text of Date component, set Date property instead");
        }
    }
}
