using System.Text.Json.Serialization;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Supported languages for code (styling)
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CodeLanguage
    {
        /// <summary>
        /// C#
        /// </summary>
        CSharp,
        /// <summary>
        /// Js
        /// </summary>
        Javascript,
        /// <summary>
        /// Html
        /// </summary>
        Html,
        /// <summary>
        /// Rust
        /// </summary>
        Rust,
        /// <summary>
        /// Something else
        /// </summary>
        Other
    }
}
