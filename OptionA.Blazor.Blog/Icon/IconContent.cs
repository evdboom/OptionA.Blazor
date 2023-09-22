using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for icons
    /// </summary>
    public class IconContent : Content
    {
        /// <inheritdoc/>
        public override ContentType Type => ContentType.Icon;

        /// <summary>
        /// Paths to render
        /// </summary>
        public List<string> Paths { get; } = new();
        /// <summary>
        /// Width when in Pathing mode
        /// </summary>
        public string? Width { get; set; }
        /// <summary>
        /// Height when in Pathing mode
        /// </summary>
        public string? Height { get; set; }
        /// <summary>
        /// Viewbox for when path is set
        /// </summary>
        public int[] ViewBoxValues { get; } = new int[4];
        /// <summary>
        /// Gets the mode to render
        /// </summary>
        public IconMode Mode { get; set; }
        /// <inheritdoc/>
        public override Dictionary<string, object?> Attributes
        {
            get
            {
                var attributes = base.Attributes;
                if (Mode == IconMode.Path)
                {
                    if (!string.IsNullOrEmpty(Width))
                    {
                        attributes["width"] = Width;
                    }
                    if (!string.IsNullOrEmpty(Height))
                    {
                        attributes["height"] = Height;
                    }
                    attributes["fill"] = "currentColor";
                    attributes["viewBox"] = string.Join(" ", ViewBoxValues);
                }
                return attributes;
            }
        }
    }
}
