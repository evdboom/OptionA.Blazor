using System.Text.Json.Serialization;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// The various styles that can be applied to a component, multiple styles can be set
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [Flags]
    public enum Style
    {
        /// <summary>
        /// Tells the builder to use the style from the parent
        /// </summary>
        Inherit = 0,
        /// <summary>
        /// No (additional) styles set for the component
        /// </summary>
        Normal = 1,
        /// <summary>
        /// Render text thin
        /// </summary>
        Thin = 2,
        /// <summary>
        /// Render text bold
        /// </summary>
        Bold = 4,
        /// <summary>
        /// Render text italic
        /// </summary>
        Italic = 8,
        /// <summary>
        /// Underline text
        /// </summary>
        Underline = 16,
        /// <summary>
        /// Strike through text
        /// </summary>
        StrikeThrough = 32,
        /// <summary>
        /// Make text lowercase
        /// </summary>
        LowerCase = 64,
        /// <summary>
        /// Make text uppercase
        /// </summary>
        UpperCase = 128,
        /// <summary>
        /// Use a monospace font
        /// </summary>
        Monospace = 256,
        /// <summary>
        /// Make sure to keep any set whitespace
        /// </summary>
        KeepWhiteSpace = 1024,
        /// <summary>
        /// Sets the text-decoration to none
        /// </summary>
        NoDecoration = 2048,
    }
}
