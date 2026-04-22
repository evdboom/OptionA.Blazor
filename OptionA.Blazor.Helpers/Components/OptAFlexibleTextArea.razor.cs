using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components;
using OptionA.Blazor.Helpers.Contracts;

namespace OptionA.Blazor.Helpers.Components;

/// <summary>
/// A text area with build in autogrow checkbox.
/// </summary>
public partial class OptAFlexibleTextArea
{
    /// <summary>
    /// Gets the <see cref="ElementReference"/> for the input element.
    /// </summary>
    public ElementReference Element => _input?.Element ?? throw new InvalidOperationException("Element is not available until after the component has been rendered");

    /// <summary>
    /// Set required bindmode for the input.
    /// </summary>
    [Parameter]
    public BindMode? Mode { get; set; }

    /// <summary>
    /// Set to false to hide the autogrow option (default is true).
    /// </summary>
    [Parameter]
    public bool? DisplayAutoGrow { get; set; }

    /// <summary>
    /// Set to true to enable autogrow initially.
    /// </summary>
    [Parameter]
    public bool? InitialAutoGrow { get; set; }

    /// <summary>
    /// Text of the text area.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// Occurs when value changes.
    /// </summary>
    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    /// <summary>
    /// Attributes to set.
    /// </summary>
    [Parameter]
    public IDictionary<string, object>? Attributes { get; set; }

    [Inject]
    private IComponentStyleProvider StyleProvider { get; set; } = null!;

    private string? InternalValue
    {
        get => Value;
        set
        {
            if (!string.Equals(Value, value))
            {
                Value = value;
                if (ValueChanged.HasDelegate)
                {
                    ValueChanged.InvokeAsync(Value);
                }
            }
        }
    }

    private bool _showAutoGrow = true;
    private bool _autoGrow;
    private string _id = string.Empty;
    private OptAInputTextArea? _input;

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        _id = $"OptATA-{Guid.NewGuid()}";
    }

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        if (InitialAutoGrow.HasValue)
        {
            _autoGrow = InitialAutoGrow.Value;
        }

        if (DisplayAutoGrow.HasValue)
        {
            _showAutoGrow = DisplayAutoGrow.Value;
        }
    }

    private Dictionary<string, object?> GetCheckBoxAttributes(string id)
    {
        var defaultAttributes = new Dictionary<string, object?>
        {
            ["id"] = id
        };

        return StyleProvider.GetAttributes(ComponentElementType.CheckboxInput, defaultAttributes);
    }

    private Dictionary<string, object?> GetLabelAttributes(string id)
    {
        var defaultAttributes = new Dictionary<string, object?>
        {
            ["for"] = id
        };

        return StyleProvider.GetAttributes(ComponentElementType.Label, defaultAttributes);
    }

    private Dictionary<string, object?> GetCheckboxContainerAttributes()
    {
        var defaultAttributes = new Dictionary<string, object?>
        {
            ["opta-checkbox-container"] = true,
        };

        return StyleProvider.GetAttributes(ComponentElementType.TextAreaAutoGrowContainer, defaultAttributes);
    }

    private Dictionary<string, object?> GetFlexibleTextAreaAttributes()
    {
        var defaultAttributes = new Dictionary<string, object?>
        {
            ["opta-flexible-text-area"] = true,
        };

        return StyleProvider.GetAttributes(ComponentElementType.FlexibleTextArea, defaultAttributes);
    }
}
