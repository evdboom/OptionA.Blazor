namespace OptionA.Blazor.Blog.Builder
{
    /// <summary>
    /// Properties for buttons associated with builder parts
    /// </summary>
    public class ButtonProperties
    {
        /// <summary>
        /// Content for the button (supports basic mardown)
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// Class to add to button
        /// </summary>
        public string? Class { get; set; }
        /// <summary>
        /// Class for the div element around the button
        /// </summary>
        public string? ContainerClass { get; set; }
        /// <summary>
        /// Title for the button
        /// </summary>
        public string? Title { get; set; }
    }
}
