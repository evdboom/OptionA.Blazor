namespace OptionA.Blazor.Blog.Builder
{
    /// <summary>
    /// Options for blog components
    /// </summary>
    public class OptaBlogBuilderOptions
    {
        /// <summary>
        /// Class for the form the post is build in
        /// </summary>
        public string? FormClass { get; set; }
        /// <summary>
        /// Specific options for the post builder
        /// </summary>
        public Dictionary<BuilderType, BuilderTypeProperties>? PostBuilderOptions { get; set; }
    }
}
