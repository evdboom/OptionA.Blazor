using System.Text.Json;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for the <see cref="Block.Block"/> component
    /// </summary>
    public class BlockContent : Content
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public BlockContent() :base() { }
        /// <summary>
        /// Constructor for use in deserialization
        /// </summary>
        /// <param name="items"></param>
        public BlockContent(Dictionary<string, JsonElement> items) : base(items)
        {
            if (items.TryGetValue(nameof(BlockType), out var type))
            {
                BlockType = JsonSerializer.Deserialize<BlockType>(type);
            }
            if (items.TryGetValue(nameof(Text), out var text))
            {
                Text = JsonSerializer.Deserialize<string>(text) ?? string.Empty;
            }
            if (items.TryGetValue(nameof(TextAfterContent), out var textAfterContent))
            {
                TextAfterContent = JsonSerializer.Deserialize<bool>(textAfterContent);
            }
        }

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

        /// <inheritdoc />
        protected override void OnSerialize(Dictionary<string, object> items)
        {
            items[nameof(BlockType)] = BlockType;
            if (!string.IsNullOrEmpty(Text)) 
            {
                items[nameof(Text)] = Text;
            }
            items[nameof(TextAfterContent)] = TextAfterContent;


        }
    }
}
