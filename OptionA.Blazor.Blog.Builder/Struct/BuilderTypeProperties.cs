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
        /// Class for container (div element) around builder item
        /// </summary>
        public string? ContainerClass { get; set; }
        /// <summary>
        /// Content for the element to override the default
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// Additional attributes to set to override the default
        /// </summary>
        public Dictionary<string, object?>? AdditionalAttributes { get; set; }
    }
}
