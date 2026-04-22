using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Xunit;

public class DevTeamWorkspaceTests
{
    private static string FindRepoRoot()
    {
        var dir = new DirectoryInfo(Environment.CurrentDirectory);
        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir.FullName, "OptionA.Blazor.sln")))
                return dir.FullName;
            dir = dir.Parent;
        }
        throw new InvalidOperationException("Repository root (OptionA.Blazor.sln) not found from " + Environment.CurrentDirectory);
    }

    [Fact]
    public void Issue16_Is_Marked_Done_In_State_And_Markdown()
    {
        var repoRoot = FindRepoRoot();
        var issuesJsonPath = Path.Combine(repoRoot, ".devteam", "state", "issues.json");
        Assert.True(File.Exists(issuesJsonPath), $"File not found: {issuesJsonPath}");
        using var doc = JsonDocument.Parse(File.ReadAllText(issuesJsonPath));
        bool found = false;
        foreach (var elem in doc.RootElement.EnumerateArray())
        {
            if (elem.TryGetProperty("Id", out var idProp) && idProp.GetInt32() == 16)
            {
                found = true;
                Assert.True(elem.TryGetProperty("Status", out var statusProp), "Status property missing for issue 16");
                var status = statusProp.GetString();
                Assert.Equal("Done", status);
            }
        }
        Assert.True(found, "Issue 16 not found in issues.json");

        var mdPath = Path.Combine(repoRoot, ".devteam", "issues", "0016-implement-the-technical-approach-and-create-execution-issues.md");
        Assert.True(File.Exists(mdPath), $"File not found: {mdPath}");
        var mdLines = File.ReadAllLines(mdPath);
        var statusLine = mdLines.FirstOrDefault(l => l.StartsWith("- Status:", StringComparison.OrdinalIgnoreCase));
        Assert.NotNull(statusLine);
        Assert.Contains("done", statusLine, StringComparison.OrdinalIgnoreCase);
    }
}
