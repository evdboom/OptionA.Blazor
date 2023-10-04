using System.Text.Json.Serialization;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content types to render
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ContentType
    {
        /// <summary>
        /// An image component, resulting in a &lt;img&gt; tag
        /// </summary>
        Image,
        /// <summary>
        /// A block of code, text inside the code block is rendered (default in Visual Studio dark theme colors) for better readability
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
        /// A Line, resulting in a &lt;hr&gt; tag, currently not supporting any styling
        /// </summary>
        Line,
        /// <summary>
        /// A quote, resulting in a &lt;blockquote&gt; tag encapsulated in a &lt;figure&gt; tag
        /// </summary>
        Quote,
        /// <summary>
        /// A paragraph, resulting in a &lt;p&gt; tag, styling inside supports basis Markdown
        /// </summary>
        Paragraph,
        /// <summary>
        /// Text, directly placed
        /// </summary>
        Text,
        /// <summary>
        /// A inline part, resulting in a &lt;span&gt; tag
        /// </summary>
        Inline,
        /// <summary>
        /// A block part, resulting in a &lt;div&gt; tag
        /// </summary>
        Block,
        /// <summary>
        /// A bold part, resulting in a &lt;strong&gt; tag
        /// </summary>
        Bold,
        /// <summary>
        /// Italic part, resulting in a &lt;em&gt; tag
        /// </summary>
        Italic,
        /// <summary>
        /// A link, resulting in a &lt;a&gt; tag
        /// </summary>
        Link,
        /// <summary>
        /// Linebreak resulting in a &lt;br /&gt; tag
        /// </summary>
        LineBreak,
        /// <summary>
        /// Icon resulting in a &lt;i&gt; or &lt;svg&gt; tag
        /// </summary>
        Icon,
    }
}
