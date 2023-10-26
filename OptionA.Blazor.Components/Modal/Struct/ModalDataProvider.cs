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
        public string DialogClass => _options.DialogClass ?? string.Empty;
        /// <inheritdoc/>
        public string? ContentClass => _options.ContentClass;
        /// <inheritdoc/>
        public string? SectionClass => _options.SectionClass;
        /// <inheritdoc/>
        public string? HeaderClass => _options.HeaderClass;
        /// <inheritdoc/>
        public string? CloseButtonClass => _options.CloseButtonClass;
        /// <inheritdoc/>
        public string? CloseButtonContent => _options.CloseButtonContent;
        /// <inheritdoc/>
        public string? FooterClass => _options.FooterClass;
        /// <inheritdoc/>
        public DragMode DragMode => _options.DefaultDragMode;
        /// <inheritdoc/>
        public bool Draggable => _options.DefaultDraggable;
        /// <inheritdoc/>
        public string? OutlineClass => _options.OutlineClass;
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
