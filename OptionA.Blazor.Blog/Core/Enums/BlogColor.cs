using System.Text.Json.Serialization;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Color options for the various components
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BlogColor
    {
        /// <summary>
        /// Tells the builder to use the color set by the parent, if a component is rendered with this option, no color will be set (resulting in default page color)
        /// </summary>
        Inherit,
        /// <summary>
        /// Color used for headers
        /// </summary>
        Header,
        /// <summary>
        /// Color used for text
        /// </summary>
        Text,
        /// <summary>
        /// Color used for footers
        /// </summary>
        Subtle,
        /// <summary>
        /// Color used for quotes
        /// </summary>
        Quote,
        /// <summary>
        /// Color used for links
        /// </summary>
        Link
    }
}
