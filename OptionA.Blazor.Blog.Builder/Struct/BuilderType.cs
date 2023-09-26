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
        /// input type text element
        /// </summary>
        TextInput,
        /// <summary>
        /// textarea element
        /// </summary>
        TextAreaInput,
        /// <summary>
        /// input element type data
        /// </summary>
        DateInput,
        /// <summary>
        /// select element
        /// </summary>
        SelectInput,
        /// <summary>
        /// Blog component
        /// </summary>
        Component,
        /// <summary>
        /// Extra properties of component
        /// </summary>
        ExtraProperties,
        /// <summary>
        /// Label of input
        /// </summary>
        Label,
        /// <summary>
        /// Content of component
        /// </summary>
        ComponentContent,
        /// <summary>
        /// Title of component
        /// </summary>
        ComponentTitle,
        /// <summary>
        /// Remove the item
        /// </summary>
        RemoveButton,
        /// <summary>
        /// Move content up in list
        /// </summary>
        MoveUpButton,
        /// <summary>
        /// Move content down in list
        /// </summary>
        MoveDownButton
    }
}
