using System.Text.Json.Serialization;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Supported code languages
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CodeLanguage
    {
        /// <summary>
        /// Unknown/Other language
        /// </summary>
        Other,
        /// <summary>
        /// c#
        /// </summary>
        CSharp,
        /// <summary>
        /// Html
        /// </summary>
        Html,
        /// <summary>
        /// Javascript or js
        /// </summary>
        Javascript
    }
}
