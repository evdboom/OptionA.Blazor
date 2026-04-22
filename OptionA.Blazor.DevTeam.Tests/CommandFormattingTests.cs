using System;
using Xunit;
using System.Text.RegularExpressions;

public class CommandFormattingTests
{
    private static string FormatCommand(string input)
    {
        var rx = new Regex(@"devteam\s+edit-issue(\d+)([^\r\n`]*)", RegexOptions.IgnoreCase);
        var m = rx.Match(input);
        if (!m.Success) return input;
        var id = m.Groups[1].Value;
        var rest = m.Groups[2].Value.Trim();

        // Parse --workspace token robustly to support unquoted paths with spaces
        var wsToken = "--workspace";
        var idx = rest.IndexOf(wsToken, StringComparison.InvariantCultureIgnoreCase);
        if (idx >= 0)
        {
            var after = rest.Substring(idx + wsToken.Length).TrimStart();
            string wsVal = null;
            string prefix = rest.Substring(0, idx);
            string suffix = string.Empty;
            if (after.StartsWith("\""))
            {
                var end = after.IndexOf('"', 1);
                if (end > 0) { wsVal = after.Substring(1, end - 1); suffix = after.Substring(end + 1); }
                else { wsVal = after.Trim('"'); }
            }
            else
            {
                var m2 = Regex.Match(after, "(.+?)(?=\\s--\\w|$)");
                if (m2.Success) { wsVal = m2.Groups[1].Value.Trim(); suffix = after.Substring(m2.Length); }
            }

            if (!string.IsNullOrEmpty(wsVal) && wsVal.Contains(" "))
            {
                var escaped = wsVal.Replace("\"", "\\\"");
                rest = $"{prefix}--workspace \"{escaped}\"{suffix}";
            }
        }

        return $"devteam edit-issue {id} {rest}";
    }

    [Fact]
    public void AddsSpaceBetweenEditIssueAndId()
    {
        var input = "devteam edit-issue16 --status done --note \"fixed\" --workspace C:\\repo\\OptionA.Blazor";
        var expected = "devteam edit-issue 16 --status done --note \"fixed\" --workspace C:\\repo\\OptionA.Blazor";
        Assert.Equal(expected, FormatCommand(input));
    }

    [Fact]
    public void QuotesWorkspaceIfContainsSpaces()
    {
        var input = "devteam edit-issue42 --status done --workspace C:\\path with spaces\\repo";
        var expected = "devteam edit-issue 42 --status done --workspace \"C:\\path with spaces\\repo\"";
        Assert.Equal(expected, FormatCommand(input));
    }

    [Fact]
    public void PreservesAlreadyQuotedWorkspace()
    {
        var input = "devteam edit-issue7 --status done --workspace \"C:\\already quoted\"";
        var expected = "devteam edit-issue 7 --status done --workspace \"C:\\already quoted\"";
        Assert.Equal(expected, FormatCommand(input));
    }
}
