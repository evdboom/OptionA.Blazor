namespace OptionA.Blazor.Components.Carousel.Struct;

/// <summary>
/// Default implementation of the <see cref="ICarouselDataProvider"/>
/// </summary>
public class CarouselDataProvider : ICarouselDataProvider
{
    private readonly CarouselOptions _options;
    private readonly Dictionary<string, object?> _itemSelectAttributes;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="configuration"></param>
    public CarouselDataProvider(Action<CarouselOptions>? configuration = null)
    {
        _options = new CarouselOptions();
        configuration?.Invoke(_options);
        _itemSelectAttributes = _options.ItemSelectAttributes ?? new();
    }

    /// <inheritdoc/>
    public string GetAutoPlayText() => _options.AutoPlayText ?? string.Empty;
    /// <inheritdoc/>
    public string DefaultAutoPlayClasses() => _options.AutoPlayClasses ?? string.Empty;
    /// <inheritdoc/>
    public string DefaultItemSelectClasses() => _options.ItemSelectClasses ?? string.Empty;
    /// <inheritdoc/>
    public string DefaultActiveItemSelectClasses() => _options.ActiveItemSelectClasses ?? string.Empty;
    /// <inheritdoc/>
    public string DefaultInactiveItemSelectClasses() => _options.InactiveItemSelectClasses ?? string.Empty;
    /// <inheritdoc/>
    public string DefaultNextClasses() => _options.NextClasses ?? string.Empty;
    /// <inheritdoc/>
    public string DefaultPreviousClasses() => _options.PreviousClasses ?? string.Empty;
    /// <inheritdoc/>
    public string DefaultNextIconClasses() => _options.NextIconClasses ?? string.Empty;
    /// <inheritdoc/>
    public string DefaultPreviousIconClasses() => _options.PreviousIconClasses ?? string.Empty;
    /// <inheritdoc/>
    public IDictionary<string, object?> AdditionalAttributesItemSelect() => _itemSelectAttributes;
}
