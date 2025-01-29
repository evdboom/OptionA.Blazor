namespace OptionA.Blazor.Storage
{
    /// <summary>
    /// Represents a file handle for a file on disk
    /// </summary>
    public class FileHandle
    {
        /// <summary>
        /// The key for the file handle
        /// </summary>
        public string Key { get; set; } = string.Empty;
        /// <summary>
        /// The name of the file
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
