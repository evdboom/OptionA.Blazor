namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for the <see cref="ListItem"/> component, inherits from <see cref="BlockContent"/>
    /// </summary>
    public class ListItemContent : BlockContent
    {
        /// <inheritdoc/>
        public override ComponentType Type => ComponentType.Row;
    }
}
