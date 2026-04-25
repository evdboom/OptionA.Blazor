namespace OptionA.Blazor.Blog.Document.Internal;

/// <summary>
/// Maps a Markdig fenced-code info string to the <see cref="CodeLanguage"/> enum.
/// </summary>
internal static class CodeLanguageMapper
{
    /// <summary>
    /// Maps a language hint string from a fenced code block to the corresponding
    /// <see cref="CodeLanguage"/> value and an optional fallback hint for unknown languages.
    /// </summary>
    /// <param name="info">The raw info string from the fenced code block (may be empty).</param>
    /// <returns>
    /// A tuple of the resolved <see cref="CodeLanguage"/> and an optional other-language hint
    /// that is non-null only when <paramref name="info"/> is non-empty but unrecognised.
    /// </returns>
    internal static (CodeLanguage Language, string? OtherLanguage) Map(string info)
    {
        return info.ToLowerInvariant() switch
        {
            "csharp" or "cs" or "c#" => (CodeLanguage.CSharp, null),
            "javascript" or "js" => (CodeLanguage.Javascript, null),
            "html" => (CodeLanguage.Html, null),
            "rust" or "rs" => (CodeLanguage.Rust, null),
            "java" => (CodeLanguage.Java, null),
            "python" or "py" => (CodeLanguage.Python, null),
            "php" => (CodeLanguage.Php, null),
            "swift" => (CodeLanguage.Swift, null),
            "c" => (CodeLanguage.C, null),
            "cpp" or "c++" => (CodeLanguage.CPlusPlus, null),
            "powershell" or "ps1" => (CodeLanguage.PowerShell, null),
            "bash" or "sh" => (CodeLanguage.Bash, null),
            "" => (CodeLanguage.Other, null),
            var other => (CodeLanguage.Other, other),
        };
    }
}
