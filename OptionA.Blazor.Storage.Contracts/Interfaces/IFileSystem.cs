using System.IO;
using System.Threading.Tasks;

namespace OptionA.Blazor.Storage;

/// <summary>
/// Interface for file system access using the browser
/// </summary>
public interface IFileSystem
{
    /// <summary>
    /// The name of the database
    /// </summary>
    public const string FileSystemDatabase = "optionafiles";
    /// <summary>
    /// The name of the object store in the database
    /// </summary>
    public const string ObjectStoreName = "files";
    /// <summary>
    /// The name of the index in the object store
    /// </summary>
    public const string IndexName = "idx_name";

    /// <summary>
    /// Reads files from disk and stores the corresponding file handles. Use OpenStreamAsync to get the underlying stream;
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    Task<FileHandle[]> OpenFilesAsync(FilePickerOptions? options = null);
    /// <summary>
    /// Reads a directory from disk and stores the corresponding file handles. Use OpenStreamAsync to get the underlying stream;
    /// </summary>
    /// <returns></returns>
    Task<FileHandle[]> OpenDirectoryAsync();
    /// <summary>
    /// Opens a read stream for the given file handle. Returns null if the file handle is not found or the filesize = 0.
    /// </summary>
    /// <param name="handle"></param>
    /// <returns></returns>
    Task<Stream?> OpenStreamAsync(string handle);
    /// <summary>
    /// Saves the given stream to disk and returns the file handle. Returns null if the saving failed.
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    Task<FileHandle?> SaveFileAsync(Stream stream, FilePickerOptions? options = null);
}
