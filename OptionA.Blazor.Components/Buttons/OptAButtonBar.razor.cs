using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components;

/// <summary>
/// Button bar to display multiple buttons
/// </summary>
public partial class OptAButtonBar
{
    /// <summary>
    /// Sets the id attribute of the bar
    /// </summary>
    [Parameter]
    public string? Id { get; set; }
    /// <summary>
    /// Orientation of the button bar
    /// </summary>
    [Parameter]
    public Orientation Orientation { get; set; }
    /// <summary>
    /// Buttons on the left or top (depending on <see cref="Orientation"/>)
    /// </summary>
    [Parameter]
    public RenderFragment? StartButtons { get; set; }
    /// <summary>
    /// Buttons in the middle
    /// </summary>
    [Parameter]
    public RenderFragment? CenterButtons { get; set; }
    /// <summary>
    /// Buttons on the right or bottom (depending on <see cref="Orientation"/>)
    /// </summary>
    [Parameter]
    public RenderFragment? EndButtons { get; set; }
    [Inject]
    private IButtonDataProvider DataProvider { get; set; } = null!;
    
    private Dictionary<string, object?> GetBarAttributes()
    {
        var result = GetAttributes();
        result["opta-button-bar"] = true;
        if (Orientation == Orientation.Vertical)
        {
            result["vertical"] = true;
        }
        
        if (TryGetClasses(DataProvider.DefaultButtonBarClass, out var classes))
        {
            result["class"] = classes;
        }

        if (!string.IsNullOrEmpty(Id))
        {
            result["id"] = Id;
        }

        return result;
    }

    private Dictionary<string, object?> GetGroupAttributes(string alignment)
    {
        var result = new Dictionary<string, object?>
        {
            ["opta-button-group"] = true,
            ["button-alignment"] = alignment
        };

        if (!string.IsNullOrEmpty(DataProvider.DefaultButtonGroupClass))
        {
            result["class"] = DataProvider.DefaultButtonGroupClass;
        }

        return result;
    }
}
