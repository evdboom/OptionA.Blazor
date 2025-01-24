using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components;

/// <summary>
/// Tabs component for placing content in tabs
/// </summary>
public partial class OptATabs
{
    /// <summary>
    /// Name of the cascading parameter
    /// </summary>
    public const string TabsParameterName = "TabsParent";
    /// <summary>
    /// Content to display should be wrapped in <see cref="OptATab"/> components
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private Dictionary<int, OptATab> _children = [];

    /// <summary>
    /// Add a tab to the tabs
    /// </summary>
    /// <param name="tab"></param>
    public void RegisterChild(OptATab tab)
    {
        _children[_children.Count] = tab;

        if (_children.Count == 1)
        {
            tab.IsCurrent = true;
            tab.Update();
        }
        StateHasChanged();
    }

    private void SelectTab(int index)
    {
        var current = _children.FirstOrDefault(x => x.Value.IsCurrent);
        if (current.Value != null)
        {
            current.Value.IsCurrent = false;
            current.Value.Update();
        }

        var tab = _children[index];
        tab.IsCurrent = true;
        tab.Update();
    }

    private Dictionary<string, object?> GetTabSelectAttributes(int index, OptATab tab)
    {
        var result = new Dictionary<string, object?>
        {
            ["opta-tab-select"] = true,
            ["onclick"] = EventCallback.Factory.Create(this, () => SelectTab(index))
        };
        if (!string.IsNullOrEmpty(tab.Title))
        {
            result["title"] = tab.Title;
        }
        if (tab.IsCurrent)
        {
            result["active"] = true;
        }

        return result;
    }

    private Dictionary<string, object?> GetHeaderAttributes()
    {
        var result = GetAttributes();
        result["opta-tabs-header"] = true;

        if (TryGetClasses(string.Empty, out var classes))
        {
            result["class"] = classes;
        }

        return result;
    }
}