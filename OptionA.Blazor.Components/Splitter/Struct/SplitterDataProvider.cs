namespace OptionA.Blazor.Components.Splitter.Struct
{
    /// <summary>
    /// Implementation of <see cref="ISplitterDataProvider"/>
    /// </summary>
    public class SplitterDataProvider : ISplitterDataProvider
    {
        private readonly SplitterOptions _options;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public SplitterDataProvider(Action<SplitterOptions>? configuration = null)
        {
            _options = new SplitterOptions();
            configuration?.Invoke(_options);
        }

        /// <inheritdoc/>
        public DragMode DragMode => _options.DragMode;
        /// <inheritdoc/>
        public string? DragBarContent => _options.DragBarContent;
        /// <inheritdoc/>
        public string? DragBarClass => _options.DragBarClass;
        /// <inheritdoc/>
        public string? OutlineClass => _options.OutlineClass;
    }
}
