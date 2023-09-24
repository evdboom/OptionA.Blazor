namespace OptionA.Blazor.Blog.Builder
{
    /// <summary>
    /// Options for blog components
    /// </summary>
    public class OptaBlogBuilderOptions
    {        
        /// <summary>
        /// Class to add to the post input elements
        /// </summary>
        public string? InputClass { get; set; }
        /// <summary>
        /// Class to add to the post labels for the connected input elements
        /// </summary>
        public string? LabelClass { get; set; }
        /// <summary>
        /// Class for the form the post is build in
        /// </summary>
        public string? Form { get; set; }
        /// <summary>
        /// Specific options for the post builder
        /// </summary>
        public Dictionary<BuilderType, BuilderTypeProperties>? PostBuilderOptions { get; set; }
    }
