using System.Text.Json.Serialization;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Size for the headers
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum HeaderSize
    {
        /// <summary>
        /// &lt;h1&gt; tag
        /// </summary>
        One = 1,
        /// <summary>
        /// &lt;h2&gt; tag
        /// </summary>
        Two = 2,
        /// <summary>
        /// &lt;h3&gt; tag
        /// </summary>
        Three = 3,
        /// <summary>
        /// &lt;h4&gt; tag
        /// </summary>
        Four = 4,
        /// <summary>
        /// &lt;h5&gt; tag
        /// </summary>
        Five = 5,
        /// <summary>
        /// &lt;h6&gt; tag
        /// </summary>
        Six = 6
    }
}
