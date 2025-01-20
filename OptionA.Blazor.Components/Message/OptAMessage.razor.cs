using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components.Message.Struct;

namespace OptionA.Blazor.Components;

/// <summary>
/// Message component for displaying toast like messages
/// </summary>
public partial class OptAMessage : IDisposable
{
    private bool _disposed;

    /// <summary>
    /// Message to display
    /// </summary>
    [Parameter]
    public OpenMessage? Message { get; set; }
    /// <summary>
    /// Invoked if user clicked close (if message is dismissable
    /// </summary>
    [Parameter]
    public EventCallback<OpenMessage> MessageClosed { get; set; }
    /// <summary>
    /// Display time on the message
    /// </summary>
    [Parameter]
    public bool? ShowTime { get; set; }

    [Inject]
    private IMessageBoxDataProvider DataProvider { get; set; } = null!;

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        if (Message is not null)
        {
            Message.TimeUpdated += UpdateTime;
        }
    }

    private void UpdateTime(object? sender, string e)
    {
        InvokeAsync(StateHasChanged);
    }

    private Dictionary<string, object?> GetMessageAttributes()
    {
        if (Message is null)
        {
            return [];
        }

        var result = GetAttributes();
        result["opta-message"] = true;
        if (TryGetClasses(DataProvider.GetMessageClasses(Message.Message.Type), out var classes))
        {
            result["class"] = classes;
        }

        return result;
    }

    private Dictionary<string, object?> GetHeaderAttributes()
    {
        if (Message is null)
        {
            return [];
        }

        var result = new Dictionary<string, object?>();
        if (!string.IsNullOrEmpty(DataProvider.HeaderClass))
        {
            result["class"] = DataProvider.HeaderClass;
        }

        return result;
    }

    private Dictionary<string, object?> GetContentAttributes()
    {
        var result = new Dictionary<string, object?>();
        if (!string.IsNullOrEmpty(DataProvider.ContentClass))
        {
            result["class"] = DataProvider.ContentClass;
        }

        return result;
    }

    private Dictionary<string, object?> GetBodyAttributes()
    {
        var result = new Dictionary<string, object?>();
        if (!string.IsNullOrEmpty(DataProvider.BodyClass))
        {
            result["class"] = DataProvider.BodyClass;
        }

        return result;
    }

    private Dictionary<string, object?> GetCloseButtonAttributes()
    {
        if (Message is null)
        {
            return [];
        }

        var result = new Dictionary<string, object?>
        {
            ["type"] = "button"
        };

        var closeButtonClass = DataProvider.GetCloseButtonClasses(Message.Message.Type);
        if (!string.IsNullOrEmpty(closeButtonClass))
        {
            result["class"] = closeButtonClass;
        }

        return result;
    }

    private async Task CloseMessage()
    {
        if (Message is null || !(Message.Message.Dismissable ?? true))
        {
            return;
        }

        await MessageClosed.InvokeAsync(Message);
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

        if (Message is not null)
        {                
            Message.TimeUpdated -= UpdateTime;
        }
    }
}