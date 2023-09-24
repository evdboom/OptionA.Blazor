using System.Text.Json.Serialization;

namespace OptionA.Blazor.Blog.Builder
{
    /// <summary>
    /// Type for all builder parts
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BuilderType
    {
        /// <summary>
        /// Title of post
        /// </summary>
        Title,
        /// <summary>
        /// Subtitle of post
        /// </summary>
        Subtitle,
        /// <summary>
        /// Date of post
        /// </summary>
        Date,
        /// <summary>
        /// A tag
        /// </summary>
        Tag,
        /// <summary>
        /// Container around tags
        /// </summary>
        TagContainer,
        /// <summary>
        /// Add tag button
        /// </summary>
        AddTagButton,
        /// <summary>
        /// Remove tag button
        /// </summary>
        RemoveTagButton,
        /// <summary>
        /// Button for adding a post
        /// </summary>
        AddPostButton,
        /// <summary>
        /// Save post button
        /// </summary>
        SavePostButton

    }
}
