using System.Text.RegularExpressions;

namespace OptionA.Blazor.Playground.UnitTests.Assets;

public partial class PlaygroundCssTests
{
    [Fact]
    public void PlaygroundCss_DefinesFrameworkAgnosticPlaygroundLayoutRules()
    {
        // Arrange
        var cssPath = Path.GetFullPath(Path.Combine(
            AppContext.BaseDirectory,
            "..",
            "..",
            "..",
            "..",
            "OptionA.Blazor.Playground",
            "wwwroot",
            "playground.css"));

        // Act
        Assert.True(File.Exists(cssPath), $"Expected playground stylesheet at '{cssPath}'.");
        var css = File.ReadAllText(cssPath);

        // Assert
        AssertBlockContains(css, "[opta-playground]", "display: flex", "flex-direction: row");
        AssertBlockContains(css, "[opta-playground][stacked]", "flex-direction: column");
        AssertBlockContains(css, "[opta-playground-preview]", "flex: 1", "padding:", "min-height:", "border:");
        AssertBlockContains(css, "[opta-playground-editor]", "flex: 0 0 auto", "width: 350px", "overflow-y: auto");
        AssertBlockContains(css, "[opta-playground][stacked] [opta-playground-editor]", "width: 100%");
        AssertBlockContains(css, "[opta-playground-code]", "width: 100%", "font-family:", "overflow-x: auto", "background:");
        AssertBlockContains(css, "[opta-playground-editor-field]", "margin-bottom:");
        AssertBlockContains(css, "[opta-playground-editor-group]", "font-weight:", "margin:");
    }

    private static void AssertBlockContains(string css, string selector, params string[] expectedFragments)
    {
        var match = CssBlockRegex().Match(css.Replace("\r\n", "\n"));
        while (match.Success)
        {
            if (match.Groups["selector"].Value.Trim() == selector)
            {
                var block = match.Groups["body"].Value;
                foreach (var fragment in expectedFragments)
                {
                    Assert.Contains(fragment, block, StringComparison.Ordinal);
                }

                return;
            }

            match = match.NextMatch();
        }

        throw new Xunit.Sdk.XunitException($"Expected CSS block for selector '{selector}'.");
    }

    [GeneratedRegex(@"(?ms)(?<selector>[^{]+)\{(?<body>[^}]*)\}")]
    private static partial Regex CssBlockRegex();
}
