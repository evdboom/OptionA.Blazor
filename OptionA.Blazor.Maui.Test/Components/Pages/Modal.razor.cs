using OptionA.Blazor.Components;

namespace OptionA.Blazor.Maui.Test.Components.Pages;

public partial class Modal
{
    private OptAModal? _modal;
    private bool _draggable;
    private ModalSize _size;
    private DragMode _dragMode;

    private string _text = string.Empty;

    private void ChangeDraggable()
    {
        _draggable = !_draggable;
    }
    private void ChangeDragMode()
    {
        if (_dragMode == DragMode.Direct)
        {
            _dragMode = DragMode.Outline;
        }
        else
        {
            _dragMode = DragMode.Direct;
        }
    }
    private void ChangeSize() 
    {
        if (_size == ModalSize.Default)
        {
            _size = ModalSize.Small;
        }
        else if (_size == ModalSize.Small)
        {
            _size = ModalSize.Large;
        }
        else if (_size == ModalSize.Large)
        {
            _size = ModalSize.ExtraLarge;
        }
        else
        {
            _size = ModalSize.Default;
        }
    }

}
