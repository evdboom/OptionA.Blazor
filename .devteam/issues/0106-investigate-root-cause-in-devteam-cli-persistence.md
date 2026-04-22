# Issue 0106: Investigate root cause in devteam CLI persistence

- Status: done
- Role: developer
- Area: devteam
- Priority: 80
- Depends On: none
- Roadmap Item: 1
- Family: devteam
- External: none
- Pipeline: 72
- Pipeline Stage: 0
- Planning Issue: no

## Detail

Root cause likely in devteam CLI not persisting in-workspace changes to exported state after batch-close operations. Assign to whoever maintains the devteam CLI: reproduce batch-close -> inspect CLI code paths that write workspace state and export

## Latest Run

- Run: 73
- Status: Completed
- Model: gpt-5-mini
- Session: devteam-developer-a563a0ac108e
- Updated: 2026-04-22T17:37:10.6704074+00:00
- Summary: Reproduced the batch-close/export mismatch. Running .devteam\scripts\test-batch-close.ps1 shows exported snapshot (.devteam/exported/state/issues.json) lags authoritative state (.devteam/state/issues.json). Investigation shows the workspace was modified (patch-batch-close wrote Done statuses) after export; exported snapshot was not regenerated. Root cause: workspace edits are not followed by an export step, so the exported archive can remain stale. Short-term fix: run devteam export after batch-close/edits. Longer-term: add automatic re-export or make devteam CLI emit updated export.
- Skills Used: verify
- Tools Used: functions.report_intent (Exploring codebase; Reproducing batch-close), functions.grep (repo search), functions.view (inspected .devteam scripts and state files), functions.powershell: pwsh -NoProfile -NonInteractive -ExecutionPolicy Bypass -File .\.devteam\scripts\test-batch-close.ps1 (reproduced failure), Get-Item to compare timestamps
- Changed Files: none

## Recent Decisions

- #165 [run] Run #73 Completed: Reproduced the batch-close/export mismatch. Running .devteam\scripts\test-batch-close.ps1 shows exported snapshot (.devteam/exported/state/issues.json) lags authoritative state (.devteam/state/issues.json). Investigation shows the workspace was modified (patch-batch-close wrote Done statuses) after export; exported snapshot was not regenerated. Root cause: workspace edits are not followed by an export step, so the exported archive can remain stale. Short-term fix: run devteam export after batch-close/edits. Longer-term: add automatic re-export or make devteam CLI emit updated export.