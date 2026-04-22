# Decision 165

- Source: run
- Issue: 106
- Run: 73
- Session: devteam-developer-a563a0ac108e
- Created: 2026-04-22T17:37:10.6704257+00:00

## Title

Run #73 Completed

## Detail

Reproduced the batch-close/export mismatch. Running .devteam\scripts\test-batch-close.ps1 shows exported snapshot (.devteam/exported/state/issues.json) lags authoritative state (.devteam/state/issues.json). Investigation shows the workspace was modified (patch-batch-close wrote Done statuses) after export; exported snapshot was not regenerated. Root cause: workspace edits are not followed by an export step, so the exported archive can remain stale. Short-term fix: run devteam export after batch-close/edits. Longer-term: add automatic re-export or make devteam CLI emit updated export.

## Changed Files

(none)