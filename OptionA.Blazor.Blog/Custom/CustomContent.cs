using Microsoft.AspNetCore.Components;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Wrapper for your own components
    /// </summary>
    public class CustomContent : Content
    {
        /// <summary>
        /// Fragment to render
        /// </summary>
        public RenderFragment? Fragment { get; set; }
        /// <inheritdoc/>
        public override ComponentType Type => ComponentType.Custom;
    }
}
