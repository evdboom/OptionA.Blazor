namespace OptionA.Blazor.Blog.Services
{
    /// <summary>
    /// Interface for contructing a post from json
    /// </summary>
    public interface IBuilderService
    {
        /// <summary>
        /// Build a post from the given json string
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public Post CreateFromJson(string json);
    }
}
