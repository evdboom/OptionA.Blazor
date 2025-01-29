namespace OptionA.Blazor.Storage;

/// <summary>
/// For the file pickers, which types are accepted
/// </summary>
public class FileAccept
{
    /// <summary>
    /// File accept for text files
    /// </summary>
    public static readonly FileAccept TextFile = new("Text file", "text/plain", ".txt");
    /// <summary>
    /// File accept for json files
    /// </summary>
    public static readonly FileAccept JsonFile = new("JSON", "application/json", ".json");
    /// <summary>
    /// File accept for images (png, gif, jpg)
    /// </summary>
    public static readonly FileAccept Images = new("Images", "image/*", ".png", ".gif", ".jpeg", ".jpg");

    /// <summary>
    /// Constructor with description
    /// </summary>
    /// <param name="description"></param>
    /// <param name="mimeType"></param>
    /// <param name="extensions"></param>
    public FileAccept(string? description, string mimeType, params string[] extensions)
    {
        Description = description;
        MimeType = mimeType;
        Extensions = extensions;
    }

    /// <summary>
    /// Constructor without description
    /// </summary>
    /// <param name="mimeType"></param>
    /// <param name="extensions"></param>
    public FileAccept(string mimeType, params string[] extensions) : this(null, mimeType, extensions)
    {

    }


    /// <summary>
    /// Optional description for the type
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Mimetype to be accepted, for instance 'text/plain' or 'image/*'
    /// </summary>
    public string MimeType { get; set; }
    /// <summary>
    /// Extensions to allow
    /// </summary>
    public string[] Extensions { get; set; }
}
