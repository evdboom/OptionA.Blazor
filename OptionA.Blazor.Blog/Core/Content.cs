namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Base class for post content
    /// </summary>
    public abstract class Content : IContent
    {
        /// <inheritdoc/>
        public IList<string> AdditionalClasses { get; } = [];
        /// <inheritdoc/>
        public IList<string> RemovedClasses { get; } = [];
        /// <inheritdoc/>
        public virtual IDictionary<string, object?> Attributes { get; } = new Dictionary<string, object?>();
        /// <inheritdoc/>
        public abstract ContentType Type { get; }
        /// <inheritdoc/>
        public abstract bool IsInvalid { get; }
    }
}
