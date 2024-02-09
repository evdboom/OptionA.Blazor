namespace OptionA.Blazor.Blog.Code.Parsers
{
    /// <summary>
    /// Types of word, depending on format
    /// </summary>
    public enum WordType
    {
        /// <summary>
        /// Something else
        /// </summary>
        Other,
        /// <summary>
        /// Regular string
        /// </summary>
        String,
        /// <summary>
        /// String which might have variables interpolated
        /// </summary>
        Interpolated,
        /// <summary>
        /// Raw string type (currently unsupported)
        /// </summary>
        Raw,
        /// <summary>
        /// Start of a comment
        /// </summary>
        Comment,
        /// <summary>
        /// Word is a marker
        /// </summary>
        Marker,
        /// <summary>
        /// Word is a new line
        /// </summary>
        NewLine
    }
}
