using System.Diagnostics.CodeAnalysis;

namespace OptionA.Blazor.Blog.Builder;

/// <summary>
/// Dataprovider for building blogs, providing default classes and attributes
/// </summary>
public interface IBlogBuilderDataProvider
{
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
    /// <param name="defaultAttributes"></param>
    /// <returns></returns>
    Dictionary<string, object?> GetAttributes(BuilderType type, Dictionary<string, object?>? defaultAttributes = null);
    /// <summary>
    /// Gets the content for the given builder type
    /// </summary>
    /// <param name="type"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    IContent? GetContent(BuilderType type, string? content);
    /// <summary>
    /// Creates a new content for the given content type
    /// </summary>
    /// <param name="contentType"></param>
    /// <returns></returns>
    IContent CreateContentForType(ContentType contentType);
    /// <summary>
    /// Creates content for a button on the content bar
    /// </summary>
    /// <param name="contentType"></param>
    /// <param name="defaultContent"></param>
    /// <returns></returns>
    IContent? ContentForContentButton(ContentType contentType, string? defaultContent);

}
