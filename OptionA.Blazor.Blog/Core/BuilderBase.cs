using Microsoft.AspNetCore.Components.Web;
namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Base class for a content builder
    /// </summary>
    /// <typeparam name="Builder"></typeparam>
    /// <typeparam name="Result"></typeparam>
    public abstract class BuilderBase<Builder, Result> : IBuilder
    {
        /// <summary>
        /// The result which will be returned by the <see cref="Build"/> method
        /// </summary>
        protected readonly Result _result;

        /// <summary>
        /// The current <see cref="Style" /> for this builder, Style is copied to child builders.
        /// </summary>
        protected Style _style;
        /// <inheritdoc/>
        public Style Style => _style;

        /// <summary>
        /// The current <see cref="PositionType" /> for the text alignment for this builder, TextAlignment is copied to child builders.
        /// </summary>
        protected PositionType _textAlignment;
        /// <inheritdoc/>
        public PositionType TextAlignment => _textAlignment;

        /// <summary>
        /// the current <see cref="BlockType" /> for this builder, BlockType is copied to child builders.
        /// </summary>
        protected BlockType _blockType;
        /// <inheritdoc/>
        public BlockType BlockType => _blockType;

        /// <summary>
        /// the current <see cref="PositionType" /> for the block alignment for this builder, BlockAlignment is copied to child builders.
        /// </summary>
        protected PositionType _blockAlignment;
        /// <inheritdoc/>
        public PositionType BlockAlignment => _blockAlignment;

        /// <summary>
        /// the current <see cref="BlogColor" /> for this builder, Color is only copied to child builders that do not have a default color themselves. 
        /// </summary>
        protected BlogColor _color;
        /// <inheritdoc/>
        public BlogColor Color => _color;
        /// <summary>
        /// the current value for margin
        /// </summary>
        protected IDictionary<Side,Strength> _margin = new Dictionary<Side, Strength>();
        /// <inheritdoc/>
        public IDictionary<Side, Strength> Margin => _margin;
        /// <summary>
        /// the current value for padding
        /// </summary>
        protected IDictionary<Side, Strength> _padding = new Dictionary<Side, Strength>();
        /// <inheritdoc/>
        public IDictionary<Side, Strength> Padding => _padding;
        /// <summary>
        /// The current value for border sides
        /// </summary>
        protected Side _border;
        ///<inheritdoc/>
        public Side Border => _border;
        /// <summary>
        /// The current value for borderradius
        /// </summary>
        protected IList<Side> _borderRadius = new List<Side>();
        /// <inheritdoc/>
        public IList<Side> BorderRadius => _borderRadius;
        /// <summary>
        /// The currently set onclick action, this is not copied to childbuilders.
        /// </summary>
        protected Func<MouseEventArgs, Task>? _onClick;
        /// <inheritdoc/>
        public Func<MouseEventArgs, Task>? OnClick => _onClick;


        /// <summary>
        /// Override if this builder has it's own default color, so it will not inherit from the parent.
        /// </summary>
        protected virtual BlogColor OwnColor => BlogColor.Inherit;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="result"></param>
        protected BuilderBase(Result result)
        {
            _result = result;
            _color = OwnColor;
            _blockType = BlockType.Block;
        }

        /// <summary>
        /// Builds the current builder, calls the <see cref="OnBuild"/> method and then returning the Result/>
        /// </summary>
        /// <returns></returns>
        public Result Build()
        {
            OnBuild();
            return _result;
        }

        /// <summary>
        /// Sets the <see cref="TextAlignment"/> for the current builder. Child builders created after this will also inherit this text alignment
        /// </summary>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public Builder WithTextAlignment(PositionType alignment)
        {
            _textAlignment = alignment;
            return This();
        }

        /// <summary>
        /// Seth the <see cref="Color" /> for the current builder. Child builders created after this will inherit this color if they do not have a default color themselves
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public Builder WithColor(BlogColor color)
        {
            _color = color;
            return This();
        }

        /// <summary>
        /// Sets the <see cref="BlockAlignment"/> for the current builder. Child builders created after this will also inherit this block alignment
        /// </summary>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public Builder WithBlockAlignment(PositionType alignment)
        {
            _blockAlignment = alignment;
            return This();
        }

        /// <summary>
        /// Sets the <see cref="Style"/> for the current builder, overrides all previously set styles. Child builders created after this will also inherit this Style
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public Builder WithStyle(Style style)
        {
            _style = style;
            return This();
        }

        /// <summary>
        /// Sets the margin to be used for this builder, margin is not copied to child builders
        /// </summary>
        /// <param name="side"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public Builder AddMargin(Side side, Strength strength)
        {
            _margin[side] = strength;
            return This();
        }

        /// <summary>
        /// Sets the padding to be used for this builder, padding is not copied to child builders
        /// </summary>
        /// <param name="side"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public Builder AddPadding(Side side, Strength strength)
        {
            _padding[side] = strength;
            return This();
        }

        /// <summary>
        /// Sets the border for this builder, this is not copied to child builders
        /// </summary>
        /// <param name="side"></param>
        /// <returns></returns>
        public Builder WithBorder(Side side)
        {
            _border = side;
            return This();
        }

        /// <summary>
        /// Sets the borderradius for this builder, this is not copied to child builders
        /// </summary>
        /// <param name="side"></param>
        /// <returns></returns>
        public Builder AddBorderRadius(Side side)
        {
            _borderRadius.Add(side);
            return This();
        }

        /// <summary>
        /// Adds the given style to the <see cref="Style"/> for the current builder, keeps previously set styles. Child builders created after this will also inherit this Style
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public Builder AddStyle(Style style)
        {
            _style |= style;
            return This();
        }

        /// <summary>
        /// Removes the given style from the the <see cref="Style"/> for the current builder, keeps other set styles. Child builders created after this will also not have this style
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public Builder RemoveStyle(Style style)
        {
            _style &= ~style;
            return This();
        }

        /// <summary>
        /// Sets the <see cref="BlockType"/> for the currenty builder, Child builders created after this will also have this BlockStyle
        /// </summary>
        /// <param name="blockType"></param>
        /// <returns></returns>
        public Builder WithBlockType(BlockType blockType)
        {
            _blockType = blockType;
            return This();
        }

        /// <summary>
        /// Adds an on click action for the result of the current builder
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Builder WithOnClick(Func<MouseEventArgs, Task> action)
        {
            _onClick = action;
            return This();
        }

        /// <summary>
        /// Should be set to 'return this;' to make the return call for generic set methods return the correct builder to make it easier chainable.
        /// </summary>
        /// <returns></returns>
        protected abstract Builder This();

        /// <summary>
        /// Called during builder build, override to add custom build logic
        /// </summary>
        protected virtual void OnBuild()
        {
        }
    }
}
