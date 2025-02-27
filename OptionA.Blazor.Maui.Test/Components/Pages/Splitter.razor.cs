﻿using OptionA.Blazor.Components;

namespace OptionA.Blazor.Maui.Test.Components.Pages;

public partial class Splitter
{
    private Orientation _orientation;
    private DragMode _dragMode;
    private int _startPercentageFirst = 50;
    private int _minimumFirst = 10;
    private int _minimumSecond = 10;
    private bool _show;
    private int _containerSize = 600;

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

    private Dictionary<string, object?> GetContainerAttributes()
    {
        var result = new Dictionary<string, object?>
        {
            ["splitter-container"] = true,
            ["style"] = $"--container-size: {_containerSize}px;"
        };

        return result;
    }
}
