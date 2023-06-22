using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// The component types to render
    /// </summary>
    public enum ComponentType
    {
        /// <summary>
        /// An image component, resulting in a &lt;img&gt; tag
        /// </summary>
        Image,
        /// <summary>
        /// A block of code, text inside the code block is rendered (default in Visual Studio dark theme colors) for better readability, (uses <see cref="Block"/> as a backing component)
        /// </summary>
        Code,
        /// <summary>
        /// A list of items, resulting in a &lt;ul&gt; or &lt;ol&gt; tag
        /// </summary>
        List,
        /// <summary>
        /// A table of items, resulting in a &lt;table&gt; tag
        /// </summary>
        Table,
        /// <summary>
        /// A header resulting in (depending on size) a &lt;h1&gt; tag (or &lt;h2&gt; for size 2, etc.)
        /// </summary>
        Header,
        /// <summary>
        /// A link, resulting in a &lt;a&gt; tag
        /// </summary>
        Link,
        /// <summary>
        /// A date to display, uses the <see cref="Block"/> as backing for display
        /// </summary>
        Date,
        /// <summary>
        /// A row, used by <see cref="Table"/> and <see cref="List"/> does not get rendered by itself by default
        /// </summary>
        Row,
        /// <summary>
        /// A 'Block' of content, depending on the <see cref="BlockType"/> resulting in a &lt;div&gt;, &lt;span&gt;, &lt;p&gt;or no tag
        /// </summary>
        Block,
        /// <summary>
        /// A Line, resulting in a &lt;hr&gt; tag, currently not supporting any styling
        /// </summary>
        Line,
        /// <summary>
        /// An Icon resulting in a &lt;i&gt; tag, set the icon through classes
        /// </summary>
        Icon,
        /// <summary>
        /// Custom type, to be used with your own content as <see cref="RenderFragment"/>
        /// </summary>
        Custom
    }
}
