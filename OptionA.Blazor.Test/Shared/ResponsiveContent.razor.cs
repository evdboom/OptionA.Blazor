using Microsoft.AspNetCore.Components;
using OptionA.Blazor.Components;
using System.Xml.Linq;

namespace OptionA.Blazor.Test.Shared
{
    public partial class ResponsiveContent
    {
        [CascadingParameter(Name = OptAResponsive.DimensionParameterName)]
        public NamedDimension? Dimension { get; set; }
        [CascadingParameter(Name = OptAResponsive.DimensionNameParameterName)]
        public string? DimensionName { get; set; }
    }
}
