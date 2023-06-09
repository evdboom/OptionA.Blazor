namespace OptionA.Blazor.Components.Buttons.Struct
{
    /// <summary>
    /// Default implementation of <see cref="IButtonDataProvider"/>
    /// </summary>
    public class ButtonDataProvider : IButtonDataProvider
    {
        private readonly Dictionary<ActionType, string>? _buttonClasses;
        private readonly string? _defaultButtonClass;
        private readonly Dictionary<ActionType, string>? _iconClasses;
        private readonly string? _defaultIconClass;

        /// <summary>
        /// Constructor, pass classes here
        /// </summary>
        /// <param name="configuration"></param>
        public ButtonDataProvider(Action<ButtonOptions>? configuration = null)
        {
            var options = new ButtonOptions();
            configuration?.Invoke(options);

            _buttonClasses = options.ButtonClasses;
            _defaultButtonClass = options.DefaultButtonClass;
            _iconClasses = options.IconClasses;
            _defaultIconClass = options.DefaultIconClass;
        }

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

            if (_buttonClasses?.TryGetValue(actionType, out string? buttonClass) ?? false)
            {
                return buttonClass;
            }

            return _defaultButtonClass ?? string.Empty;
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

            if (_iconClasses?.TryGetValue(actionType, out string? iconClass) ?? false)
            {
                return iconClass;
            }

            return _defaultIconClass ?? string.Empty;
        }
    }
}
