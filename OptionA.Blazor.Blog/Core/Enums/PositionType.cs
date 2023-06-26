﻿using System.Text.Json.Serialization;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Position types to correctly place the various components and content
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PositionType
    {
        /// <summary>
        /// Don't set postition type
        /// </summary>
        Inherit,
        /// <summary>
        /// Position left
        /// </summary>
        Left,
        /// <summary>
        /// Position right
        /// </summary>
        Right,
        /// <summary>
        /// Position center
        /// </summary>
        Center,
    }
}
