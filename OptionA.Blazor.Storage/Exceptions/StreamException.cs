namespace OptionA.Blazor.Storage.Exceptions;

/// <summary>
/// Exception for stream related errors
/// </summary>
/// <remarks>
/// Creates a new <see cref="StreamException"/> with the given message
/// </remarks>
/// <param name="message"></param>
public class StreamException(string message) : Exception(message)
{
    /// <summary>
    /// The code for when a stream is not found
    /// </summary>
    public const int StreamNotFoundExceptionCode = 404;
    /// <summary>
    /// Creates a new <see cref="StreamException"/> for when a stream is not found
    /// </summary>
    /// <param name="fileHandle"></param>
    /// <returns></returns>
    public static StreamException NewStreamNotFoundException(string fileHandle)
    {
        return new StreamException($"No stream found for file {fileHandle}")
        {
            HResult = StreamNotFoundExceptionCode
        };
    }
}
