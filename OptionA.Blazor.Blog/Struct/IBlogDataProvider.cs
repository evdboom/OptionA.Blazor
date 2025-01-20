namespace OptionA.Blazor.Blog;

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
    HeaderSize PostHeaderSize { get; }
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
    DateDisplayType PostDateDisplay { get; }
    /// <summary>
    /// Class to add to header of the post
    /// </summary>
    string? PostTitleClass { get; }
    /// <summary>
    /// Class to add to the date part of post header
    /// </summary>
    string? PostDateClass { get; }
    /// <summary>
    /// Class to add to the subtitle of the post
    /// </summary>
    string? PostSubtitleClass { get; }
    /// <summary>
    /// Base url for when clicking on a tag
    /// </summary>
    string? TagOverviewHref { get; }
    /// <summary>
    /// True to place a hr tag after the header part of the post
    /// </summary>
    bool DisplayLineAfterPostHeader { get; }
    /// <summary>
    /// New line to use for the blog code parser, default is <see cref="Environment.NewLine"/>
    /// </summary>
    string? NewLine { get; }

}
