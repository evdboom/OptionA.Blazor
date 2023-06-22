namespace OptionA.Blazor.Blog
{
    /// <summary>
    /// Classes used by the various components in this package, override with your own or clear if you do not wish to use the default style and behavior.
    /// </summary>
    public static class DefaultClasses
    {
        /// <summary>
        /// Default class for the encasing block of a <see cref="Code.Code"/>
        /// </summary>
        public static IList<string> CodeBlock { get; set; } = new List<string>
        {
            "opta-code-block",
        };

        /// <summary>
        /// Default class for the header of a <see cref="Code.Code"/>
        /// </summary>
        public static IList<string> CodeHeaderBlock { get; set; } = new List<string>
        {
            "opta-code-header-block"
        };

        /// <summary>
        /// Default classes selected code parts
        /// </summary>
        public static string? SelectedCode { get; set; } = "opta-code-selected";

        /// <summary>
        /// Default classes for displaying tags
        /// </summary>
        public static IList<string> Tag { get; set; } = new List<string>
        {
            "opta-tag-item",
            "opta-style-uppercase"
        };

        /// <summary>
        /// Default classes for containers
        /// </summary>
        public static IList<string> Container { get; set; } = new List<string>
        {
            "opta-container",
        };

        /// <summary>
        /// Default classes for containers hide button
        /// </summary>
        public static IList<string> ContainerHideButton { get; set; } = new List<string>();
        
        /// <summary>
        /// Default classes for container headers (Tag container, Archive container)
        /// </summary>
        public static IList<string> ContainerHeader { get; set; } = new List<string>
        {
            "opta-container-header",
        };

        /// <summary>
        /// Default classes for the name of the header.
        /// </summary>
        public static IList<string> ContainerHeaderName { get; set; } = new List<string>
        {
            "opta-container-headername",
        };

        /// <summary>
        /// Default classes for the name of the header.
        /// </summary>
        public static IList<string> ContainerContent { get; set; } = new List<string>
        {
            "opta-container-content",
        };

        /// <summary>
        /// Default classes for container displaying tags
        /// </summary>
        public static IList<string> TagContainer { get; set; } = new List<string>
        {
            "opta-tag-container",
        };

        /// <summary>
        /// Default classes for container displaying the archive
        /// </summary>
        public static IList<string> ArchiveContainer { get; set; } = new List<string>();

        /// <summary>
        /// Default classes for container displaying the archive
        /// </summary>
        public static IList<string> TableOfContentsContainer { get; set; } = new List<string>()
        {
            "opta-container"
        };

        /// <summary>
        /// Default class for diplaying posts in compact mode.
        /// </summary>
        public static IList<string> CompactMode { get; set; } = new List<string>
        {
            "opta-compact-mode"
        };

        /// <summary>
        /// Default classes for the various <see cref="CodePart"/> in a piece of code to better clarify code
        /// </summary>
        public static IDictionary<CodePart, string> CodeClasses { get; set; } = new Dictionary<CodePart, string>
        {
            { CodePart.Keyword, "opta-code-keyword" },
            { CodePart.ControlKeyword, "opta-code-controlkeyword" },
            { CodePart.Method, "opta-code-method" },
            { CodePart.String, "opta-code-string" },
            { CodePart.Comment, "opta-code-comment" },
            { CodePart.Attribute, "opta-code-attribute" },
            { CodePart.Component, "opta-code-component" },
            { CodePart.Directive, "opta-code-directive" },
            { CodePart.TagDelimiter, "opta-code-tagdelimiter" }
        };

        /// <summary>
        /// Default classes for the default colors of the components
        /// </summary>
        public static IDictionary<BlogColor, string> ColorClasses { get; set; } = new Dictionary<BlogColor, string>
        {
            { BlogColor.Link, "opta-color-link" },
            { BlogColor.Header, "opta-color-header" },
            { BlogColor.Text, "opta-color-text" },
            { BlogColor.Quote, "opta-color-quote" },
            { BlogColor.Subtle, "opta-color-subtle" },
        };

        /// <summary>
        /// Default classes for the various styles used by components
        /// </summary>
        public static IDictionary<Style, string> StyleClasses { get; set; } = new Dictionary<Style, string>
        {
            { Style.Thin, "opta-style-thin" },
            { Style.Bold, "opta-style-bold" },
            { Style.Italic, "opta-style-italic" },
            { Style.Underline, "opta-style-underline" },
            { Style.StrikeThrough, "opta-style-strikethrough" },
            { Style.LowerCase, "opta-style-lowercase" },
            { Style.UpperCase, "opta-style-uppercase" },
            { Style.Monospace, "opta-style-monospace" },
            { Style.KeepWhiteSpace, "opta-style-keepwhitespace" },
            { Style.NoDecoration, "opta-style-none" },
        };

        /// <summary>
        /// Default classes for the borders to set on components
        /// </summary>
        public static IDictionary<Side, string> BorderClasses { get; set; } = new Dictionary<Side, string>
        {
            { Side.Top, "opta-border-top" },
            { Side.Right, "opta-border-right" },
            { Side.Bottom, "opta-border-bottom" },
            { Side.Left, "opta-border-left" },
            { Side.X, "opta-border-x"},
            { Side.Y, "opta-border-y" },
            { Side.All, "opta-border" }
        };

        /// <summary>
        /// Default classes for the border radius to set on components
        /// </summary>
        public static IDictionary<Side, string> BorderRadiusClasses { get; set; } = new Dictionary<Side, string>
        {
            { Side.All, "opta-borderradius" },
            { Side.Top | Side.Left, "opta-borderradius-topleft" },
            { Side.Top | Side.Right, "opta-borderradius-topright" },
            { Side.Bottom | Side.Left, "opta-borderradius-bottomleft" },
            { Side.Bottom | Side.Right, "opta-borderradius-bottomright" }
        };

        /// <summary>
        /// Default classes for the margin to be set on components
        /// </summary>
        public static IDictionary<Side, IDictionary<Strength, string>> MarginClasses { get; set; } = new Dictionary<Side, IDictionary<Strength, string>>
        {
            { 
                Side.Top, new Dictionary<Strength, string>
                {
                    { Strength.One, "opta-margin-top-1" },
                    { Strength.Two, "opta-margin-top-2" },
                    { Strength.Three, "opta-margin-top-3" },
                    { Strength.Four, "opta-margin-top-4" },
                    { Strength.Five, "opta-margin-top-5" },
                    { Strength.Auto, "opta-margin-top-auto" },
                    { Strength.MinusOne, "opta-neg-margin-top-1" },
                    { Strength.MinusTwo, "opta-neg-margin-top-2" },
                    { Strength.MinusThree, "opta-neg-margin-top-3" },
                    { Strength.MinusFour, "opta-neg-margin-top-4" },
                    { Strength.MinusFive, "opta-neg-margin-top-5" },
                }
            },
            {
                Side.Right, new Dictionary<Strength, string>
                {
                    { Strength.One, "opta-margin-right-1" },
                    { Strength.Two, "opta-margin-right-2" },
                    { Strength.Three, "opta-margin-right-3" },
                    { Strength.Four, "opta-margin-right-4" },
                    { Strength.Five, "opta-margin-right-5" },
                    { Strength.Auto, "opta-margin-right-auto" },
                    { Strength.MinusOne, "opta-neg-margin-right-1" },
                    { Strength.MinusTwo, "opta-neg-margin-right-2" },
                    { Strength.MinusThree, "opta-neg-margin-right-3" },
                    { Strength.MinusFour, "opta-neg-margin-right-4" },
                    { Strength.MinusFive, "opta-neg-margin-right-5" },
                }
            },
            {
                Side.Bottom, new Dictionary<Strength, string>
                {
                    { Strength.One, "opta-margin-bottom-1" },
                    { Strength.Two, "opta-margin-bottom-2" },
                    { Strength.Three, "opta-margin-bottom-3" },
                    { Strength.Four, "opta-margin-bottom-4" },
                    { Strength.Five, "opta-margin-bottom-5" },
                    { Strength.Auto, "opta-margin-bottom-auto" },
                    { Strength.MinusOne, "opta-neg-margin-bottom-1" },
                    { Strength.MinusTwo, "opta-neg-margin-bottom-2" },
                    { Strength.MinusThree, "opta-neg-margin-bottom-3" },
                    { Strength.MinusFour, "opta-neg-margin-bottom-4" },
                    { Strength.MinusFive, "opta-neg-margin-bottom-5" },
                }
            },
            {
                Side.Left, new Dictionary<Strength, string>
                {
                    { Strength.One, "opta-margin-left-1" },
                    { Strength.Two, "opta-margin-left-2" },
                    { Strength.Three, "opta-margin-left-3" },
                    { Strength.Four, "opta-margin-left-4" },
                    { Strength.Five, "opta-margin-left-5" },
                    { Strength.Auto, "opta-margin-left-auto" },
                    { Strength.MinusOne, "opta-neg-margin-left-1" },
                    { Strength.MinusTwo, "opta-neg-margin-left-2" },
                    { Strength.MinusThree, "opta-neg-margin-left-3" },
                    { Strength.MinusFour, "opta-neg-margin-left-4" },
                    { Strength.MinusFive, "opta-neg-margin-left-5" },
                }
            },
            {
                Side.X, new Dictionary<Strength, string>
                {
                    { Strength.One, "opta-margin-x-1" },
                    { Strength.Two, "opta-margin-x-2" },
                    { Strength.Three, "opta-margin-x-3" },
                    { Strength.Four, "opta-margin-x-4" },
                    { Strength.Five, "opta-margin-x-5" },
                    { Strength.Auto, "opta-margin-x-auto" },
                    { Strength.MinusOne, "opta-neg-margin-x-1" },
                    { Strength.MinusTwo, "opta-neg-margin-x-2" },
                    { Strength.MinusThree, "opta-neg-margin-x-3" },
                    { Strength.MinusFour, "opta-neg-margin-x-4" },
                    { Strength.MinusFive, "opta-neg-margin-x-5" },
                }
            },
            {
                Side.Y, new Dictionary<Strength, string>
                {
                    { Strength.One, "opta-margin-y-1" },
                    { Strength.Two, "opta-margin-y-2" },
                    { Strength.Three, "opta-margin-y-3" },
                    { Strength.Four, "opta-margin-y-4" },
                    { Strength.Five, "opta-margin-y-5" },
                    { Strength.Auto, "opta-margin-y-auto" },
                    { Strength.MinusOne, "opta-neg-margin-y-1" },
                    { Strength.MinusTwo, "opta-neg-margin-y-2" },
                    { Strength.MinusThree, "opta-neg-margin-y-3" },
                    { Strength.MinusFour, "opta-neg-margin-y-4" },
                    { Strength.MinusFive, "opta-neg-margin-y-5" },
                }
            },
            {
                Side.All, new Dictionary<Strength, string>
                {
                    { Strength.One, "opta-margin-1" },
                    { Strength.Two, "opta-margin-2" },
                    { Strength.Three, "opta-margin-3" },
                    { Strength.Four, "opta-margin-4" },
                    { Strength.Five, "opta-margin-5" },
                    { Strength.Auto, "opta-margin-auto" },
                    { Strength.MinusOne, "opta-neg-margin-1" },
                    { Strength.MinusTwo, "opta-neg-margin-2" },
                    { Strength.MinusThree, "opta-neg-margin-3" },
                    { Strength.MinusFour, "opta-neg-margin-4" },
                    { Strength.MinusFive, "opta-neg-margin-5" },
                }
            }
        };

        /// <summary>
        /// Default classes for the padding to be set on components
        /// </summary>
        public static IDictionary<Side, IDictionary<Strength, string>> PaddingClasses { get; set; } = new Dictionary<Side, IDictionary<Strength, string>>
        {
            {
                Side.Top, new Dictionary<Strength, string>
                {
                    { Strength.One, "opta-padding-top-1" },
                    { Strength.Two, "opta-padding-top-2" },
                    { Strength.Three, "opta-padding-top-3" },
                    { Strength.Four, "opta-padding-top-4" },
                    { Strength.Five, "opta-padding-top-5" },
                }
            },
            {
                Side.Right, new Dictionary<Strength, string>
                {
                    { Strength.One, "opta-padding-right-1" },
                    { Strength.Two, "opta-padding-right-2" },
                    { Strength.Three, "opta-padding-right-3" },
                    { Strength.Four, "opta-padding-right-4" },
                    { Strength.Five, "opta-padding-right-5" },
                }
            },
            {
                Side.Bottom, new Dictionary<Strength, string>
                {
                    { Strength.One, "opta-padding-bottom-1" },
                    { Strength.Two, "opta-padding-bottom-2" },
                    { Strength.Three, "opta-padding-bottom-3" },
                    { Strength.Four, "opta-padding-bottom-4" },
                    { Strength.Five, "opta-padding-bottom-5" },
                }
            },
            {
                Side.Left, new Dictionary<Strength, string>
                {
                    { Strength.One, "opta-padding-left-1" },
                    { Strength.Two, "opta-padding-left-2" },
                    { Strength.Three, "opta-padding-left-3" },
                    { Strength.Four, "opta-padding-left-4" },
                    { Strength.Five, "opta-padding-left-5" },
                }
            },
            {
                Side.X, new Dictionary<Strength, string>
                {
                    { Strength.One, "opta-padding-x-1" },
                    { Strength.Two, "opta-padding-x-2" },
                    { Strength.Three, "opta-padding-x-3" },
                    { Strength.Four, "opta-padding-x-4" },
                    { Strength.Five, "opta-padding-x-5" },
                }
            },
            {
                Side.Y, new Dictionary<Strength, string>
                {
                    { Strength.One, "opta-padding-y-1" },
                    { Strength.Two, "opta-padding-y-2" },
                    { Strength.Three, "opta-padding-y-3" },
                    { Strength.Four, "opta-padding-y-4" },
                    { Strength.Five, "opta-padding-y-5" },
                }
            },
            {
                Side.All, new Dictionary<Strength, string>
                {
                    { Strength.One, "opta-padding-1" },
                    { Strength.Two, "opta-padding-2" },
                    { Strength.Three, "opta-padding-3" },
                    { Strength.Four, "opta-padding-4" },
                    { Strength.Five, "opta-padding-5" },
                }
            },
        };

        /// <summary>
        /// Default classes for the various list styles used by the <see cref="List.List"/> component
        /// </summary>
        public static IDictionary<ListStyle, string> ListStyleClasses { get; set; } = new Dictionary<ListStyle, string>
        {
            { ListStyle.None, "opta-list-style-none" },
            { ListStyle.Circle, "opta-list-style-circle" },
            { ListStyle.OpenCircle, "opta-list-style-opencircle" },
            { ListStyle.Square, "opta-list-style-square" },
            { ListStyle.DisclosureOpen, "opta-list-style-disclosureopen" },
            { ListStyle.DisclosureClosed, "opta-list-style-disclosureclosed" },
            { ListStyle.Numeric, "opta-list-style-numeric" },
            { ListStyle.LowerAlpha, "opta-list-style-loweralpha" },
            { ListStyle.UpperAlpha, "opta-list-style-upperalpha" },
            { ListStyle.UpperRoman, "opta-list-style-upperroman" },
        };

        /// <summary>
        /// Default classes for the text alignment inside a block
        /// </summary>
        public static IDictionary<PositionType, string> TextAlignmentClasses { get; set; } = new Dictionary<PositionType, string>
        {
            { PositionType.Left, "opta-text-left" },
            { PositionType.Right, "opta-text-right" },
            { PositionType.Center, "opta-text-center" },
        };

        /// <summary>
        /// Default classes for the alignment of a block
        /// </summary>
        public static IDictionary<PositionType, string> BlockAlignmentClasses { get; set; } = new Dictionary<PositionType, string>
        {
            { PositionType.Left, "opta-block-left" },
            { PositionType.Right, "opta-block-right" },
            { PositionType.Center, "opta-block-center" }
        };

        /// <summary>
        /// Optional classes for each Content class, they are injected through the builders of the various content classes. If you directly create content classes you have to insert these yourself
        /// </summary>
        public static IDictionary<Type, IList<string>> ContentClasses { get; set; } = new Dictionary<Type, IList<string>>();       

        /// <summary>
        /// Adds multiple values to the list if not already present.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="values"></param>
        public static void AddRange(this IList<string> list, params string[] values)
        {
            foreach(var value in values)
            {
                if (!list.Contains(value))
                {
                    list.Add(value);
                }                
            }
        }
    }
}
