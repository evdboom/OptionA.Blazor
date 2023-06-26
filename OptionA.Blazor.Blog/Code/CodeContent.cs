using OptionA.Blazor.Blog.Parsers;
using System.Text.Json;

namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for the <see cref="Code.Code"/> component
    /// </summary>
    public class CodeContent : BlockContent
    {
        private static readonly Dictionary<CodeLanguage, IParser> _parsers = new()
        {
            { CodeLanguage.CSharp, new CSharpParser() },
            { CodeLanguage.Html, new HtmlParser() }
        };

        /// <summary>
        /// Default constructor
        /// </summary>
        public CodeContent() : base() { }
        /// <summary>
        /// Constructor for use in deserialization
        /// </summary>
        /// <param name="items"></param>
        public CodeContent(Dictionary<string, JsonElement> items) : base(items)
        {
            if (items.TryGetValue(nameof(Code), out var code))
            {
                Code = JsonSerializer.Deserialize<string>(code) ?? string.Empty;
            }
            if (items.TryGetValue(nameof(Language), out var language))
            {
                Language = JsonSerializer.Deserialize<CodeLanguage>(language);
            }
        }

        /// <summary>
        /// Code to transform
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// Language of the code
        /// </summary>
        public CodeLanguage Language { get; set; }
        /// <inheritdoc/>
        public override ComponentType Type => ComponentType.Code;

        /// <summary>
        /// Childcontent for code block is set by block, cannot set it directly
        /// </summary>
        public override IList<IContent> ChildContent => GetChildren()
            .ToList();

        /// <summary>
        /// Adds the <see cref="DefaultClasses.CodeBlock"/> class to the content
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<string> GetContentClassesList()
        {
            foreach (var className in DefaultClasses.CodeBlock)
            {
                yield return className;
            }
        }

        /// <inheritdoc />
        protected override void OnSerialize(Dictionary<string, object> items)
        {
            base.OnSerialize(items);
            items[nameof(Code)] = Code;
            items[nameof(Language)] = Language;

        }

        private IEnumerable<IContent> GetChildren()
        {
            var builder = ComponentBuilder.CreateBuilder(Post!);
            if (Language != CodeLanguage.Other)
            {
                builder
                    .CreateBlock()
                        .WithStyle(Style.Bold)
                        .WithText(Language.ToDisplayLanguage())
                        .WithTextAlignment(PositionType.Right)
                        .AddClasses(DefaultClasses.CodeHeaderBlock)
                        .Build();
            }

            if (!_parsers.TryGetValue(Language, out var parser))
            {
                return builder
                   .AddContent(Code)
                   .Build();
            }

            foreach (var (part, type, marker) in parser.GetParts(Code))
            {
                if (marker == MarkerType.None && type == CodePart.Text)
                {
                    builder
                        .AddContent(part);

                }
                else
                {
                    var content = builder
                        .CreateInline()
                        .WithText(part)
                        .AddClass(type.GetPartClass());
                    if (marker.HasFlag(MarkerType.Selection))
                    {
                        content
                            .AddClass(DefaultClasses.SelectedCode);
                    }
                    content
                        .Build();
                }
            }

            return builder
                .Build();
        }
    }
}
