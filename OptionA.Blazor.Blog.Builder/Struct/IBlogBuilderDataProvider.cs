using System.Diagnostics.CodeAnalysis;

namespace OptionA.Blazor.Blog.Builder
{
    /// <summary>
    /// Dataprovider for building blogs, providing default classes and attributes
    /// </summary>
    public interface IBlogBuilderDataProvider
    {
        /// <summary>
        /// Class for the form the post is build in
        /// </summary>
        string? FormClass { get; }
        /// <summary>
        /// tries to get the properties for the given builder type, if set
        /// </summary>
        /// <param name="type"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        bool TryGetProperties(BuilderType type, [NotNullWhen(true)] out BuilderTypeProperties? properties);
        /// <summary>
        /// Gets the attributes for the given builder type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="attributes"></param>
        /// <param name="id"></param>
        /// <param name="forId"></param>
        /// <returns></returns>
        Dictionary<string, object?> GetAttributes(BuilderType type, AttributeTypes attributes, string? id = null, string? forId = null);
        /// <summary>
        /// Gets the content for the given builder type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="attributes"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        InlineContent? GetContent(BuilderType type, AttributeTypes attributes, string? defaultValue = null);
    }
}
