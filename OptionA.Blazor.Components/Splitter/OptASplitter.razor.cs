using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Components
{
    /// <summary>
    /// splitter component for creating scalabale parts
    /// </summary>
    public partial class OptASplitter
    {
        /// <summary>
        /// First child fragment, left or top (depending on orientation)
        /// </summary>
        [Parameter]
        public RenderFragment? First { get; set; }
        /// <summary>
        /// First child fragment, right or bottom (depending on orientation)
        /// </summary>
        [Parameter]
        public RenderFragment? Second { get; set; }
        /// <summary>
        /// Orientation of splitter
        /// </summary>
        [Parameter]
        public Orientation Orientation { get; set; }
        /// <summary>
        /// Initial percentage of width/height of first fragment
        /// </summary>
        [Parameter]
        public int? StartPercentageFirst { get; set; }
        /// <summary>
        /// Minimum percentage of width/height
        /// </summary>
        [Parameter]
        public int? MinimumPercentageFirst { get; set; }
        /// <summary>
        /// Minimum percentage of width/height
        /// </summary>
        [Parameter]
        public int? MinimumPercentageSecond { get; set; }
        /// <summary>
        /// Maximum percentage of width/height
        /// </summary>
        [Parameter]
        public int? MaximumPercentageFirst { get; set; }
        /// <summary>
        /// Maximum percentage of width/height
        /// </summary>
        [Parameter]
        public int? MaximumPercentageSecond { get; set; }

        private Dictionary<string, object?> GetFirstContainerAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-splitter-first"] = true
            };


            return result;
        }

        private Dictionary<string, object?> GetSecondContainerAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-splitter-second"] = true
            };

            return result;
        }

        private Dictionary<string, object?> GetSplitterAttributes()
        {
            var result = new Dictionary<string, object?>
            {
                ["opta-splitter-bar"] = true
            };


            return result;
        }
    }
}
