using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components;

namespace OptionA.Blazor.Test.Pages
{
    public partial class MessageBox
    {
        [Inject]
        IMessageService MessageService { get; set; } = null!;

        private Location _location;
        private MessageType _type;
        private bool _dismissable;
        private bool _withTitle;
        private bool _withTimeout;
        private bool _showTime;

        private void AddMessage()
        {
            var message = new MessageItem
            {
                Title = _withTitle
                    ? "Test Title"
                    : null,
                Content = "Test me!",
                Type = _type,
                Dismissable = _dismissable,
                Timeout = _withTimeout
                    ? null
                    : Timeout.Infinite
            };
            MessageService.AddMessage(message);
        }

        // change the location clockwise, corners require 2 location flags set
        private void ChangeLocation()
        {
            _location = _location switch
            {
                Location.Unset => Location.Right,
                Location.Right => Location.Bottom | Location.Right,
                Location.Bottom | Location.Right => Location.Bottom,
                Location.Bottom => Location.Left | Location.Bottom,
                Location.Left | Location.Bottom => Location.Left,
                Location.Left => Location.Top | Location.Left,
                Location.Top | Location.Left => Location.Top,
                Location.Top => Location.Right | Location.Top,
                _ => Location.Unset
            };
        }

        //change the message type, moving to the next type
        private void ChangeType()
        {
            _type = _type switch
            {
                MessageType.Info => MessageType.Success,
                MessageType.Success => MessageType.Warning,
                MessageType.Warning => MessageType.Error,
                MessageType.Error => MessageType.Info,
                _ => MessageType.Info
            };
        }
    }
}
