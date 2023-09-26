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
        /// <summary>
        /// Options for the create new post button
        /// </summary>
        public BuilderTypeProperties? CreatePostButton { get; set; }
        /// <summary>
        /// Options for the save post button
        /// </summary>
        public BuilderTypeProperties? SavePostButton { get; set; }
        /// <summary>
        /// Headersize for new headers in the post, defaults to two (smaller then post title)
        /// </summary>
        public HeaderSize? DefaultHeaderSize { get; set; }
    }
}
