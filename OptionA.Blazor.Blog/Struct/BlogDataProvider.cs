namespace OptionA.Blazor.Blog.Struct;

/// <summary>
/// Implementation of <see cref="IBlogDataProvider"/>
/// </summary>
public class BlogDataProvider : IBlogDataProvider
{
    private readonly OptABlogOptions _options;

    /// <summary>
    /// Configured options for this provider
    /// </summary>
    public OptABlogOptions Options => _options;

    /// <inheritdoc/>
    public HeaderSize PostHeaderSize => _options.PostHeaderSize ?? HeaderSize.One;
    /// <inheritdoc/>
    public string? TagClass => _options.TagClass;
    /// <inheritdoc/>
    public string? HeaderTagContainerClass => _options.HeaderTagContainerClass;
    /// <inheritdoc/>
    public DateDisplayType PostDateDisplay => _options.PostDateDisplay ?? DateDisplayType.LongDate;
    /// <inheritdoc/>
    public string? PostTitleClass => _options.PostTitleClass;
    /// <inheritdoc/>
    public string? PostDateClass => _options.PostDateClass;
    /// <inheritdoc/>
    public string? PostSubtitleClass => _options.PostSubtitleClass;
    /// <inheritdoc/>
    public string? TagOverviewHref => _options.TagOverviewHref;
    /// <inheritdoc/>
    public bool DisplayLineAfterPostHeader => _options.DisplayLineAfterPostHeader ?? true;
    /// <inheritdoc/>
    public string? NewLine => _options.NewLine;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    public BlogDataProvider(Action<OptABlogOptions>? configuration = null)
    {
        _options = new();
        configuration?.Invoke(_options);
    }

    /// <inheritdoc/>
    public List<string> DefaultClassesForType(ContentType type)
    {
        if (_options.DefaultClassesPerType != null && _options.DefaultClassesPerType.TryGetValue(type, out var classes))
        {
            return classes;
        }

        return new();
    }
}
