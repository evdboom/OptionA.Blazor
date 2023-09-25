using System.Text.Json.Serialization;

namespace OptionA.Blazor.Blog.Builder
{
    /// <summary>
    /// Types of attributes to set in builder parts
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [Flags]
    public enum AttributeTypes
    {
        /// <summary>
        /// No attributes
        /// </summary>
        None = 0,
        /// <summary>
        /// class attribute
        /// </summary>
        Class = 1,
        /// <summary>
        /// class attribute for element containers
        /// </summary>
        ContainerClass = 2,        
        /// <summary>
        /// class attribute for labels
        /// </summary>
        LabelClass = 4,
        /// <summary>
        /// Content for buttons etc
        /// </summary>
        Content = 8,
        /// <summary>
        /// Content for labels
        /// </summary>
        Label = 16,
        /// <summary>
        /// placeholders for input
        /// </summary>
        Placeholder = 32,
        /// <summary>
        /// title attribute
        /// </summary>
        Title = 64,
        /// <summary>
        /// class attribute for lists of items (for instance tags)
        /// </summary>
        GroupClass = 128,
        /// <summary>
        /// class attribute for lists of items, inside group (for instance tags)
        /// </summary>
        InnerGroupClass = 256,
        /// <summary>
        /// button for adding items
        /// </summary>
        AddButton = 512,
        /// <summary>
        /// button for removing items
        /// </summary>
        RemoveButton = 1024,
        /// <summary>
        /// class attribute for div around extra properties of component
        /// </summary>
        ExtraPropertiesClass = 2048,
    }
}
