namespace OptionA.Blazor.Components.Menu.Struct
{
    /// <inheritdoc/>
    public class MenuDataProvider : IMenuDataProvider
    {
        private readonly string? _defaultMenuClass;
        private readonly string? _defaultMenuItemClass;
        private readonly string? _defaultMenuLinkClass;
        private readonly string? _defaultMenuGroupClass;
        private readonly string? _defaultMenuDividerClass;
        private readonly string? _activeClass;
        private readonly string? _defaultMenuContainerClass;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration"></param>

        public MenuDataProvider(Action<MenuOptions>? configuration = null)
        {
            var options = new MenuOptions();
            configuration?.Invoke(options);

            _defaultMenuClass = options.DefaultMenuClass;
            _defaultMenuItemClass = options.DefaultMenuItemClass;
            _defaultMenuLinkClass = options.DefaultMenuLinkClass;
            _defaultMenuGroupClass = options.DefaultMenuGroupClass;
            _defaultMenuDividerClass = options.DefaultMenuDividerClass;
            _activeClass = options.ActiveClass;
            _defaultMenuContainerClass = options.DefaultMenuContainerClass;
        }

        /// <inheritdoc/>
        public string GetDividerClass() => _defaultMenuDividerClass ?? string.Empty;

        /// <inheritdoc/>
        public string GetGroupClass() => _defaultMenuGroupClass ?? string.Empty;

        /// <inheritdoc/>
        public string GetLinkClass() => _defaultMenuLinkClass ?? string.Empty;

        /// <inheritdoc/>
        public string GetMenuClass() => _defaultMenuClass ?? string.Empty;

        /// <inheritdoc/>
        public string GetMenuItemClass() => _defaultMenuItemClass ?? string.Empty;

        /// <inheritdoc/>
        public string GetActiveClass() => _activeClass ?? string.Empty;

        /// <inheritdoc/>
        public string GetMenuContainerClass() => _defaultMenuContainerClass ?? string.Empty;


    }
}
