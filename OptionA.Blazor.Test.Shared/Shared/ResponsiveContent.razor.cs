using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components;

namespace OptionA.Blazor.Test.Shared.Shared;

public partial class ResponsiveContent
{
    [CascadingParameter(Name = OptAResponsive.DimensionParameterName)]
    public NamedDimension? Dimension { get; set; }
    [CascadingParameter(Name = OptAResponsive.DimensionNameParameterName)]
    public string? DimensionName { get; set; }
    [CascadingParameter(Name = OptAResponsive.ValidDimensionsParameterName)]
    public IEnumerable<string>? ValidDimensions { get; set; }
    [CascadingParameter(Name = OptAResponsive.AllDimensionBreakPointsParameterName)]
    public IEnumerable<(string Name, int Width)>? BreakPoints { get; set; }
}
