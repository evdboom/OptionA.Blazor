namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Interface for builders that support generic child builders, for instance <see cref="BlockBuilder{Parent}"/>
    /// </summary>
    public interface IParentBuilder : IContentParentBuilder
    {
    }
}
