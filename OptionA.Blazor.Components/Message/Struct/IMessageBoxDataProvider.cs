namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Interface for adding base classes to messagebox components
    /// </summary>
    public interface IMessageBoxDataProvider
    {
        /// <summary>
        /// Gets the default classes for the message container
        /// </summary>
        string ContainerClass { get; }
        /// <summary>
        /// Gets the default location for the messagebox
        /// </summary>
        /// <returns></returns>
        Location DefaultLocation { get; }
        /// <summary>
        /// Gets the default z-index for the messagebox
        /// </summary>
        int DefaultZIndex { get; }
        /// <summary>
        /// If true, will show the remaining open time for the message, or if the message does not have a timeout, will show the time the message was added
        /// </summary>
        bool ShowTime { get; }
        /// <summary>
        /// Content for the close button if dismissable
        /// </summary>
        string? CloseButtonContent { get; }
        /// <summary>
        /// Class to add to the content of the message
        /// </summary>
        string? ContentClass { get; }
        /// <summary>
        /// Default class to add to the body of the message
        /// </summary>
        string? BodyClass { get; }
        /// <summary>
        /// Default class to add to the header of the message
        /// </summary>
        string? HeaderClass { get; }
        /// <summary>
        /// Gets the classes for the close button of a message of a certain type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        string GetCloseButtonClasses(MessageType type);
        /// <summary>
        /// Gets the default classes for the message of a certain type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        string GetMessageClasses(MessageType type);
        /// <summary>
        /// Gets the dismissable state for the given message type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool GetDefaultDismissable(MessageType type);

        /// <summary>
        /// Gets the timeout if not supplied for the given message type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int GetDefaultTimeout(MessageType type);
    }
}
