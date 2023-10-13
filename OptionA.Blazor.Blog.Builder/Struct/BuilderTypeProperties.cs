namespace OptionA.Blazor.Blog.Builder
{
    /// <summary>
    /// Properties for a input item
    /// </summary>
    public class BuilderTypeProperties
    {
        /// <summary>
        /// Class to add to component
        /// </summary>
        public string? Class { get; set; }
        /// <summary>
        /// Content for the element to override the default
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// ContentType for the create content
        /// </summary>
        public ContentType? ContentType { get; set; }
        /// <summary>
        /// Additional attributes to set to override the default
        /// </summary>
        public Dictionary<string, object?>? AdditionalAttributes { get; set; }
    }
}
