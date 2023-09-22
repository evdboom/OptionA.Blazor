using System.Text.Json;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Interface for content to be rendered inside a post
    /// </summary>
    public interface IContent
    {
        /// <summary>
        /// Additional classes to add to this specific content
        /// </summary>
        List<string> AdditionalClasses { get; }
        /// <summary>
        /// Classes to remove from the defaults provided by the <see cref="IBlogDataProvider"/>
        /// </summary>
        List<string> RemovedClasses { get; }
        /// <summary>
        /// Attributes to add to this specific content
        /// </summary>
        Dictionary<string, object?> Attributes { get; }
        /// <summary>
        /// Type of content
        /// </summary>
        ContentType Type { get; }
    }
}
