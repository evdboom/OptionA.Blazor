# Issue 0068: Add deterministic reconciliation script and backups for .devteam state

- Status: done
- Role: developer
- Area: repo-automation
- Priority: 85
- Depends On: 0056
- Roadmap Item: 1
- Family: repoautomation
- External: none
- Pipeline: 44
- Pipeline Stage: 0
- Planning Issue: no

## Detail

What: Implement a deterministic, idempotent reconciliation script that can rebuild .devteam/state/runs.json and other derived state from raw artifacts (e.g., .devteam/runs/*.md, .devteam/state/issues.json) and creates timestamped backups before mutating state. Why: restores accidental deletions and makes recovery repeatable. How: provide a PowerShell script (preferred for speed) at .devteam/scripts/reconcile-workspace-state.ps1 that:1) Verifies authoritative source (issues.json),2) Backs up existing state files to .devteam/state/*.bak.YYYYMMDDHHMMSS,3) Re-generates runs.json and workspace.json deterministically from runs/ and issues.json,4) Writes a reconciliation log with checksum and git status snapshot. Include unit/integration tests (PowerShell Pester or small dotnet test harness) that exercise restore, backup, and idempotency. Acceptance criteria: script added, tested locally via CI job that runs in a dry-run mode, produces backups, and succeeds on idempotent runs. FilesInScope: .devteam/scripts/reconcile-workspace-state.ps1, .devteam/state/*.bak (pattern), .devteam/state/runs.json, .devteam/runs/*.md, tests/ or .devteam/tests for script. Linked decisions: authoritative policy from previous issue.

## Latest Run

- Run: 49
- Status: Completed
- Model: gpt-5-mini
- Session: devteam-developer-14fe3413d826
- Updated: 2026-04-25T22:04:19.0658361+00:00
- Summary: Added a deterministic PowerShell reconciliation script and Pester tests. Files created:
- .devteam/scripts/reconcile-workspace-state.ps1 — accepts -WorkspaceRoot and -DryRun, verifies .devteam/state/issues.json, backs up only when content changes, regenerates runs.json and workspace.json deterministically, writes reconcile log with SHA256 checksum and git snapshot.
- .devteam/tests/reconcile.Tests.ps1 — Pester tests exercising restore, backup, and idempotency using a temporary test workspace.
- Skills Used: (none)
- Tools Used: functions.report_intent- functions.powershell- functions.create- functions.read_powershell- functions.stop_powershell
- Changed Files: none

## Recent Decisions

(none)