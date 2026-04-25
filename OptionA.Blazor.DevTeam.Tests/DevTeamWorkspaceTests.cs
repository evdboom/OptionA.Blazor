using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using Xunit;

/// <summary>
/// Fixture-based integrity tests for the .devteam workspace state snapshots.
/// Tests use embedded JSON fixtures so they never depend on the live (mutable)
/// .devteam workspace files and remain stable in CI regardless of runtime progress.
/// </summary>
public class DevTeamWorkspaceTests
{
    // -------------------------------------------------------------------------
    // Fixture loading helpers
    // -------------------------------------------------------------------------

    private static string LoadFixture(string relativeResourcePath)
    {
        var assembly = Assembly.GetExecutingAssembly();
        // Resource names use dots instead of path separators; dashes become underscores.
        var resourceSuffix = relativeResourcePath
            .Replace('\\', '.')
            .Replace('/', '.')
            .Replace('-', '_');
        var resourceName = "OptionA.Blazor.DevTeam.Tests.Fixtures." + resourceSuffix;
        using var stream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Embedded resource not found: {resourceName}");
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    private static JsonDocument LoadFixtureJson(string relativeResourcePath)
        => JsonDocument.Parse(LoadFixture(relativeResourcePath));

    // -------------------------------------------------------------------------
    // Integrity-check helpers (pure logic, no file-system access)
    // -------------------------------------------------------------------------

    /// <summary>
    /// Returns the set of issue IDs that are marked Done in the given issues JSON.
    /// </summary>
    private static HashSet<int> GetDoneIssueIds(JsonDocument issuesDoc)
    {
        var result = new HashSet<int>();
        foreach (var issue in issuesDoc.RootElement.EnumerateArray())
        {
            if (issue.TryGetProperty("Status", out var statusProp)
                && NormalizeStatus(statusProp.GetString() ?? "") == "done"
                && issue.TryGetProperty("Id", out var idProp))
            {
                result.Add(idProp.GetInt32());
            }
        }
        return result;
    }

    /// <summary>
    /// For each issue in <paramref name="doneIssueIds"/>, returns the
    /// ResultingIssueStatus of the latest run (highest Id) in runsDoc.
    /// Issues with no run are omitted.
    /// </summary>
    private static Dictionary<int, string?> GetLatestRunResultingStatus(
        JsonDocument runsDoc, HashSet<int> doneIssueIds)
    {
        // Collect latest run per issue
        var latestRunByIssue = new Dictionary<int, (int RunId, string? ResultingStatus)>();
        foreach (var run in runsDoc.RootElement.EnumerateArray())
        {
            if (!run.TryGetProperty("IssueId", out var issueIdProp)) continue;
            var issueId = issueIdProp.GetInt32();
            if (!doneIssueIds.Contains(issueId)) continue;

            run.TryGetProperty("Id", out var runIdProp);
            var runId = runIdProp.GetInt32();

            string? resulting = null;
            if (run.TryGetProperty("ResultingIssueStatus", out var resProp)
                && resProp.ValueKind == JsonValueKind.String)
            {
                resulting = resProp.GetString();
            }

            if (!latestRunByIssue.TryGetValue(issueId, out var existing)
                || runId > existing.RunId)
            {
                latestRunByIssue[issueId] = (runId, resulting);
            }
        }

        return latestRunByIssue.ToDictionary(
            kv => kv.Key,
            kv => kv.Value.ResultingStatus);
    }

    /// <summary>
    /// Returns (questionId, questionText) pairs for all Open questions whose
    /// text references an issue number that is in <paramref name="doneIssueIds"/>.
    /// </summary>
    private static List<(int QuestionId, string Text)> FindStaleOpenQuestions(
        JsonDocument questionsDoc, HashSet<int> doneIssueIds)
    {
        var stale = new List<(int, string)>();
        var issueRefPattern = new Regex(@"#(\d+)", RegexOptions.Compiled);

        foreach (var q in questionsDoc.RootElement.EnumerateArray())
        {
            if (!q.TryGetProperty("Status", out var statusProp)) continue;
            if (NormalizeStatus(statusProp.GetString() ?? "") != "open") continue;

            q.TryGetProperty("Id", out var idProp);
            q.TryGetProperty("Text", out var textProp);
            var text = textProp.GetString() ?? "";

            foreach (Match m in issueRefPattern.Matches(text))
            {
                var refId = int.Parse(m.Groups[1].Value);
                if (doneIssueIds.Contains(refId))
                {
                    stale.Add((idProp.GetInt32(), text));
                    break;
                }
            }
        }

        return stale;
    }

    private static string NormalizeStatus(string status)
        => new string(status.Where(char.IsLetterOrDigit).ToArray()).ToLowerInvariant();

    // -------------------------------------------------------------------------
    // Valid-workspace fixture: all rules should pass
    // -------------------------------------------------------------------------

    [Fact]
    public void ValidWorkspace_NoDoneIssueRunStatusContradictions()
    {
        using var issues = LoadFixtureJson("valid-workspace.issues.json");
        using var runs   = LoadFixtureJson("valid-workspace.runs.json");

        var doneIds = GetDoneIssueIds(issues);
        var latestStatus = GetLatestRunResultingStatus(runs, doneIds);

        var contradictions = latestStatus
            .Where(kv => NormalizeStatus(kv.Value ?? "") == "inprogress")
            .Select(kv => kv.Key)
            .ToList();

        Assert.Empty(contradictions);
    }

    [Fact]
    public void ValidWorkspace_NoStaleOpenQuestionsAboutDoneIssues()
    {
        using var issues    = LoadFixtureJson("valid-workspace.issues.json");
        using var questions = LoadFixtureJson("valid-workspace.questions.json");

        var doneIds = GetDoneIssueIds(issues);
        var stale = FindStaleOpenQuestions(questions, doneIds);

        Assert.Empty(stale);
    }

    [Fact]
    public void ValidWorkspace_AllRunIssueIdsExistInIssues()
    {
        using var issues = LoadFixtureJson("valid-workspace.issues.json");
        using var runs   = LoadFixtureJson("valid-workspace.runs.json");

        var issueIds = new HashSet<int>();
        foreach (var issue in issues.RootElement.EnumerateArray())
        {
            if (issue.TryGetProperty("Id", out var idProp))
                issueIds.Add(idProp.GetInt32());
        }

        var danglingRunIds = new List<int>();
        foreach (var run in runs.RootElement.EnumerateArray())
        {
            if (run.TryGetProperty("IssueId", out var issueIdProp)
                && !issueIds.Contains(issueIdProp.GetInt32()))
            {
                run.TryGetProperty("Id", out var runIdProp);
                danglingRunIds.Add(runIdProp.GetInt32());
            }
        }

        Assert.Empty(danglingRunIds);
    }

    [Fact]
    public void ValidWorkspace_NoDuplicateIssueIds()
    {
        using var issues = LoadFixtureJson("valid-workspace.issues.json");

        var seen  = new HashSet<int>();
        var dupes = new List<int>();

        foreach (var issue in issues.RootElement.EnumerateArray())
        {
            if (!issue.TryGetProperty("Id", out var idProp)) continue;
            var id = idProp.GetInt32();
            if (!seen.Add(id))
                dupes.Add(id);
        }

        Assert.Empty(dupes);
    }

    // -------------------------------------------------------------------------
    // Contradictory-done-inprogress fixture: detector must flag the contradiction
    // (mirrors the actual state found in the audit: issues 18/19 marked Done
    //  while the latest run for each still shows ResultingIssueStatus=InProgress)
    // -------------------------------------------------------------------------

    [Fact]
    public void ContradictoryFixture_DetectsIssuesDoneButRunsShowInProgress()
    {
        using var issues = LoadFixtureJson("contradictory-done-inprogress.issues.json");
        using var runs   = LoadFixtureJson("contradictory-done-inprogress.runs.json");

        var doneIds = GetDoneIssueIds(issues);
        var latestStatus = GetLatestRunResultingStatus(runs, doneIds);

        var contradictions = latestStatus
            .Where(kv => NormalizeStatus(kv.Value ?? "") == "inprogress")
            .Select(kv => kv.Key)
            .OrderBy(id => id)
            .ToList();

        // Fixture intentionally captures the audit finding: issues 18 and 19
        // are Done in issues.json but their latest runs say InProgress.
        Assert.Equal(new[] { 18, 19 }, contradictions);
    }

    // -------------------------------------------------------------------------
    // Stale-open-questions fixture: detector must flag the stale questions
    // (mirrors the actual state found in the audit: questions.md still asks
    //  whether issues 3/8 are complete even though issues.json says Done)
    // -------------------------------------------------------------------------

    [Fact]
    public void StaleQuestionsFixture_DetectsOpenQuestionsAboutDoneIssues()
    {
        using var issues    = LoadFixtureJson("stale-open-questions.issues.json");
        using var questions = LoadFixtureJson("stale-open-questions.questions.json");

        var doneIds = GetDoneIssueIds(issues);
        var stale = FindStaleOpenQuestions(questions, doneIds);

        // Both open questions reference issues 3 and/or 8, which are Done.
        Assert.NotEmpty(stale);
        Assert.Equal(2, stale.Count);
        Assert.Contains(stale, q => q.QuestionId == 1);
        Assert.Contains(stale, q => q.QuestionId == 2);
    }
}

