namespace OptionA.Blazor.Blog.Code.Parsers
{
    /// <summary>
    /// Parser for c# code
    /// </summary>
    public class CSharpParser : ParserBase
    {
        /// <inheritdoc/>
        public override CodeLanguage Language => CodeLanguage.CSharp;

        /// <summary>
        /// Constructor
        /// </summary>
        public CSharpParser() : base()
        {
            _partCheckers.Add((code, word, current) => IsKeyword(word));
            _partCheckers.Add((code, word, current) => IsControlKeyword(word));
            _partCheckers.Add((code, word, current) => IsMethodStart(current, code));
        }

        private readonly List<string> _controlKeywords = new()
        {
            "break",
            "case",
            "continue",
            "do",
            "else",
            "for",
            "foreach",
            "if",
            "return",
            "switch",
            "throw",
            "using",
            "try",
            "catch",
            "finally",
            "yield",
            "while",
        };
        private readonly List<string> _keyWords = new()
        {
            "abstract",
            "as",
            "base",
            "bool",
            "byte",
            "char",
            "checked",
            "class",
            "const",
            "decimal",
            "default",
            "delegate",
            "double",
            "enum",
            "event",
            "explicit",
            "extern",
            "false",
            "fixed",
            "float",
            "goto",
            "implicit",
            "in",
            "int",
            "interface",
            "internal",
            "is",
            "lock",
            "long",
            "namespace",
            "new",
            "null",
            "object",
            "operator",
            "out",
            "override",
            "params",
            "private",
            "protected",
            "public",
            "readonly",
            "ref",
            "sbyte",
            "sealed",
            "short",
            "sizeof",
            "stackalloc",
            "static",
            "string",
            "struct",
            "this",
            "true",
            "typeof",
            "uint",
            "ulong",
            "unchecked",
            "unsafe",
            "ushort",
            "virtual",
            "void",
            "volatile",
            "add",
            "and",
            "alias",
            "ascending",
            "args",
            "async",
            "await",
            "by",
            "descending",
            "dynamic",
            "equals",
            "file",
            "from",
            "get",
            "global",
            "group",
            "init",
            "into",
            "join",
            "let",
            "managed",
            "nameof",
            "nint",
            "not",
            "notnull",
            "nuint",
            "on",
            "or",
            "orderby",
            "partial",
            "record",
            "remove",
            "required",
            "scoped",
            "select",
            "set",
            "unmanaged",
            "value",
            "var",
            "when",
            "where",
            "with",
        };
        /// <inheritdoc/>
        protected override char[] Specials => new[]
        {
            '(',
            '<',
            '.',
            '\'',
            ')',
            '>',
            '{',
            '}',
            '=',
            '!',
            ';',
            ',',
            '|',
        };

        /// <inheritdoc/>
        protected override Dictionary<string, WordTypeModel> StringStarters => new()
        {
            { "\"", new(WordType.String, "\"", 1, "\"") },
            { "@\"", new(WordType.String, "@\"", 2, "\"")  },
            { "$\"", new(WordType.Interpolated, "$\"", 2, "\"") },
            { "$@\"", new(WordType.Interpolated, "$@\"", 3, "\"") },
            { "@$\"", new(WordType.Interpolated, "@$\"", 3, "\"") },
            { "//", new(WordType.Comment, "//", 0, Environment.NewLine) },
            { "///", new(WordType.Comment, "///", 0, Environment.NewLine) },
            { "/*", new(WordType.Comment, "/*", 0, "*/") },
        };

         private static CodeType IsMethodStart(string current, string code)
        {
            var nextChar = code.FirstOrDefault();
            return nextChar == '(' &&
                (string.IsNullOrEmpty(current)
                || current.EndsWith(' ')
                || current.EndsWith('.'))
                ? CodeType.Method
                : CodeType.Text;
        }

        private CodeType IsKeyword(string word)
        {
            return _keyWords.Contains(word)
                ? CodeType.Keyword
                : CodeType.Text;
        }

        private CodeType IsControlKeyword(string word)
        {
            return _controlKeywords.Contains(word)
                ? CodeType.ControlKeyword
                : CodeType.Text;
        }
    }
}
