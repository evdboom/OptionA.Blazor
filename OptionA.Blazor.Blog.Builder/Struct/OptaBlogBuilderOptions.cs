namespace OptionA.Blazor.Blog.Builder
{
    /// <summary>
    /// Options for blog components
    /// </summary>
    public class OptaBlogBuilderOptions
    {
        /// <summary>
        /// Specific options for the post builder
        /// </summary>
        public Dictionary<BuilderType, BuilderTypeProperties>? PostBuilderOptions { get; set; }
        /// <summary>
        /// Headersize for new headers in the post, defaults to two (smaller then post title)
        /// </summary>
        public HeaderSize? DefaultHeaderSize { get; set; }
        /// <summary>
        /// Default code language for new code blocks
        /// </summary>
        public CodeLanguage? DefaultCodeLanguage { get; set; }
    }
}
