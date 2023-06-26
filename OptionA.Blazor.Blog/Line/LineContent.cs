using System.Text.Json;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for linecomponent
    /// </summary>
    public class LineContent : Content
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public LineContent() : base() { }
        /// <summary>
        /// Constructor for use in deserialization
        /// </summary>
        /// <param name="items"></param>
        public LineContent(Dictionary<string, JsonElement> items) : base(items) { }

        /// <inheritdoc/>
        public override ComponentType Type => ComponentType.Line;

        /// <inheritdoc/>
        protected override void OnSerialize(Dictionary<string, object> items)
        {
            
        }
    }
}
