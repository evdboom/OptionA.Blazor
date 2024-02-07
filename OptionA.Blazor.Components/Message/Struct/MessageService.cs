namespace OptionA.Blazor.Components.Message.Struct
{
    /// <inheritdoc/>
    public class MessageService : IMessageService
    {
        /// <inheritdoc/>
        public event EventHandler<MessageItem>? MessageAdded;

        /// <inheritdoc/>
        public void AddMessage(MessageItem message)
        {
            MessageAdded?.Invoke(this, message);
        }
    }
}
