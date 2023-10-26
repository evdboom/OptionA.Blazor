namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Provides default classes and behavior for splitter
    /// </summary>
    public interface ISplitterDataProvider
    {
        /// <summary>
        /// Default dragmode for splitters
        /// </summary>
        public DragMode DragMode { get; }
        /// <summary>
        /// Content to set inside dragbar
        /// </summary>
        public string? DragBarContent { get; }
        /// <summary>
        /// Classes to add to the dragbar
        /// </summary>
        public string? DragBarClass { get; }
        /// <summary>
        /// Class to add to outline in case of dragmode outline
        /// </summary>
        public string? OutlineClass { get; }
    }
}
