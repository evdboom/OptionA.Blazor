namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for the <see cref="Block.Block"/> component
    /// </summary>
    public class BlockContent : Content
    {
        /// <inheritdoc/>
        public override ComponentType Type => ComponentType.Block;
        /// <inheritdoc/>
        public BlockType BlockType { get; set; }
        /// <summary>
        /// Text to display override for custom behavior
        /// </summary>
        public virtual string Text { get; set; } = string.Empty;
        /// <summary>
        /// Boolean to determine where to place the text if also <see cref="Content.ChildContent"/> is present, default is before the content
        /// </summary>
        public bool TextAfterContent { get; set; }        

        /// <summary>
        /// Overridden the default to also set blocktype
        /// </summary>
        /// <param name="builder"></param>
        public override void SetProperties(IBuilder builder)
        {
            base.SetProperties(builder);
            BlockType = builder.BlockType;            
        }
    }
}
