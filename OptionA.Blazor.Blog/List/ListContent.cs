namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for the <see cref="List.List"/> component
    /// </summary>
    public class ListContent : Content
    {
        ///<inheritdoc/>
        public override ComponentType Type => ComponentType.List;

        /// <summary>
        /// List or bullet style for this list
        /// </summary>
        public ListStyle ListStyle { get; set; }
        /// <summary>
        /// True if the list should be an ordered list
        /// </summary>
        public bool Ordered { get; set; }
        /// <summary>
        /// Start for the ordered list
        /// </summary>
        public int Start { get; set; } = 1;

        /// <summary>
        /// Returns the list-style content class if available in <see cref="DefaultClasses"/>
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<string> GetContentClassesList()
        {
            if (DefaultClasses.ListStyleClasses.TryGetValue(ListStyle, out string? className))
            {
                yield return className;
            }
        }

        /// <inheritdoc/>
        public override IDictionary<string, object?> Attributes
        {
            get
            {
                var attributes = base.Attributes;
                if (Start > 1)
                {
                    attributes["start"] = Start;
                }
                return attributes;
            }
        }
    }
}
