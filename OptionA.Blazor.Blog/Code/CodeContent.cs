namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Content for the <see cref="Code.Code"/> component
    /// </summary>
    public class CodeContent : BlockContent
    {
        /// <summary>
        /// Parser for transforming raw code into more readable code.
        /// </summary>
        public IParser? Parser { get; set; }
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
            foreach(var className in DefaultClasses.CodeBlock)
            {
                yield return className;
            }
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

            if (Parser is null)
            {
                return builder
                    .AddContent(Code)
                    .Build();
            }

            foreach (var (part, type, marker) in Parser.GetParts(Code))
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
