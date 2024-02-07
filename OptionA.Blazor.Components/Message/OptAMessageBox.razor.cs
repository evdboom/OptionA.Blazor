using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components.Message.Struct;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// Message box component to display messages in the UI    
    /// </summary>
    public partial class OptAMessageBox : IDisposable
    {
        private readonly List<OpenMessage> _openMessages = [];

        private bool _disposed;

        /// <summary>
        /// Attributes to be added to the messages inside the container
        /// </summary>
        [Parameter]
        public Dictionary<string, object?>? MessageAttributes {  get; set; }
        /// <summary>
        /// Locations for the messagebox. Add 2 adjacent for a corner (for example: <see cref="Location.Top"/> | <see cref="Location.Right"/> for top right
        /// </summary>
        [Parameter]
        public Location Location { get; set; }
        /// <summary>
        /// Display time on the messages
        /// </summary>
        [Parameter]
        public bool? ShowTime { get; set; }

        [Inject]
        private IMessageBoxDataProvider DataProvider { get; set; } = null!;
        [Inject]
        private IMessageService Service { get; set; } = null!;

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            Service.MessageAdded += AddMessage;
        }

        private Dictionary<string, object?> GetContainerAttributes()
        {
            var result = GetAttributes();
            result["opta-message-box"] = true;
            if (TryGetClasses(DataProvider.ContainerClass, out var classes))
            {
                result["class"] = classes;
            }

            result["location"] = GetLocation();
            result["style"] = $"--opta-messagebox-zindex:{DataProvider.DefaultZIndex};";

            return result;
        }

        private string GetLocation()
        {
            var location = Location == Location.Unset
                ? DataProvider.DefaultLocation
                : Location;
            
            if (location == Location.Unset)
            {
                return "topright";
            }

            var result = string.Empty;
            if (location.HasFlag(Location.Top))
            {
                result += "top";
            }
            else if (location.HasFlag(Location.Bottom))
            {
                result += "bottom";
            }

            if (location.HasFlag(Location.Left))
            {
                result += "left";
            }
            else if (location.HasFlag(Location.Right))
            {
                result += "right";
            }

            return result;
        }


        private void AddMessage(object? sender, MessageItem e)
        {
            var open = SetDefaults(e);
            _openMessages.Add(open);
            StateHasChanged();
        }

        private OpenMessage SetDefaults(MessageItem e)
        {
            if (!e.Timeout.HasValue)
            {
                e.Timeout = DataProvider.GetDefaultTimeout(e.Type);
            }
            if (!e.Dismissable.HasValue)
            {
                e.Dismissable = DataProvider.GetDefaultDismissable(e.Type);
            }
            if (e.Timeout == Timeout.Infinite)
            {
                e.Dismissable = true;
            }

            var open = new OpenMessage(e);            
            open.Closed += CloseMessage;

            return open;
        }

        private void CloseMessage(object? sender, OpenMessage e)
        {
            CloseMessage(e);
        }

        private void CloseMessage(OpenMessage message) 
        {
            message.Closed -= CloseMessage;
            _openMessages.Remove(message);
            message.Dispose();
            StateHasChanged();
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

            foreach(var message in _openMessages)
            {
                message.Closed -= CloseMessage;
                message.Dispose();
            }

            Service.MessageAdded -= AddMessage;
        }
    }
}