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
        public string DividerClass => _options.DefaultMenuDividerClass ?? string.Empty;

        /// <inheritdoc/>
        public string GroupClass => _options.DefaultMenuGroupClass ?? string.Empty;

        /// <inheritdoc/>
        public string LinkClass => _options.DefaultMenuLinkClass ?? string.Empty;

        /// <inheritdoc/>
        public string MenuClass => _options.DefaultMenuClass ?? string.Empty;

        /// <inheritdoc/>
        public string MenuItemClass => _options.DefaultMenuItemClass ?? string.Empty;

        /// <inheritdoc/>
        public string ActiveClass => _options.ActiveClass ?? string.Empty;

        /// <inheritdoc/>
        public string MenuContainerClass => _options.DefaultMenuContainerClass ?? string.Empty;

        /// <inheritdoc/> 
        public string DropdownClass => _options.DefaultDropdownClass ?? string.Empty;

        /// <inheritdoc/>
        public bool OpenGroupOnMouseOver => _options.OpenGroupOnMouseOver ?? false;

        /// <inheritdoc/>
        public int GroupCloseTime => _options.GroupCloseTime ?? 0;


    }
}
