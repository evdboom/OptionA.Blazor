using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components;

namespace OptionA.Blazor.Test.Shared
{
    public partial class ResponsiveContent
    {
        [CascadingParameter(Name = OptAResponsive.DimensionParameterName)]
        public NamedDimension? Dimension { get; set; }
        [CascadingParameter(Name = OptAResponsive.DimensionNameParameterName)]
        public string? DimensionName { get; set; }
        [CascadingParameter(Name = OptAResponsive.ValidDimensionsParameterName)]
        public IEnumerable<string>? ValidDimensions { get; set; }
    }
}
