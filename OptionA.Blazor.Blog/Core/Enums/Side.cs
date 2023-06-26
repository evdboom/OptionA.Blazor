using System.Text.Json.Serialization;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Sides for margin, border, padding etc.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [Flags]
    public enum Side
    {
        /// <summary>
        /// Default 
        /// </summary>
        Inherit = 0,
        /// <summary>
        /// Top side
        /// </summary>
        Top = 1,
        /// <summary>
        /// Right side or End
        /// </summary>
        Right = 2,
        /// <summary>
        /// Bottom side
        /// </summary>
        Bottom = 4,
        /// <summary>
        /// Left side or Start
        /// </summary>
        Left = 8,
        /// <summary>
        /// X sides (left and right)
        /// </summary>
        X = Right | Left,
        /// <summary>
        /// Y sides (top and bottom)
        /// </summary>
        Y = Top | Bottom,
        /// <summary>
        /// All sides
        /// </summary>
        All = X | Y,
    }
}
