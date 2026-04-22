# Issue 0139: CI check for exported/state parity

- Status: open
- Role: developer
- Area: devteam
- Priority: 85
- Depends On: none
- Roadmap Item: 1
- Family: devteam
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

Add a CI job (or extend existing test-batch-close.ps1) that runs export, compares .devteam/exported/state/issues.json to .devteam/state/issues.json (or runs reconcile-batch-close.ps1 in dry-run), and fails the build if differences exist. This prevents regressions.

## Latest Run

(none)

## Recent Decisions

(none)