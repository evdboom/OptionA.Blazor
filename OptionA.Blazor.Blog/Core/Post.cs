using System.Text.Json;

namespace OptionA.Blazor.Blog;

/// <summary>
/// Class representing a blog post
/// </summary>
public class Post
{
    /// <summary>
    /// Tags describing content of the post
    /// </summary>
    public List<string> Tags { get; } = new List<string>();
    /// <summary>
    /// Content of the post
    /// </summary>
    public List<IContent> Content { get; } = new List<IContent>();
    /// <summary>
    /// Date of the post
    /// </summary>
    public DateTime Date { get; set; }
    /// <summary>
    /// Title of the post
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// Subtitle of the post
    /// </summary>
    public string? Subtitle { get; set; }
}
