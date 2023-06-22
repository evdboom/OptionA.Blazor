namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Determines the type of tag to use for (mainly) <see cref="Block"/> or derived components.
    /// </summary>
    public enum BlockType
    {
        /// <summary>
        /// Render just the content, resulting in a no tag for a <see cref="Block"/> or derived components, this also affects styles set as the component itself will not use these (set through parent)
        /// </summary>
        Content,
        /// <summary>
        /// Normal block, resulting in a &lt;div&gt; tag for a <see cref="Block"/> or derived components
        /// </summary>
        Block,
        /// <summary>
        /// Inline block, resulting in a &lt;span&gt; tag for a <see cref="Block"/> or derived components
        /// </summary>
        Inline,

        /// <summary>
        /// Paragraph block, similar to <see cref="Block"/>, but results in a &lt;p&gt; tag for a <see cref="Block"/> or derived components
        /// </summary>
        Paragraph,        

    }
}
