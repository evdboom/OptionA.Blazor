﻿namespace OptionA.Blazor.Blog.Text.Parser;

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
    Linebreak,
    /// <summary>
    /// Marker to tell there is an icon, specify icon class inside the tags
    /// </summary>
    Icon,
    /// <summary>
    /// Marker to tell the next part is a citation
    /// </summary>
    Cite
}
