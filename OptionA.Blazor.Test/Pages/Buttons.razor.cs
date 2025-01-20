using Microsoft.AspNetCore.Components.Web;
using OptionA.Blazor.Components;

namespace OptionA.Blazor.Test.Pages;

public partial class Buttons
{
    private bool _show;
    private bool _disabled;
    private Orientation _orientation;
    private ActionType _actionType;
    private ButtonTypes _buttonTypes = ButtonTypes.Icon;
    private string _name = string.Empty;
    private string _description = string.Empty;
    private string _otherButton = string.Empty;
    private string _otherIcon = string.Empty;
    private int _containerSize = 600;

    private int _clicked;

    private Dictionary<string, object?> GetContainerAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["button-container"] = true,
            ["style"] = $"--container-size: {_containerSize}px;"
        };

        return result;
    }

    private void ChangeOrientation()
    {
        if (_orientation == Orientation.Horizontal)
        {
            _orientation = Orientation.Vertical;
        }
        else
        {
            _orientation = Orientation.Horizontal;
        }
    }

    private Task OnClick(MouseEventArgs e)
    {
        _clicked++;
        return InvokeAsync(StateHasChanged);            
    }

    private bool IsDisabled()
    {
        return _disabled;
    }

    private void ChangeActionType()
    {
        _actionType = _actionType switch
        {
            ActionType.Default => ActionType.Add,
            ActionType.Add => ActionType.Remove,
            ActionType.Remove => ActionType.Refresh,
            ActionType.Refresh => ActionType.Search,
            ActionType.Search => ActionType.Edit,
            ActionType.Edit => ActionType.Cancel,
            ActionType.Cancel => ActionType.Confirm,
            ActionType.Confirm => ActionType.Other,
            _ => ActionType.Default
        };
    }

    private void ChangeButtonType()
    {
        if (_buttonTypes == ButtonTypes.Icon)
        {
            _buttonTypes = ButtonTypes.Name;
        }
        else if (_buttonTypes == ButtonTypes.Name)
        {
            _buttonTypes = ButtonTypes.Full;
        }
        else
        {
            _buttonTypes = ButtonTypes.Icon;
        }
    }
}
