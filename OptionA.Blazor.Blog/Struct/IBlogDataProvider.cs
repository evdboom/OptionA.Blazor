namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Provides default classes for blog components
    /// </summary>
    public interface IBlogDataProvider
    {
        /// <summary>
        /// Gets the default classes for the given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<string> DefaultClassesForType(ContentType type);
    }
}
