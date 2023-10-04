using System.Diagnostics.CodeAnalysis;

namespace OptionA.Blazor.Components.Modal.Struct
{
    /// <summary>
    /// Default implementation of <see cref="IModalDataProvider"/>
    /// </summary>
    public class ModalDataProvider : IModalDataProvider
    {
        private readonly ModalOptions _options;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration"></param>
        public ModalDataProvider(Action<ModalOptions>? configuration = null)
        {
            _options = new ModalOptions();
            configuration?.Invoke(_options);
        }

        /// <inheritdoc/>
        public string? ModalClass => _options.ModalClass;
        /// <inheritdoc/>
        public string? DialogClass => _options.DialogClass;
        /// <inheritdoc/>
        public string? ContentClass => _options.ContentClass;
        /// <inheritdoc/>
        public string? HeaderClass => _options.HeaderClass;
        /// <inheritdoc/>
        public string? TitleClass => _options.TitleClass;
        /// <inheritdoc/>
        public string? CloseButtonClass => _options.CloseButtonClass;
        /// <inheritdoc/>
        public string? CloseButtonContent => _options.CloseButtonContent;
        /// <inheritdoc/>
        public string? BodyClass => _options.BodyClass;
        /// <inheritdoc/>
        public string? FooterClass => _options.FooterClass;
        /// <inheritdoc/>
        public string? BackdropClass => _options.BackdropClass;
        /// <inheritdoc/>
        public bool Draggable => _options.Draggable;
        /// <inheritdoc/>
        public string? ScrollableDialogClass => _options.ScrollableDialogClass;
        /// <inheritdoc/>
        public int ModalZIndex => _options.ModalZIndex ?? 1000;
        /// <inheritdoc/>
        public int ModalZIndexSteps => _options.ModalZIndexSteps ?? 1;
        /// <inheritdoc/>
        public bool TryGetClassForSize(ModalSize size, [NotNullWhen(true)] out string? className)
        {
            if (_options.SizeClasses is null)
            {
                className = null;
                return false;
            }

            return _options.SizeClasses.TryGetValue(size, out className);
        }
    }
}
