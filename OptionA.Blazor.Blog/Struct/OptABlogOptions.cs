namespace OptionA.Blazor.Blog;

/// <summary>
/// Options for blog components
/// </summary>
public class OptABlogOptions
{
    /// <summary>
    /// If certain content types require default classes fill them here.
    /// </summary>
    public Dictionary<ContentType, List<string>>? DefaultClassesPerType { get; set; }
    /// <summary>
    /// Header size for post headers, defaults to <see cref="HeaderSize.One"/>
    /// </summary>
    public HeaderSize? PostHeaderSize { get; set; }
    /// <summary>
    /// Class to add to tags in the blog
    /// </summary>
    public string? TagClass { get; set; }
    /// <summary>
    /// Post header tag container class
    /// </summary>
    public string? HeaderTagContainerClass { get; set; }
    /// <summary>
    /// Display format for the post date in the post header
    /// </summary>
    public DateDisplayType? PostDateDisplay { get; set; }
    /// <summary>
    /// Post header title class
    /// </summary>
    public string? PostTitleClass { get; set; }
    /// <summary>
    /// Post header date class
    /// </summary>
    public string? PostDateClass { get; set; }
    /// <summary>
    /// Post header Subtitle class
    /// </summary>
    public string? PostSubtitleClass { get; set; }
    /// <summary>
    /// If tags can be used as link, base url (relative to page), without trailing /
    /// </summary>
    public string? TagOverviewHref { get; set; }
    /// <summary>
    /// True if a hr tag should be placed after post header, default is true
    /// </summary>
    public bool? DisplayLineAfterPostHeader { get; set; }
    /// <summary>
    /// New line to use for the blog code parser, default is <see cref="Environment.NewLine"/>
    /// </summary>
    public string? NewLine { get; set; }
}
