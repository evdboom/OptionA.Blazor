﻿using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Blog.Builder.HelperComponents;
using OptionA.Blazor.Components;

namespace OptionA.Blazor.Blog.Builder.Parts;

/// <summary>
/// Build code blog part
/// </summary>
public partial class OptACodeBuilder
{
    private const string CodeId = "opta-code";
    private const string CodeLanguageId = "opta-code-Language";
    private const string OtherLanguageId = "opta-other-Language";

    /// <summary>
    /// Index of the current content in the collection
    /// </summary>
    [Parameter]
    public int ContentIndex { get; set; }
    /// <summary>
    /// Total number of content (for disabling move up, move down)
    /// </summary>
    [Parameter]
    public int TotalContentCount { get; set; }
    /// <summary>
    /// Content to create
    /// </summary>
    [Parameter]
    public CodeContent? Content { get; set; }
    /// <summary>
    /// Called when something changes
    /// </summary>
    [Parameter]
    public EventCallback ContentChanged { get; set; }
    /// <summary>
    /// Called when content should be removed
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
    [Inject]
    private IBlogBuilderDataProvider DataProvider { get; set; } = null!;

    private OptAFlexibleTextArea? _input;
    private BindMode? _bindMode = BindMode.OnChange;
    private bool _showAutoGrow = false;
    private bool _autoGrow = true;

    private CodeLanguage? InternalLanguage
    {
        get => Content?.Language ?? default;
        set
        {
            if (Content is null || !value.HasValue)
            {
                return;
            }
            if (!value.Value.Equals(Content.Language))
            {
                Content.Language = value.Value;
                if (ContentChanged.HasDelegate)
                {
                    ContentChanged.InvokeAsync();
                }
            }
        }
    }

    /// <inheritdoc/>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (_input is not null)
            {
                await _input.Element.FocusAsync(false);
            }
        }

    }

    private Dictionary<string, object?> GetCodeAttributes()
    {
        var defaultAttributes = new Dictionary<string, object?>
        {
            ["placeholder"] = "Text...",
            ["id"] = $"{CodeId}-{ContentIndex}"
        };

        return DataProvider.GetAttributes(BuilderType.TextAreaInput, defaultAttributes);
    }

    private Dictionary<string, object?> GetCodeLanguageAttributes()
    {
        var defaultAttributes = new Dictionary<string, object?>
        {
            ["id"] = $"{CodeLanguageId}-{ContentIndex}"
        };

        return DataProvider.GetAttributes(BuilderType.SelectInput, defaultAttributes);
    }

    private Dictionary<string, object?> GetOtherLanguageAttributes()
    {
        var defaultAttributes = new Dictionary<string, object?>
        {
            ["id"] = $"{OtherLanguageId}-{ContentIndex}"
        };

        return DataProvider.GetAttributes(BuilderType.TextInput, defaultAttributes);
    }

    private Dictionary<string, object?> GetLabelAttributes(string id)
    {
        var defaultAttributes = new Dictionary<string, object?>
        {
            ["for"] = $"{id}-{ContentIndex}"
        };

        return DataProvider.GetAttributes(BuilderType.Label, defaultAttributes);
    }
}
