namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Extensions for the Block classes
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Creates a blockbuilder for the given parent
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static BlockBuilder<Parent> CreateBlock<Parent>(this Parent parent) where Parent : IParentBuilder
        {
            return new BlockBuilder<Parent>(parent);
        }

        /// <summary>
        /// Adds a block of text to the current builder
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Parent AddBlock<Parent>(this Parent parent, object? text) where Parent : IParentBuilder
        {
            return CreateBlock(parent)
                .WithText($"{text}")
                .Build();
        }

        /// <summary>
        /// Creates a blockbuilder for the given parent and sets the <see cref="BlockType"/> to <see cref="BlockType.Paragraph"/>
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static BlockBuilder<Parent> CreateParagraph<Parent>(this Parent parent) where Parent : IParentBuilder
        {
            return CreateBlock(parent)
                .WithBlockType(BlockType.Paragraph);
        }

        /// <summary>
        /// Adds a paragraph to the current builder
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Parent AddParagraph<Parent>(this Parent parent, object? text) where Parent : IParentBuilder
        {
            return CreateParagraph(parent)
                .WithText($"{text}")
                .Build();
        }

        /// <summary>
        /// Creates a blockbuilder for the given parent and sets the <see cref="BlockType"/> to <see cref="BlockType.Inline"/>
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static BlockBuilder<Parent> CreateInline<Parent>(this Parent parent) where Parent : IParentBuilder
        {
            return CreateBlock(parent)
                .WithBlockType(BlockType.Inline);
        }

        /// <summary>
        /// Adds a inline text to the current builder
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Parent AddInline<Parent>(this Parent parent, object? text) where Parent : IParentBuilder
        {
            return CreateInline(parent)
                .WithText($"{text}")
                .Build();
        }

        /// <summary>
        /// Creates a blockbuilder for the given parent and sets the <see cref="BlockType"/> to <see cref="BlockType.Content"/>
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static BlockBuilder<Parent> CreateContent<Parent>(this Parent parent) where Parent : IParentBuilder
        {
            return CreateBlock(parent)
                .WithBlockType(BlockType.Content);
        }

        /// <summary>
        /// Adds text to the current builder
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Parent AddContent<Parent>(this Parent parent, object? text) where Parent : IParentBuilder
        {
            return CreateContent(parent)
                .WithText($"{text}")
                .Build();
        }

        /// <summary>
        /// Adds a space (' ') to the current builder
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static Parent AddSpace<Parent>(this Parent parent) where Parent : IParentBuilder
        {
            return CreateInline(parent)
                .WithText(" ")
                .Build();
        }

        /// <summary>
        /// Adds a quote to the current builder
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="quote"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Parent AddQuote<Parent>(this Parent parent, string quote, string source) where Parent : IParentBuilder
        {
            return AddQuote(parent, quote, source, string.Empty);
        }

        /// <summary>
        /// Adds a quote to the current builder
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="quote"></param>
        /// <param name="source"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        public static Parent AddQuote<Parent>(this Parent parent, string quote, string source, string link) where Parent : IParentBuilder
        {
            var paragraph = CreateParagraph(parent)
                .WithStyle(Style.Normal)
                .WithTextAlignment(PositionType.Center)
                .CreateBlock()
                    .WithBorder(Side.All)
                    .AddBorderRadius(Side.All)
                    .AddPadding(Side.All, Strength.Two)
                    .WithBlockType(BlockType.Block)
                    .WithStyle(Style.Italic)
                    .WithText($"\"{quote}\"")
                    .WithColor(BlogColor.Quote)
                    .Build();

            if (!string.IsNullOrEmpty(link))
            {
                return paragraph
                    .CreateBlock()
                        .WithBlockType(BlockType.Block)
                        .WithStyle(Style.Italic)
                        .CreateLink()
                            .WithColor(BlogColor.Subtle)
                            .WithStyle(Style.NoDecoration)
                            .WithHref(link)
                            .WithMode(LinkMode.NewTab)
                            .WithText(source)
                            .Build()
                        .Build()
                    .Build();
            }
            else
            {
                return paragraph
                    .CreateBlock()
                        .WithBlockType(BlockType.Block)
                        .WithStyle(Style.Italic)
                        .WithText(source)
                        .WithColor(BlogColor.Subtle)
                        .Build()
                    .Build();
            }
        }

        /// <summary>
        /// Adds a footer to the current builder
        /// </summary>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="parent"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Parent AddFooter<Parent>(this Parent parent, object? text) where Parent : IParentBuilder
        {
            return CreateBlock(parent)
                .WithBlockType(BlockType.Block)
                .WithStyle(Style.Italic)
                .WithText($"{text}")
                .WithTextAlignment(PositionType.Center)
                .WithColor(BlogColor.Subtle)
                .Build();
        }
    }
}
