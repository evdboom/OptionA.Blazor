namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Interface for builders that have a content child, but might nog be general (for instance <see cref="TableBuilder{Parent}"/> or <see cref="ListBuilder{Parent}"/>)
    /// </summary>
    public interface IContentParentBuilder : IBuilder        
    {
        /// <summary>
        /// The post for which the content is created
        /// </summary>
        IPost? Post { get; }
        /// <summary>
        /// Adds the content to this builder
        /// </summary>
        /// <param name="content"></param>
        void AddContent(IContent content);
    }
}
