using LandaPacs.Shared.Interfaces;

namespace LandaPacs.Storage.Interfaces
{
    public interface IFileService : IAsyncMessageInitializer
    {
        public const string ObjectStoreName = "files";
        public const string IndexName = "idx_name";

        /// <summary>
        /// Reads files from disk and stores the corresponding file handles. Use OpenStreamAsync to get the underlying stream;
        /// </summary>
        /// <returns></returns>
        Task<string[]> OpenFilesAsync();
        /// <summary>
        /// Reads a directory from disk and stores the corresponding file handles. Use OpenStreamAsync to get the underlying stream;
        /// </summary>
        /// <returns></returns>
        Task<string[]> OpenDirectoryAsync();
        /// <summary>
        /// Opens a read stream for the given file handle.
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        /// <exception cref="StreamException">When no stream is found for the given handle</exception>
        Task<Stream> OpenStreamAsync(string handle);
    }
}
