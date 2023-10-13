namespace OptionA.Blazor.Blog.Code.Parsers
{
    /// <summary>
    /// Parser for html code (and some razor)
    /// </summary>
    public class HtmlParser : ParserBase
    {
        /// <inheritdoc/>
        public override CodeLanguage Language => CodeLanguage.Html;

        private bool _insideTag;
        private bool _directiveStarted;

        private readonly List<string> _htmlElements = new()
        {
            "html",
            "base",
            "head",
            "link",
            "meta",
            "style",
            "title",
            "body",
            "address",
            "article",
            "aside",
            "footer",
            "header",
            "h1",
            "h2",
            "h3",
            "h4",
            "h5",
            "h6",
            "main",
            "nav",
            "section",
            "blockquote",
            "dd",
            "div",
            "dl",
            "dt",
            "figcaption",
            "figure",
            "hr",
            "li",
            "menu",
            "ol",
            "p",
            "pre",
            "ul",
            "a",
            "abbr",
            "b",
            "bdi",
            "bdo",
            "br",
            "cite",
            "code",
            "data",
            "dfn",
            "em",
            "i",
            "kbd",
            "mark",
            "q",
            "rp",
            "rt",
            "ruby",
            "s",
            "samp",
            "small",
            "span",
            "strong",
            "sub",
            "sup",
            "time",
            "u",
            "var",
            "wbr",
            "area",
            "audio",
            "img",
            "map",
            "track",
            "video",
            "embed",
            "iframe",
            "object",
            "picture",
            "portal",
            "source",
            "svg",
            "math",
            "canvas",
            "noscript",
            "script",
            "del",
            "ins",
            "caption",
            "col",
            "colgroup",
            "table",
            "tbody",
            "td",
            "tfoot",
            "th",
            "thead",
            "tr",
            "button",
            "datalist",
            "fieldset",
            "form",
            "input",
            "label",
            "legend",
            "meter",
            "optgroup",
            "option",
            "output",
            "progress",
            "select",
            "textarea",
            "details",
            "dialog",
            "summary",
            "slot",
            "template"
        };
        private readonly List<string> _htmlAttributes = new()
        {
            "accept",
            "accept-charset",
            "accesskey",
            "action",
            "align",
            "allow",
            "alt",
            "async",
            "autocapitalize",
            "autocomplete",
            "autofocus",
            "autoplay",
            "background",
            "bgcolor",
            "border",
            "buffered",
            "capture",
            "challenge",
            "charset",
            "checked",
            "cite",
            "class",
            "code",
            "codebase",
            "color",
            "cols",
            "colspan",
            "content",
            "contenteditable",
            "contextmenu",
            "controls",
            "coords",
            "crossorigin",
            "data",
            "data-*",
            "datetime",
            "decoding",
            "default",
            "defer",
            "dir",
            "dirname",
            "disabled",
            "download",
            "draggable",
            "enctype",
            "for",
            "form",
            "formaction",
            "formenctype",
            "formmethod",
            "formnovalidate",
            "formtarget",
            "headers",
            "height",
            "hidden",
            "high",
            "href",
            "hreflang",
            "http-equiv",
            "icon",
            "id",
            "integrity",
            "inputmode",
            "ismap",
            "itemprop",
            "keytype",
            "kind",
            "label",
            "lang",
            "list",
            "loop",
            "low",
            "max",
            "maxlength",
            "minlength",
            "media",
            "method",
            "min",
            "multiple",
            "muted",
            "name",
            "novalidate",
            "open",
            "optimum",
            "pattern",
            "ping",
            "placeholder",
            "poster",
            "preload",
            "radiogroup",
            "readonly",
            "referrerpolicy",
            "rel",
            "required",
            "reversed",
            "role",
            "rows",
            "rowspan",
            "sandbox",
            "scope",
            "selected",
            "shape",
            "size",
            "sizes",
            "slot",
            "span",
            "spellcheck",
            "src",
            "srcdoc",
            "srclang",
            "srcset",
            "start",
            "step",
            "style",
            "tabindex",
            "target",
            "title",
            "translate",
            "type",
            "usemap",
            "value",
            "width",
            "wrap"
        };

        private readonly Dictionary<string, string> _csharpStarters = new()
        {
            { "inherits", Environment.NewLine },
            { "if", "}" },
            { "switch", "}" },
            { "foreach", "}" },
            { "using", Environment.NewLine },
            { "page", Environment.NewLine },
            { "namespace", Environment.NewLine }
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlParser() : base()
        {
            _partCheckers.Add((code, word, previous) => IsDirective(word));
            _partCheckers.Add((code, word, previous) => IsPartOfDirective(word));
            _partCheckers.Add((code, word, previous) => IsTagStart(word));
            _partCheckers.Add((code, word, previous) => IsTag(word, previous));
            _partCheckers.Add((code, word, previous) => IsAttribute(word));
        }

        /// <inheritdoc/>
        protected override char[] Specials => new[]
        {
            '<',
            '@',
            '>',
            '=',
            '/',
            '!',
            '-'
        };

        /// <inheritdoc/>
        protected override Dictionary<string, WordTypeModel> StringStarters => new()
        {
            {  "<!--", new(WordType.Comment, "<!--", 0, "-->") }
        };


        private readonly string[] _tagStarters = new[]
        {
            "<",
            "</"
        };

        private readonly string[] _tagEnders = new[]
{
            ">",
            "/>"
        };

        private readonly string _cSharpStart = "@";

        private CodeType IsPartOfDirective(string word)
        {
            if (_directiveStarted)
            {
                _directiveStarted = false;
                return _csharpStarters.Keys.Contains(word)
                    ? CodeType.Directive
                    : CodeType.Text;
            }

            return CodeType.Text;
        }

        private CodeType IsDirective(string word)
        {
            if (word == _cSharpStart)
            {
                _directiveStarted = true;
                return CodeType.Directive;
            }
            else
            {
                return CodeType.Text;
            }

        }


        private CodeType IsTag(string word, string previous)
        {
            if (!string.IsNullOrEmpty(word) && _tagStarters.Any(s => previous.EndsWith(s)))
            {
                return _htmlElements.Contains(word)
                    ? CodeType.Keyword
                    : CodeType.Component;
            }

            return CodeType.Text;
        }

        private CodeType IsTagStart(string word)
        {
            if (_tagStarters.Any(s => word == s))
            {
                _insideTag = true;
                return CodeType.TagDelimiter;

            }
            else if (_tagEnders.Any(e => word == e))
            {
                _insideTag = false;
                return CodeType.TagDelimiter;
            }

            return CodeType.Text;
        }

        private CodeType IsAttribute(string word)
        {
            //var result = 
            return _insideTag && _htmlAttributes.Contains(word)
                ? CodeType.Attribute
                : CodeType.Text;
        }
    }
}
