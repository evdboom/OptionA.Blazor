namespace OptionA.Blazor.Components.Buttons.Struct
{
    /// <summary>
    /// Default implementation of <see cref="IButtonDataProvider"/>
    /// </summary>
    public class ButtonDataProvider : IButtonDataProvider
    {
        private readonly ButtonOptions _options;

        /// <summary>
        /// Constructor, pass classes here
        /// </summary>
        /// <param name="configuration"></param>
        public ButtonDataProvider(Action<ButtonOptions>? configuration = null)
        {
            _options = new ButtonOptions();
            configuration?.Invoke(_options);
        }

        /// <inheritdoc/>
        public string? DefaultButtonBarClass => _options.DefaultButtonBarClass;

        /// <inheritdoc/>
        public string? DefaultButtonGroupClass => _options?.DefaultButtonGroupClass;

        /// <inheritdoc/>
        public string GetActionClass(ActionType actionType)
        {
            return GetActionClass(actionType, null);
        }

        /// <inheritdoc/>
        public string GetActionClass(ActionType actionType, string? otherButtonClass)
        {
            if (actionType == ActionType.Other && !string.IsNullOrEmpty(otherButtonClass))
            {
                return otherButtonClass;
            }

            if (_options.ButtonClasses?.TryGetValue(actionType, out string? buttonClass) ?? false)
            {
                return buttonClass;
            }

            return _options.DefaultButtonClass ?? string.Empty;
        }

        /// <inheritdoc/>
        public string GetIconClass(ActionType actionType)
        {
            return GetIconClass(actionType, null);
        }

        /// <inheritdoc/>
        public string GetIconClass(ActionType actionType, string? otherIconClass)
        {
            if (actionType == ActionType.Other && !string.IsNullOrEmpty(otherIconClass))
            {
                return otherIconClass;
            }

            if (_options.IconClasses?.TryGetValue(actionType, out string? iconClass) ?? false)
            {
                return iconClass;
            }

            return _options.DefaultIconClass ?? string.Empty;
        }
    }
}
