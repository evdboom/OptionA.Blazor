using System.Text.Json;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for <see cref="Date"/> component, inherits from <see cref="BlockContent"/>
    /// </summary>
    public class DateContent : BlockContent
    {
        /// <summary>
        /// Default contstructor
        /// </summary>
        public DateContent() : base() { }
        /// <summary>
        /// Constructor for use in deserialization
        /// </summary>
        /// <param name="items"></param>
        public DateContent(Dictionary<string, JsonElement> items) : base(items)
        {
            if (items.TryGetValue(nameof(Date), out var date))
            {
                Date = JsonSerializer.Deserialize<DateTime>(Date);
            }
            if (items.TryGetValue(nameof(DisplayType), out var type))
            {
                DisplayType = JsonSerializer.Deserialize<DateDisplayType>(type);
            }
        }

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

        /// <inheritdoc />
        protected override void OnSerialize(Dictionary<string, object> items)
        {
            base.OnSerialize(items);
            items[nameof(Date)] = Date;
            items[nameof(DisplayType)] = DisplayType;
        }
    }
}
