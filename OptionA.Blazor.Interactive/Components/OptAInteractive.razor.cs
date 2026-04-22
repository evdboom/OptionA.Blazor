using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Playground;

namespace OptionA.Blazor.Interactive;

/// <summary>
/// Bridges the interactive package surface to the existing playground renderer.
/// </summary>
public partial class OptAInteractive
{
    /// <summary>
    /// Gets or sets the component descriptor that defines the previewed component.
    /// </summary>
    [Parameter]
    public PlaygroundDescriptorBase? Descriptor { get; set; }

    /// <summary>
    /// Gets or sets the optional layout override.
    /// </summary>
    [Parameter]
    public PlaygroundLayout? Layout { get; set; }

    [Inject]
    private IInteractiveDataProvider DataProvider { get; set; } = null!;

    private Dictionary<string, object?> GetInteractiveAttributes()
    {
        var result = GetAttributes();
        result["opta-interactive"] = true;
        result["code-editing-enabled"] = DataProvider.CodeEditingEnabled;
        result["preferred-code-editor"] = DataProvider.PreferredCodeEditor.ToString().ToLowerInvariant();
        result["default-code-language"] = DataProvider.DefaultCodeLanguage ?? string.Empty;
        result["export-formats"] = string.Join(",", DataProvider.EnabledExportFormats.Select(format => format.ToString().ToLowerInvariant()));

        if (TryGetClasses(DataProvider.DefaultInteractiveClass, out var classes))
        {
            result["class"] = classes;
        }

        return result;
    }
}
