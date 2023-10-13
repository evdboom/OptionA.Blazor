using System.Text.Json;

namespace OptionA.Blazor.Blog.Services
{
    /// <summary>
    /// Interface for contructing a post from json
    /// </summary>
    public interface IBuilderService
    {
        /// <summary>
        /// Creates a new post from the given Json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        public Post CreateFromJson(string json);
        /// <summary>
        /// Transforms the post to json
        /// </summary>
        /// <param name="post"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public string ToJson(Post post, JsonSerializerOptions? options = null);
    }
}
