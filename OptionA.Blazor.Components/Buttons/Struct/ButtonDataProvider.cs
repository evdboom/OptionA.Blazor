using OptionA.Blazor.Components.Buttons.Enum;

namespace OptionA.Blazor.Components.Buttons.Struct
{
    /// <summary>
    /// Default implementation of <see cref="IButtonDataProvider"/>
    /// </summary>
    public class ButtonDataProvider : IButtonDataProvider
    {
        private readonly IDictionary<ActionType, string> _buttonClasses;
        private readonly string _defaultButtonClass;
        private readonly IDictionary<ActionType, string> _iconClasses;
        private readonly string _defaultIconClass;

        /// <summary>
        /// Constructor, pass classes here
        /// </summary>
        /// <param name="buttonClasses"></param>
        /// <param name="defaultButtonClass"></param>
        /// <param name="iconClasses"></param>
        /// <param name="defaultIconClass"></param>
        public ButtonDataProvider(IDictionary<ActionType, string> buttonClasses, string defaultButtonClass, IDictionary<ActionType, string> iconClasses, string defaultIconClass)
        {
            _buttonClasses = buttonClasses;
            _defaultButtonClass = defaultButtonClass;
            _iconClasses = iconClasses;
            _defaultIconClass = defaultIconClass;
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

            if (_buttonClasses.TryGetValue(actionType, out string? buttonClass))
            {
                return buttonClass;
            }

            return _defaultButtonClass;
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

            if (_iconClasses.TryGetValue(actionType, out string? iconClass))
            {
                return iconClass;
            }

            return _defaultIconClass;
        }
    }
}
