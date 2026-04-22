using System.Globalization;
using System.Text;
using System.Text.Encodings.Web;

namespace OptionA.Blazor.Playground;

/// <summary>
/// Generates readable Razor markup for a playground descriptor and its current parameter values.
/// </summary>
public static class PlaygroundCodeGenerator
{
    private const int MaxLineLength = 100;
    private const string AttributeIndent = "    ";

    /// <summary>
    /// Builds Razor markup for the supplied descriptor and parameter values.
    /// </summary>
    /// <param name="descriptor">The component descriptor to render as code.</param>
    /// <param name="currentParameters">The current parameter values selected in the playground.</param>
    /// <returns>A formatted Razor markup snippet.</returns>
    public static string Generate(PlaygroundDescriptorBase descriptor, IReadOnlyDictionary<string, object?> currentParameters)
    {
        ArgumentNullException.ThrowIfNull(descriptor);
        ArgumentNullException.ThrowIfNull(currentParameters);

        var componentName = GetComponentName(descriptor.ComponentType);
        var attributes = descriptor.Parameters
            .Where(parameter => currentParameters.TryGetValue(parameter.Name, out var value) && !Equals(value, parameter.DefaultValue))
            .Select(parameter => $"{parameter.Name}=\"{FormatValue(currentParameters[parameter.Name])}\"")
            .ToList();

        if (attributes.Count == 0)
        {
            return $"<{componentName} />";
        }

        var singleLineMarkup = $"<{componentName} {string.Join(" ", attributes)} />";
        if (singleLineMarkup.Length <= MaxLineLength)
        {
            return singleLineMarkup;
        }

        var builder = new StringBuilder();
        builder.Append('<').Append(componentName).AppendLine();

        foreach (var attribute in attributes)
        {
            builder.Append(AttributeIndent).Append(attribute).AppendLine();
        }

        builder.Append("/>");
        return builder.ToString();
    }

    private static string GetComponentName(Type componentType)
    {
        var typeName = componentType.Name;
        var genericMarkerIndex = typeName.IndexOf('`');
        return genericMarkerIndex >= 0 ? typeName[..genericMarkerIndex] : typeName;
    }

    private static string FormatValue(object? value)
    {
        if (value is null)
        {
            return "null";
        }

        return value switch
        {
            string stringValue => HtmlEncoder.Default.Encode(stringValue),
            bool boolValue => boolValue ? "true" : "false",
            Enum enumValue => $"{enumValue.GetType().Name}.{enumValue}",
            IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture),
            _ => value.ToString() ?? string.Empty
        };
    }
}
