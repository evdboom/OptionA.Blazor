namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Message to be shown
    /// </summary>
    public class MessageItem
    {
        /// <summary>
        /// Time in millisecond before closing this message, if not supplied will use the default set by configuration. set to <see cref="Timeout.Infinite"/> to keep message open untill dismissed
        /// </summary>
        public int? Timeout { get; set; }
        /// <summary>
        /// Optional title for the message
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Message to display
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// Type of message
        /// </summary>
        public MessageType Type { get; set; }
        /// <summary>
        /// True if user can close the message before timeout, otherwise false. If not set, will use the default from the configuration  Note: messages with timeout set to <see cref="Timeout.Infinite"/> can always be closed
        /// </summary>
        public bool? Dismissable { get; set; }
    }
}
