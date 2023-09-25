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
        /// Class to add to label of input
        /// </summary>
        public string? LabelClass { get; set; }
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
        /// <summary>
        /// Title for the element
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Class for around lists of items (for instance tags)
        /// </summary>
        public string? GroupClass { get; set; }
        /// <summary>
        /// Class for around lists of items (for instance tags), inside group
        /// </summary>
        public string? InnerGroupClass { get; set; }
        /// <summary>
        /// Class for around the extra properties of a component
        /// </summary>
        public string? ExtraPropertiesClass { get; set; }
        /// <summary>
        /// Properties for a add button (for lists, like tags)
        /// </summary>
        public ButtonProperties? AddButton { get; set; }
        /// <summary>
        /// Properties for a remove button (for lists, like tags)
        /// </summary>
        public ButtonProperties? RemoveButton { get; set; }
    }
}
