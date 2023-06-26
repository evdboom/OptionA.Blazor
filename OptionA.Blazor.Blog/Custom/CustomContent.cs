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

        /// <summary>
        /// Currently not supported for customcomponent, they can only be set through builders.
        /// </summary>
        /// <param name="items"></param>
        /// <exception cref="NotSupportedException"></exception>
        protected override void OnSerialize(Dictionary<string, object> items)
        {
            throw new NotSupportedException();
        }
    }
}
