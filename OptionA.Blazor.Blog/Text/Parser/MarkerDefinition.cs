using System.Diagnostics.CodeAnalysis;

namespace OptionA.Blazor.Blog.Text.Parser
{
    /// <summary>
    /// Definition for a marker
    /// </summary>
    public abstract class MarkerDefinition : IMarkerDefinition
    {
        /// <inheritdoc/>
        public abstract int Priority { get; }
        /// <inheritdoc/>
        public char FirstChar => Starter[0];            
        /// <inheritdoc/>
        public abstract string Starter { get; }
        /// <inheritdoc/>
        public abstract string Ender { get; }
        /// <inheritdoc/>
        public abstract MarkerType Type { get; }
        /// <inheritdoc/>
        public abstract bool IsValidForMarker(string input, [NotNullWhen(true)] out string? content);
        /// <inheritdoc/>
        public abstract IContent CreateContent(string content);
    }
}
