using Microsoft.AspNetCore.Components.Web;
namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Base interface for all builders
    /// </summary>
    public interface IBuilder
    {
        /// <summary>
        /// Gets the currently set <see cref="Style" /> for this builder, Style is copied to child builders.
        /// </summary>
        Style Style { get; }
        /// <summary>
        /// Gets the currently set <see cref="PositionType" /> for the text alignment for this builder, TextAlignment is copied to child builders.
        /// </summary>
        PositionType TextAlignment { get; }
        /// <summary>
        /// Gets the currently set <see cref="PositionType" /> for the block alignment for this builder, BlockAlignment is copied to child builders.
        /// </summary>
        PositionType BlockAlignment { get; }
        /// <summary>
        /// Gets the currently set <see cref="BlockType" /> for this builder, BlockType is copied to child builders.
        /// </summary>
        BlockType BlockType { get; }
        /// <summary>
        /// Gets the currently set <see cref="BlogColor" /> for this builder, Color is only copied to child builders that do not have a default color themselves. 
        /// </summary>
        BlogColor Color { get; }
        /// <summary>
        /// Gets the currently set Margin
        /// </summary>
        IDictionary<Side, Strength> Margin { get; }
        /// <summary>
        /// Gets the currently set Padding
        /// </summary>
        IDictionary<Side, Strength> Padding { get; }
        /// <summary>
        /// Gets the current value for sides of the border
        /// </summary>
        Side Border { get; }
        /// <summary>
        /// Gets the current value for sides of the borderradius
        /// </summary>
        IList<Side> BorderRadius { get; }
        /// <summary>
        /// Gets the currently set OnClick action for this builder, this is not copied to child builders.
        /// </summary>
        Func<MouseEventArgs,Task>? OnClick { get; }
    }
}
