using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Post component
/// </summary>
public partial class OptAPost
{
    /// <summary>
    /// Post to display
    /// </summary>
    [Parameter]
    public Post? Post { get; set; }

    /// <summary>
    /// Title to display
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// Tags to display
    /// </summary>
    [Parameter]
    public IEnumerable<string>? Tags { get; set; }

    /// <summary>
    /// Date to display
    /// </summary>
    [Parameter]
    public DateTime? Date { get; set; }

    /// <summary>
    /// Subtitle to display
    /// </summary>
    [Parameter]
    public string? Subtitle { get; set; }

    /// <summary>
    /// Content to display when no child content is supplied
    /// </summary>
    [Parameter]
    public IEnumerable<IContent>? Content { get; set; }

    /// <summary>
    /// Child content for fully dynamic post body rendering
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Inject]
    private IBlogDataProvider DataProvider { get; set; } = null!;

    private HeaderContent? _title;
    private IEnumerable<IContent>? _tags;
    private BlockContent? _date;
    private BlockContent? _subtitle;
    private IEnumerable<IContent>? _content;

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        var effectiveTitle = Title ?? Post?.Title;
        var effectiveTags = Tags ?? Post?.Tags;
        var effectiveDate = Date ?? Post?.Date;
        var effectiveSubtitle = Subtitle ?? Post?.Subtitle;

        _title = null;
        _tags = null;
        _date = null;
        _subtitle = null;
        _content = Content ?? Post?.Content;

        if (!string.IsNullOrEmpty(effectiveTitle))
        {
            _title = new HeaderContent
            {
                Content = effectiveTitle,
                Size = DataProvider.PostHeaderSize,
            };
            if (!string.IsNullOrEmpty(DataProvider.PostTitleClass))
            {
                _title.AdditionalClasses.Add(DataProvider.PostTitleClass);
            }
        }

        if (effectiveTags is not null)
        {
            _tags = effectiveTags.Select(GetTagContent);
        }

        if (effectiveDate.HasValue && effectiveDate.Value >= DateTime.MinValue)
        {
            _date = new BlockContent
            {
                Content = DataProvider.PostDateDisplay.ToDateFormat(effectiveDate.Value),
            };

            if (!string.IsNullOrEmpty(DataProvider.PostDateClass))
            {
                _date.AdditionalClasses.Add(DataProvider.PostDateClass);
            }
        }

        if (!string.IsNullOrEmpty(effectiveSubtitle))
        {
            _subtitle = new BlockContent { Content = effectiveSubtitle };
            if (!string.IsNullOrEmpty(DataProvider.PostSubtitleClass))
            {
                _subtitle.AdditionalClasses.Add(DataProvider.PostSubtitleClass);
            }
        }
    }

    private IContent GetTagContent(string tag)
    {
        IContent result = !string.IsNullOrEmpty(DataProvider.TagOverviewHref)
            ? new LinkContent
            {
                Content = tag,
                Href = $"{DataProvider.TagOverviewHref}/{tag}".ToLowerInvariant(),
                Target = "_self",
            }
            : new InlineContent { Content = tag };
        if (!string.IsNullOrEmpty(DataProvider.TagClass))
        {
            result.AdditionalClasses.Add(DataProvider.TagClass);
        }

        return result;
    }
}
