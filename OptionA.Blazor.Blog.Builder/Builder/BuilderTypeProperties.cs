namespace OptionA.Blazor.Blog.Builder
{
    /// <summary>
    /// Properties for a input item
    /// </summary>
    public class BuilderTypeProperties
    {
        /// <summary>
        /// Name or label to diplay over input (if applicable)
        /// </summary>
        public string? Label { get; set; }
        /// <summary>
        /// Placeholder to diplay in input (if applicable)
        /// </summary>
        public string? Placeholder { get; set; }
        /// <summary>
        /// Class to add to component
        /// </summary>
        public string? Class { get; set; }
        /// <summary>
        /// Value of item (Markdown and icon, for button builder types)
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// Class for container (div element) around builder item
        /// </summary>
        public string? ContainerClass { get; set; }
    }
}
