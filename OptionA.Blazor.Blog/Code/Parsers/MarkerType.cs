namespace OptionA.Blazor.Blog.Code.Parsers
{
    /// <summary>
    /// Enum for different code markers
    /// </summary>
    [Flags]
    public enum MarkerType
    {
        /// <summary>
        /// No marker
        /// </summary>
        None = 0,
        /// <summary>
        /// Marker to tell the code after should be marked as selected
        /// </summary>
        Selection = 1
    }
}
