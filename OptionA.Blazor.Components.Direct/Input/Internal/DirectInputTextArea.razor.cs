using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace OptionA.Blazor.Components.Direct.Input.Internal;

/// <summary>
/// Implementation of <see cref="Microsoft.AspNetCore.Components.Forms.InputTextArea"/> with bind to oninput
/// </summary>
public partial class DirectInputTextArea
{
    private const string EnterKey = "Enter";
    private const string ForceFocusFunction = "forceFocus";

    /// <summary>
    /// Gets the reference to the input
    /// </summary>
    public new ElementReference Element => _input;
    /// <summary>
    /// Bindmode for the input, default is <see cref="BindMode.OnInput"/>
    /// </summary>
    [Parameter]
    public BindMode? Mode { get; set; }
    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;

    private ElementReference _input;
    private BindMode InternalMode => Mode ?? BindMode.OnInput;
    private Lazy<Task<IJSObjectReference>>? _moduleTask;

    /// <summary>
    /// Set to true to enable autogrow
    /// </summary>
    [Parameter]
    public bool AutoGrow { get; set; }

    private Dictionary<string, object> Attributes
    {
        get
        {
            var result = AdditionalAttributes?.ToDictionary(d => d.Key, d => d.Value) ?? new();
            return result;
        }
    }

    

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        _moduleTask = new(() => JsRuntime.InvokeAsync<IJSObjectReference>(
            "import",
            "./_content/OptionA.Blazor.Components.Direct/Input/Internal/DirectInputTextArea.razor.js").AsTask());
    }

    private async Task ForceBindOnEnter(KeyboardEventArgs args)
    {
        if (args.Key == EnterKey)
        {
            var module = await _moduleTask!.Value;
            await module.InvokeVoidAsync(ForceFocusFunction, _input);
        }
    }

    private Dictionary<string, object?> GetContainerAttributes()
    {
        var result = new Dictionary<string, object?>()
        {
            ["auto-grow"] = true
        };

        return result;
    }
}
