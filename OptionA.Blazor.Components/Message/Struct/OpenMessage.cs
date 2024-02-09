using System;

namespace OptionA.Blazor.Components.Message.Struct
{
    /// <summary>
    /// Class for displaying an open message
    /// </summary>
    public class OpenMessage : IDisposable
    {
        private readonly DateTime _openTime;
        private readonly Timer _timeTimer;

        private bool _invokePerMinute;
        private bool _disposed;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="message"></param>
        public OpenMessage(MessageItem message)
        {
            Message = message;
            
            _openTime = DateTime.UtcNow;
            if (Message.Timeout.HasValue && message.Timeout!.Value != Timeout.Infinite)
            {                
                var closeTimer = new Timer(CloseElapsed, null, Message.Timeout.Value, Timeout.Infinite);
                Time = (Message.Timeout.Value / 1000).ToString("F0");
                _timeTimer = new Timer(TimeElapsed, null, 1000, 1000);
            }
            else
            {
                Time = "Just now";
                _timeTimer = new Timer(TimeElapsed, null, 5000, 1000);
            }
            
            
        }

        /// <summary>
        /// Message to display
        /// </summary>
        public MessageItem Message { get; }
        /// <summary>
        /// Time to display
        /// </summary>
        public string? Time { get; set; }
        /// <summary>
        /// Event triggered when time should be updated
        /// </summary>
        public event EventHandler<string>? TimeUpdated;
        /// <summary>
        /// Event triggered when timer is up
        /// </summary>
        public event EventHandler<OpenMessage>? Closed;

        private void TimeElapsed(object? state)
        {
            var open = DateTime.UtcNow - _openTime;
            if (Message.Timeout.HasValue && Message.Timeout.Value != Timeout.Infinite)
            {
                var time = (Message.Timeout.Value / 1000) - open.TotalSeconds;
                Time = time.ToString("F0");
            }
            else
            {
                // return a time that displays the message has been opened for some time, if it's less then a minute show seconds, otherwise minutes
                Time = open.TotalSeconds < 60
                    ? open.TotalSeconds.ToString("F0") + " seconds ago"
                    : open.TotalMinutes.ToString("F0") + " minutes ago";

                if (open.TotalSeconds > 60 && !_invokePerMinute)
                {
                    _invokePerMinute = true;
                    _timeTimer.Change(60000, 60000);                    
                }
                
            }            

           TimeUpdated?.Invoke(this, Time);
        }

        private void CloseElapsed(object? state)
        {
            Closed?.Invoke(this, this);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(_disposed);
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposed)
        {
            if (disposed)
            {
                return;
            }

            _timeTimer.Dispose();
        }
    }
}
