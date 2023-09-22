namespace OptionA.Blazor.Blog.Text.Parser
{
    /// <summary>
    /// Enum for different markers
    /// </summary>
    public enum MarkerType
    {
        /// <summary>
        /// No marker
        /// </summary>
        None,
        /// <summary>
        /// Marker to tell text after should be bold
        /// </summary>
        Bold,
        /// <summary>
        /// Marker to tell text after shoud be italic
        /// </summary>
        Italic,
        /// <summary>
        /// Marker to tell a link is starting
        /// </summary>
        Link,
        /// <summary>
        /// Marker to tell there is a linebreak
        /// </summary>
        Linebreak
    }
}
