namespace OptionA.Blazor.Components.Carousel.Struct
{
    /// <summary>
    /// Default implementation of the <see cref="ICarouselDataProvider"/>
    /// </summary>
    public class CarouselDataProvider : ICarouselDataProvider
    {
        private readonly string? _autoPlayText;
        private readonly string? _autoPlayClasses;
        private readonly string? _itemSelectClasses;
        private readonly string? _activeItemSelectClasses;
        private readonly string? _inactiveItemSelectClasses;
        private readonly string? _nextClasses;
        private readonly string? _previousClasses;
        private readonly string? _nextIconClasses;
        private readonly string? _previousIconClasses;
        private readonly Dictionary<string, object?>? _itemSelectAttributes;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration"></param>
        public CarouselDataProvider(Action<CarouselOptions>? configuration = null)
        {
            var options = new CarouselOptions();
            configuration?.Invoke(options);

            _autoPlayText = options.AutoPlayText;
            _autoPlayClasses = options.AutoPlayClasses;
            _itemSelectClasses = options.ItemSelectClasses;
            _nextClasses = options.NextClasses;
            _previousClasses = options.PreviousClasses;
            _activeItemSelectClasses = options.ActiveItemSelectClasses;
            _inactiveItemSelectClasses = options.InactiveItemSelectClasses;
            _nextIconClasses = options.NextIconClasses;
            _previousIconClasses = options.PreviousIconClasses;
            _itemSelectAttributes = options.ItemSelectAttributes;
        }

        /// <inheritdoc/>
        public string GetAutoPlayText() => _autoPlayText ?? string.Empty;
        /// <inheritdoc/>
        public string DefaultAutoPlayClasses() => _autoPlayClasses ?? string.Empty;
        /// <inheritdoc/>
        public string DefaultItemSelectClasses() => _itemSelectClasses ?? string.Empty;
        /// <inheritdoc/>
        public string DefaultActiveItemSelectClasses() => _activeItemSelectClasses ?? string.Empty;
        /// <inheritdoc/>
        public string DefaultInactiveItemSelectClasses() => _inactiveItemSelectClasses ?? string.Empty;
        /// <inheritdoc/>
        public string DefaultNextClasses() => _nextClasses ?? string.Empty;
        /// <inheritdoc/>
        public string DefaultPreviousClasses() => _previousClasses ?? string.Empty;
        /// <inheritdoc/>
        public string DefaultNextIconClasses() => _nextIconClasses ?? string.Empty;
        /// <inheritdoc/>
        public string DefaultPreviousIconClasses() => _previousIconClasses ?? string.Empty;
        /// <inheritdoc/>
        public IDictionary<string, object?> AdditionalAttributesItemSelect() => _itemSelectAttributes ?? new();
    }
}
