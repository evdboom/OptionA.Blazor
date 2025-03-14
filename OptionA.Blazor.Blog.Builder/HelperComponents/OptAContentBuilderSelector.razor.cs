﻿using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog.Builder.HelperComponents;

/// <summary>
/// Component for selecting correct builder
/// </summary>
public partial class OptAContentBuilderSelector
{
    /// <summary>
    /// Index of content in post (for id uniqueness)
    /// </summary>
    [Parameter]
    public int ContentIndex { get; set; }
    /// <summary>
    /// Total number of content (for disabling move up, move down)
    /// </summary>
    [Parameter]
    public int TotalContentCount { get; set; }
    /// <summary>
    /// Content to select
    /// </summary>
    [Parameter]
    public IContent? Content { get; set; }
    /// <summary>
    /// Called whenever content changes
    /// </summary>
    [Parameter]
    public EventCallback ContentChanged { get; set; }
    /// <summary>
    /// Called whenever content should be removed
    /// </summary>
    [Parameter]
    public EventCallback ContentRemoved { get; set; }
    /// <summary>
    /// Occurs when move up is clicked
    /// </summary>
    [Parameter]
    public EventCallback MovedUp { get; set; }
    /// <summary>
    /// Occurs when move down is clicked
    /// </summary>
    [Parameter]
    public EventCallback MovedDown { get; set; }
    /// <summary>
    /// Called when the drag operation is started
    /// </summary>
    [Parameter]
    public EventCallback<DragEvent> DragStarted { get; set; }
    /// <summary>
    /// Called when the drag operation is ended
    /// </summary>
    [Parameter]
    public EventCallback<DragEvent> DragEnded { get; set; }
    /// <summary>
    /// Called when the component is moved to a new index
    /// </summary>
    [Parameter]
    public EventCallback<int> MovedToIndex { get; set; }
}
