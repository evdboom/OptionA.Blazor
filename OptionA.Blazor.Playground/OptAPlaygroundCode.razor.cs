using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Playground;

/// <summary>
/// Minimal code shell used by the playground container.
/// </summary>
public partial class OptAPlaygroundCode
{
    /// <summary>
    /// Gets or sets the descriptor being displayed as code.
    /// </summary>
    [Parameter]
    public PlaygroundDescriptorBase? Descriptor { get; set; }

    /// <summary>
    /// Gets or sets the current playground parameter values.
    /// </summary>
    [CascadingParameter]
    public Dictionary<string, object?> CurrentParameters { get; set; } = [];

    /// <summary>
    /// Gets or sets the cascaded callback used to update a playground parameter value.
    /// </summary>
    [CascadingParameter]
    public EventCallback<(string Name, object? Value)> ValueChanged { get; set; }

    [Inject]
    private IPlaygroundDataProvider DataProvider { get; set; } = null!;

    private Dictionary<string, object?> GetCodeAttributes()
    {
        var result = GetAttributes();
        result["opta-playground-code"] = true;

        if (TryGetClasses(DataProvider.DefaultCodeClass, out var classes))
        {
            result["class"] = classes;
        }

        return result;
    }

    private string GetGeneratedCode()
    {
        return Descriptor is null
            ? string.Empty
            : PlaygroundCodeGenerator.Generate(Descriptor, CurrentParameters);
    }
}
