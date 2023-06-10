namespace OptionA.Blazor.Components.Menu.Struct
{
    /// <inheritdoc/>
    public class MenuDataProvider : IMenuDataProvider
    {
        private readonly MenuOptions _options;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration"></param>

        public MenuDataProvider(Action<MenuOptions>? configuration = null)
        {
            _options = new MenuOptions();
            configuration?.Invoke(_options);
        }

        /// <inheritdoc/>
        public string GetDividerClass() => _options.DefaultMenuDividerClass ?? string.Empty;

        /// <inheritdoc/>
        public string GetGroupClass() => _options.DefaultMenuGroupClass ?? string.Empty;

        /// <inheritdoc/>
        public string GetLinkClass() => _options.DefaultMenuLinkClass ?? string.Empty;

        /// <inheritdoc/>
        public string GetMenuClass() => _options.DefaultMenuClass ?? string.Empty;

        /// <inheritdoc/>
        public string GetMenuItemClass() => _options.DefaultMenuItemClass ?? string.Empty;

        /// <inheritdoc/>
        public string GetActiveClass() => _options.ActiveClass ?? string.Empty;

        /// <inheritdoc/>
        public string GetMenuContainerClass() => _options.DefaultMenuContainerClass ?? string.Empty;

        /// <inheritdoc/>
        public bool OpenGroupOnMouseOver() => _options.OpenGroupOnMouseOver ?? false;

        /// <inheritdoc/>
        public int CloseGroupAfterMilisecondDelay() => _options.CloseGroupAfterMillisecondDelay ?? 0;


    }
}
