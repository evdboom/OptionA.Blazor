using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components;

/// <summary>
/// Base class for components
/// </summary>
public class OptAComponent : ComponentBase
{
    /// <summary>
    /// Additional classes to add to the component
    /// </summary>
    [Parameter]
    public string? AdditionalClasses { get; set; }
    /// <summary>
    /// Classes to remove from the default supplied
    /// </summary>
    [Parameter]
    public IList<string>? RemovedClasses { get; set; }
    /// <summary>
    /// Attributes to set for the component
    /// </summary>
    [Parameter]
    public IDictionary<string, object?>? Attributes { get; set; }

    /// <summary>
    /// Tries to get the classes for this component
    /// </summary>
    /// <param name="defaultClass"></param>
    /// <param name="resultClass"></param>
    /// <returns></returns>
    protected bool TryGetClasses(string? defaultClass, out string resultClass)
    {
        var start = ParseClasses(defaultClass, AdditionalClasses);
        var removed = RemovedClasses ?? [];

        var classList = start                
            .Except(removed)
            .ToList();

        resultClass = string.Join(' ', classList);

        return classList.Count != 0;
    }

    /// <summary>
    /// Gets the attributes set for this component
    /// </summary>
    /// <returns></returns>
    protected Dictionary<string, object?> GetAttributes()
    {
        var result = new Dictionary<string, object?>();

        if (Attributes is not null)
        {
            foreach (var attribute in Attributes)
            {
                result[attribute.Key] = attribute.Value;
            }
        }

        return result;
    }

    /// <summary>
    /// Gives the list of unique classes for the given default and additional
    /// </summary>
    /// <param name="defaultClass"></param>
    /// <param name="additionalClass"></param>
    /// <returns></returns>
    protected List<string> ParseClasses(string? defaultClass, string? additionalClass)
    {
        var start = (defaultClass ?? string.Empty)
            .Split(' ')
            .ToList();
        var additional = (additionalClass ?? string.Empty)
            .Split(' ')
            .ToList();

        return start
            .Concat(additional)                
            .Distinct()
            .Where(c => !string.IsNullOrEmpty(c))
            .ToList();
    }
}
