using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Playground;

/// <summary>
/// Renders parameter editors for a playground descriptor.
/// </summary>
public partial class OptAPlaygroundEditor
{
    /// <summary>
    /// Gets or sets the descriptor that defines which parameters are editable.
    /// </summary>
    [Parameter]
    public PlaygroundDescriptorBase? Descriptor { get; set; }

    /// <summary>
    /// Gets or sets the current playground parameter values.
    /// </summary>
    [CascadingParameter]
    public Dictionary<string, object?> CurrentParameters { get; set; } = [];

    /// <summary>
    /// Gets or sets the callback raised when a parameter value changes.
    /// </summary>
    [CascadingParameter]
    public EventCallback<(string Name, object? Value)> ValueChanged { get; set; }

    [Inject]
    private IPlaygroundDataProvider DataProvider { get; set; } = null!;

    private Dictionary<string, object?> GetEditorAttributes()
    {
        var result = GetAttributes();
        result["opta-playground-editor"] = true;

        if (TryGetClasses(DataProvider.DefaultEditorClass, out var classes))
        {
            result["class"] = classes;
        }

        return result;
    }

    private Dictionary<string, object?> GetGroupAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["opta-playground-editor-group"] = true
        };

        if (TryGetClasses(DataProvider.DefaultEditorGroupClass, out var classes))
        {
            result["class"] = classes;
        }

        return result;
    }

    private Dictionary<string, object?> GetLabelAttributes()
    {
        var result = new Dictionary<string, object?>();

        if (TryGetClasses(DataProvider.DefaultEditorLabelClass, out var classes))
        {
            result["class"] = classes;
        }

        return result;
    }

    private IReadOnlyList<EditorGroup> GetParameterGroups()
    {
        return Descriptor?.Parameters
            .Select((parameter, index) => new IndexedParameter(parameter, index))
            .GroupBy(item => item.Parameter.Group)
            .OrderBy(group => group.Key is null ? 0 : 1)
            .ThenBy(group => group.Key ?? string.Empty, StringComparer.Ordinal)
            .Select(group => new EditorGroup(
                group.Key,
                group.OrderBy(item => item.Parameter.Order)
                    .ThenBy(item => item.Index)
                    .Select(item => item.Parameter)
                    .ToList()))
            .ToList()
            ?? [];
    }

    private static string GetLabelText(PlaygroundParameterDescriptor parameter)
    {
        return string.IsNullOrWhiteSpace(parameter.DisplayName)
            ? parameter.Name
            : parameter.DisplayName;
    }

    private static Type GetEditorComponentType(PlaygroundParameterDescriptor parameter)
    {
        return parameter.EditorType switch
        {
            ParameterEditorType.Text => typeof(OptAEditorText),
            ParameterEditorType.Number => typeof(OptAEditorNumber),
            ParameterEditorType.Boolean => typeof(OptAEditorBoolean),
            ParameterEditorType.Enum => typeof(OptAEditorEnum),
            ParameterEditorType.Select => typeof(OptAEditorSelect),
            ParameterEditorType.Color => typeof(OptAEditorColor),
            ParameterEditorType.Content => typeof(OptAEditorContent),
            _ => typeof(OptAEditorText)
        };
    }

    private Dictionary<string, object?> GetEditorParameters(PlaygroundParameterDescriptor parameter)
    {
        return new Dictionary<string, object?>
        {
            [nameof(OptAEditorBase.Descriptor)] = parameter,
            [nameof(OptAEditorBase.Value)] = GetCurrentValue(parameter),
            [nameof(OptAEditorBase.ValueChanged)] = EventCallback.Factory.Create<object?>(this, value => HandleEditorValueChanged(parameter.Name, value))
        };
    }

    private object? GetCurrentValue(PlaygroundParameterDescriptor parameter)
    {
        return CurrentParameters.TryGetValue(parameter.Name, out var value)
            ? value
            : parameter.DefaultValue;
    }

    private Task HandleEditorValueChanged(string name, object? value)
    {
        return ValueChanged.InvokeAsync((name, value));
    }

    private sealed record IndexedParameter(PlaygroundParameterDescriptor Parameter, int Index);

    private sealed record EditorGroup(string? Name, IReadOnlyList<PlaygroundParameterDescriptor> Parameters);
}
