namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Options for blog components
    /// </summary>
    public class BlogOptions
    {
        /// <summary>
        /// If certain content types require default classes fill them here.
        /// </summary>
        public Dictionary<ContentType, List<string>>? DefaultClassesPerType { get; set; }
    }
}
