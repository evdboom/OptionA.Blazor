namespace OptionA.Blazor.Components.Gallery.Struct;

/// <summary>
/// Default implementation of the <see cref="IGalleryDataProvider"/>
/// </summary>
public class GalleryDataProvider : IGalleryDataProvider
{
    private readonly GalleryOptions _options;       

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="configuration"></param>
    public GalleryDataProvider(Action<GalleryOptions>? configuration = null)
    {
        _options = new GalleryOptions();
        configuration?.Invoke(_options);
    }
    /// <inheritdoc/>
    public string DefaultNextClasses() => _options.NextClasses ?? string.Empty;
    /// <inheritdoc/>
    public string DefaultPreviousClasses() => _options.PreviousClasses ?? string.Empty;
    /// <inheritdoc/>
    public string DefaultNextIconClasses() => _options.NextIconClasses ?? string.Empty;
    /// <inheritdoc/>
    public string DefaultPreviousIconClasses() => _options.PreviousIconClasses ?? string.Empty;
    /// <inheritdoc/>
    public string GetThumbnailContainerClasses(GalleryMode mode) => mode == GalleryMode.SideBySide
        ? (_options.SideBySideThumbnailContainerClasses ?? string.Empty)
        : (_options.ModalThumbnailContainerClasses ?? string.Empty);
    /// <inheritdoc/>
    public string GetImageContainerClasses(GalleryMode mode) => mode == GalleryMode.SideBySide
        ? (_options.SideBySideImageContainerClasses ?? string.Empty)
        : (_options.ModalImageContainerClasses ?? string.Empty);
    /// <inheritdoc/>
    public string GetDefaultImageClasses() => _options.ImageClasses ?? string.Empty;
    /// <inheritdoc/>
    public string GetDefaultThumbnailClasses() => _options.ThumbnailClasses ?? string.Empty;
    /// <inheritdoc/>
    public string GetGalleryClasses(GalleryMode mode) => mode == GalleryMode.SideBySide
        ? (_options.SideBySideGalleryClasses ?? string.Empty)
        : (_options.ModalGalleryClasses ?? string.Empty);
    /// <inheritdoc/>
    public string ModalCloseButtonClasses() => _options.ModalCloseButtonClasses ?? string.Empty;
    /// <inheritdoc/>
    public string ModalBackgroundClasses() => _options.ModalBackgroundClasses ?? string.Empty;        
    /// <inheritdoc/>
    public string ModalClasses() => _options.ModalClasses ?? string.Empty;
    /// <inheritdoc/>
    public string ModalCloseButtonText() => _options.ModalCloseButtonText ?? string.Empty;
    /// <inheritdoc/>
    public string ModalWrapperClasses() => _options.ModalWrapperClasses ?? string.Empty;        
}
