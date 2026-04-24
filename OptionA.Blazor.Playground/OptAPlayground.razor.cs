using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Playground;

/// <summary>
/// Coordinates the interactive playground surface for a component descriptor.
/// </summary>
public partial class OptAPlayground
{
    private PlaygroundDescriptorBase? _initializedDescriptor;

    /// <summary>
    /// Gets or sets the descriptor that defines the playground content.
    /// </summary>
    [Parameter]
    public PlaygroundDescriptorBase? Descriptor { get; set; }

    /// <summary>
    /// Gets or sets the optional layout override for the playground surface.
    /// </summary>
    [Parameter]
    public PlaygroundLayout? Layout { get; set; }

    [Inject]
    private IPlaygroundDataProvider DataProvider { get; set; } = null!;

    /// <summary>
    /// Gets the current parameter values shown by the playground child components.
    /// </summary>
    protected Dictionary<string, object?> CurrentParameters { get; private set; } = [];

    /// <summary>
    /// Gets the callback cascaded to child components for value updates.
    /// </summary>
    protected EventCallback<(string Name, object? Value)> ValueChangedCallback { get; private set; }

    private PlaygroundLayout ResolvedLayout => Layout ?? DataProvider.DefaultLayout;

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        ValueChangedCallback = EventCallback.Factory.Create<(string Name, object? Value)>(this, HandleValueChanged);
    }

    /// <inheritdoc/>
    protected override void OnParametersSet()
    {
        if (!ReferenceEquals(_initializedDescriptor, Descriptor))
        {
            _initializedDescriptor = Descriptor;
            CurrentParameters = CreateCurrentParameters(Descriptor);
        }
    }

    private Dictionary<string, object?> GetContainerAttributes()
    {
        var result = GetAttributes();
        result["opta-playground"] = true;
        result["code-editing-enabled"] = DataProvider.CodeEditingEnabled.ToString().ToLowerInvariant();
        result["preferred-code-editor"] = DataProvider.PreferredCodeEditor.ToString().ToLowerInvariant();
        result["default-code-language"] = DataProvider.DefaultCodeLanguage ?? string.Empty;
        result["export-formats"] = string.Join(",", DataProvider.EnabledExportFormats.Select(format => format.ToString().ToLowerInvariant()));

        if (ResolvedLayout == PlaygroundLayout.Stacked)
        {
            result["stacked"] = true;
        }

        if (TryGetClasses(DataProvider.DefaultPlaygroundClass, out var classes))
        {
            result["class"] = classes;
        }

        return result;
    }

    private Task HandleValueChanged((string Name, object? Value) change)
    {
        CurrentParameters[change.Name] = change.Value;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private static Dictionary<string, object?> CreateCurrentParameters(PlaygroundDescriptorBase? descriptor)
    {
        return descriptor?.Parameters.ToDictionary(parameter => parameter.Name, parameter => parameter.DefaultValue) ?? [];
    }
}
