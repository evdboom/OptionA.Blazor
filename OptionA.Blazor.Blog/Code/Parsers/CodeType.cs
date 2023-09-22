namespace OptionA.Blazor.Blog.Code.Parsers
{
    /// <summary>
    /// Types of code to display
    /// </summary>
    public enum CodeType
    {
        /// <summary>
        /// Generic, regular text
        /// </summary>
        Text,
        /// <summary>
        /// string, usually encompassed by " characters
        /// </summary>
        String,
        /// <summary>
        /// Method or function
        /// </summary>
        Method,
        /// <summary>
        /// Language keyword
        /// </summary>
        Keyword,
        /// <summary>
        /// Language control keyword
        /// </summary>
        ControlKeyword,
        /// <summary>
        /// Piece of comment in code
        /// </summary>
        Comment,
        /// <summary>
        /// Attribute of an element
        /// </summary>
        Attribute,
        /// <summary>
        /// Blazor component
        /// </summary>
        Component,
        /// <summary>
        /// Part of a tag, for instance the &lt; or &gt; characters
        /// </summary>
        TagDelimiter,
        /// <summary>
        /// Directive, for instance for blazor
        /// </summary>
        Directive,
        /// <summary>
        /// Language special, could be rendered as text but for escaping
        /// </summary>
        Special
    }
}
