namespace OptionA.Blazor.Components;

/// <summary>
/// Service to add event for the OptAMessageBox to pick up
/// </summary>
public interface IMessageService
{
    /// <summary>
    /// Triggered whenever a message is added, primarily for use by OptAMessageBox component to start display
    /// </summary>
    event EventHandler<MessageItem>? MessageAdded;

    /// <summary>
    /// Add a message to display
    /// </summary>
    /// <param name="message"></param>
    void AddMessage(MessageItem message);        
}
