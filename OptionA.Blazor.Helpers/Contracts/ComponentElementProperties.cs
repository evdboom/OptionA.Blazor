namespace OptionA.Blazor.Helpers.Contracts;

/// <summary>
/// Styling and content overrides for a component element.
/// </summary>
public class ComponentElementProperties
{
    /// <summary>
    /// CSS class to apply.
    /// </summary>
    public string? Class { get; set; }

    /// <summary>
    /// Content override for the element.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Additional class to apply to rendered content.
    /// </summary>
    public string? ContentClass { get; set; }

    /// <summary>
    /// Additional HTML attributes to merge into the element.
    /// </summary>
    public Dictionary<string, object?>? AdditionalAttributes { get; set; }
}
