namespace OptionA.Blazor.Components.Message.Struct
{
    /// <inheritdoc/>
    public class MessageBoxDataProvider : IMessageBoxDataProvider
    {
        private readonly MessageBoxOptions _options;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration"></param>
        public MessageBoxDataProvider(Action<MessageBoxOptions>? configuration = null)
        {
            _options = new MessageBoxOptions();
            configuration?.Invoke(_options);
        }

        /// <inheritdoc/>
        public string ContainerClass => _options.ContainerClass ?? string.Empty;
        /// <inheritdoc/>
        public Location DefaultLocation => _options.DefaultLocation;
        /// <inheritdoc/>
        public int DefaultZIndex => _options.DefaultZIndex;
        /// <inheritdoc/>
        public bool ShowTime => _options.ShowTime;
        /// <inheritdoc/>
        public string? CloseButtonContent => _options.CloseButtonContent;
        /// <inheritdoc/>
        public string? ContentClass => _options.ContentClass;
        /// <inheritdoc/>
        public string? BodyClass => _options.BodyClass;
        /// <inheritdoc/>
        public string? HeaderClass => _options.HeaderClass;
        /// <inheritdoc/>
        public bool GetDefaultDismissable(MessageType type)
        {
            if (_options.Dismissable is null || !_options.Dismissable.TryGetValue(type, out var result))
            {
                return _options.DefaultDismissable;
            }
            else
            {
                return result;
            }
        }
        /// <inheritdoc/>
        public int GetDefaultTimeout(MessageType type)
        {
            if (_options.TimeOuts is null || !_options.TimeOuts.TryGetValue(type, out var result))
            {
                return _options.DefaultTimeOut;
            }
            else
            {
                return result;
            }
        }
        /// <inheritdoc/>
        public string GetMessageClasses(MessageType type)
        {
            if (_options.MessageClasses is null || !_options.MessageClasses.TryGetValue(type, out var result))
            {
                return _options.DefaultMessageClasses ?? string.Empty;
            }
            else
            {
                return result;
            }
        }
        /// <inheritdoc/>
        public string GetCloseButtonClasses(MessageType type)
        {
            if (_options.CloseButtonClasses is null || !_options.CloseButtonClasses.TryGetValue(type, out var result))
            {
                return _options.DefaultCloseButtonClass ?? string.Empty;
            }
            else
            {
                return result;
            }
        }
    }
}
