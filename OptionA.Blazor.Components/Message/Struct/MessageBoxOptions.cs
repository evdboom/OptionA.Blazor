using Microsoft.Extensions.Options;

namespace OptionA.Blazor.Components;

/// <summary>
/// Options to set for messagebox components
/// </summary>
public class MessageBoxOptions
{
    /// <summary>
    /// Timeout if not set and not in dictionary, default is 5000 milliseconds
    /// </summary>
    public int DefaultTimeOut { get; set; } = 5000;
    /// <summary>
    /// Dismissable if not set and not in dictionary, default is true
    /// </summary>
    public bool DefaultDismissable { get; set; } = true;
    /// <summary>
    /// Default location for the messagebox if not set in the parameter. Default is top right
    /// </summary>
    public Location DefaultLocation { get; set; } = Location.Top | Location.Right;
    /// <summary>
    /// Default z-index for the messagebox, default is 1000
    /// </summary>
    public int DefaultZIndex { get; set; } = 1000;
    /// <summary>
    /// If true, will show the remaining open time for the message, or if the message does not have a timeout, will show the time the message was added
    /// </summary>
    public bool ShowTime { get; set; }
    /// <summary>
    /// Class to add to the close button if dismissable of not set per message type
    /// </summary>
    public string? DefaultCloseButtonClass { get; set; }
    /// <summary>
    /// Content for the close button if dismissable
    /// </summary>
    public string? CloseButtonContent { get; set; }
    /// <summary>
    /// Class to add to the header of the message
    /// </summary>
    public string? HeaderClass { get; set; }
    /// <summary>
    /// Class to add to the content of the message
    /// </summary>
    public string? ContentClass { get; set; }
    /// <summary>
    /// Classes for the close button (if dismissable), per message type
    /// </summary>
    public Dictionary<MessageType, string>? CloseButtonClasses { get; set; }
    /// <summary>
    /// Timeout per message type
    /// </summary>
    public Dictionary<MessageType, int>? TimeOuts { get; set; }
    /// <summary>
    /// Dismissable per message type
    /// </summary>
    public Dictionary<MessageType, bool>? Dismissable { get; set; }
    /// <summary>
    /// Messageclasses per message type
    /// </summary>
    public Dictionary<MessageType, string>? MessageClasses { get;  set; }
    /// <summary>
    /// Message class if not set per message type
    /// </summary>
    public string? DefaultMessageClasses { get; set; }
    /// <summary>
    /// Default classes to add to messagebox container
    /// </summary>
    public string? ContainerClass { get; set; }
    /// <summary>
    /// Default class to add to the body of the message
    /// </summary>
    public string? BodyClass { get; set; }
}
