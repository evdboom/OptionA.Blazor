namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Provides default classes for blog components
    /// </summary>
    public interface IBlogDataProvider
    {
        /// <summary>
        /// Gets the default classes for the given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<string> DefaultClassesForType(ContentType type);
        /// <summary>
        /// Header size to use for the title of the post
        /// </summary>
        HeaderSize? PostHeaderSize { get; }
        /// <summary>
        /// Class to add to tags
        /// </summary>
        string? TagClass { get; }
        /// <summary>
        /// Class to add to tag container in title
        /// </summary>
        string? HeaderTagContainerClass { get; }
        /// <summary>
        /// Way to display post date
        /// </summary>
        DateDisplayType? PostDateDisplay { get; }
        /// <summary>
        /// Class to add to header of the post
        /// </summary>
        string? PostTitleClass { get; }
        /// <summary>
        /// Class to add to the subtitle of the post
        /// </summary>
        string? PostSubtitleClass { get; }

    }
}
