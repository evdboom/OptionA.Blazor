# Open questions

## Question 1

- Type: non-blocking
- Status: Open
- Asked: 2026-04-25T20:47:46.7614657+00:00

Issues #3 and #8 do not appear in the current candidate list — are they completed? Confirming would unlock #16 (audit) and potentially #13.

## Question 2

- Type: non-blocking
- Status: Open
- Asked: 2026-04-25T20:52:56.7118613+00:00

Are issues #3 and #8 completed? Confirming their status would fully unblock #16 (audit) and should close open question #1.

## Question 3

- Type: non-blocking
- Status: Open
- Asked: 2026-04-25T21:32:27.6347862+00:00

Confirm the chosen authoritative policy (use .devteam/state/issues.json as authoritative). If another policy is preferred (runs-as-source), the refinement will be updated accordingly.

## Question 4

- Type: non-blocking
- Status: Open
- Asked: 2026-04-25T21:32:27.6347886+00:00

Prefer a PowerShell script (.devteam/scripts/reconcile-workspace-state.ps1) or a small dotnet tool project (OptionA.Blazor.DevTeam.Tooling) for the implementation? (default: script for speed; project for stronger typing/versioning)

## Question 5

- Type: non-blocking
- Status: Open
- Asked: 2026-04-25T21:39:08.7243303+00:00

Open questions #1–#4 remain unresolved but are all non-blocking. Question #3 (authoritative policy for issues.json) and #4 (script vs dotnet tool) are directly relevant to #47's implementation — the agent executing #47 should pick defaults (issues.json as authoritative, PowerShell script) unless user intervenes.

## Question 6

- Type: non-blocking
- Status: Open
- Asked: 2026-04-25T21:40:36.0501949+00:00

Should issue #50 be marked Done in the MCP now that tests are green, or leave it open for a follow-up audit?

## Question 7

- Type: non-blocking
- Status: Open
- Asked: 2026-04-25T21:56:16.2406315+00:00

#58, #39, #33 are all bUnit test issues for document-rendering front-matter — once #57 compile errors are fixed, should these be merged into a single consolidated test issue to avoid triple-coverage overlap?

## Question 8

- Type: non-blocking
- Status: Open
- Asked: 2026-04-25T22:00:30.4622976+00:00

Question #7 remains open: issues #58, #39, and #33 all write bUnit tests for document-rendering front-matter and risk triple-coverage overlap. Consider consolidating into one issue before any of them run.

## Question 9

- Type: non-blocking
- Status: Open
- Asked: 2026-04-25T22:02:56.8020827+00:00

Prefer applying the small test API update now (patch tests to use Render) or schedule to developer issue above?

## Question 10

- Type: non-blocking
- Status: Open
- Asked: 2026-04-25T22:04:19.0656795+00:00

Confirm authoritative policy: prefer .devteam/state/issues.json as the authoritative source for reconciliation (script defaults to this).

## Question 11

- Type: non-blocking
- Status: Open
- Asked: 2026-04-25T22:04:19.0657583+00:00

Should a CI job be added to run the script in real (non-dry-run) mode on-demand, or keep it dry-run-only in CI and require manual approval for real runs?

