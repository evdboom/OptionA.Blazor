using System.Text.Json.Serialization;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Strength for margin, padding etc
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Strength
    {
        /// <summary>
        /// Default
        /// </summary>
        Inherit,
        /// <summary>
        /// Lowest strength, should result in lowest applied value
        /// </summary>
        One = 1,
        /// <summary>
        /// Should result in more then a bit, but less then avery value
        /// </summary>
        Two = 2,
        /// <summary>
        /// Should result in the average value to apply
        /// </summary>
        Three = 3,
        /// <summary>
        /// Should result in more then average but less then most value
        /// </summary>
        Four = 4,
        /// <summary>
        /// Higest strength, should result in highest applied value
        /// </summary>
        Five = 5,
        /// <summary>
        /// Lowest negative strength (only applies to margin)
        /// </summary>
        MinusOne,
        /// <summary>
        /// A bit more negative strength (only applies to margin)
        /// </summary>
        MinusTwo,
        /// <summary>
        /// Average negative strength (only applies to margin)
        /// </summary>
        MinusThree,
        /// <summary>
        /// A bit more then average negative strength (only applies to margin)
        /// </summary>
        MinusFour,
        /// <summary>
        /// Strongest negative strength (only applies to margin)
        /// </summary>
        MinusFive,
        /// <summary>
        /// Sets the margin or padding to auto
        /// </summary>
        Auto
    }
}
