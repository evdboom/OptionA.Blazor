using System.Text.Json;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Base class for post content
    /// </summary>
    public abstract class Content : IContent
    {
        /// <inheritdoc/>
        public List<string> AdditionalClasses { get; } = new List<string>();
        /// <inheritdoc/>
        public List<string> RemovedClasses { get; } = new List<string>();
        /// <inheritdoc/>
        public virtual Dictionary<string, object?> Attributes { get; } = new Dictionary<string, object?>();
        /// <inheritdoc/>
        public abstract ContentType Type { get; }
    }
}
