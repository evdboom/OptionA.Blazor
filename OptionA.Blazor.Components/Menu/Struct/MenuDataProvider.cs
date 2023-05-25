namespace OptionA.Blazor.Components.Menu.Struct
{
    /// <inheritdoc/>
    public class MenuDataProvider : IMenuDataProvider
    {
        private readonly string _defaultMenuClass;
        private readonly string _defaultMenuItemClass;
        private readonly string _defaultMenuLinkClass;
        private readonly string _defaultMenuGroupClass;
        private readonly string _defaultMenuDividerClass;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="defaultMenuClass"></param>
        /// <param name="defaultMenuItemClass"></param>
        /// <param name="defaultMenuLinkClass"></param>
        /// <param name="defaultMenuGroupClass"></param>
        /// <param name="defaultMenuDividerClass"></param>
        public MenuDataProvider(string defaultMenuClass, string defaultMenuItemClass, string defaultMenuLinkClass, string defaultMenuGroupClass, string defaultMenuDividerClass)
        {
            _defaultMenuClass = defaultMenuClass;
            _defaultMenuItemClass = defaultMenuItemClass;
            _defaultMenuLinkClass = defaultMenuLinkClass;
            _defaultMenuGroupClass = defaultMenuGroupClass;
            _defaultMenuDividerClass = defaultMenuDividerClass;
        }

        /// <inheritdoc/>
        public string GetDividerClass() => _defaultMenuDividerClass;

        /// <inheritdoc/>
        public string GetGroupClass() => _defaultMenuGroupClass;

        /// <inheritdoc/>
        public string GetLinkClass() => _defaultMenuLinkClass;

        /// <inheritdoc/>
        public string GetMenuClass() => _defaultMenuClass;

        /// <inheritdoc/>
        public string GetMenuItemClass() => _defaultMenuItemClass;
        
    }
}
