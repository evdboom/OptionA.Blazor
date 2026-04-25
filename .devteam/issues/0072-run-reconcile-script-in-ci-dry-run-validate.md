# Issue 0072: Run reconcile script in CI (dry-run + validate)

- Status: open
- Role: devops
- Area: ci
- Priority: 75
- Depends On: 0068
- Roadmap Item: 1
- Family: ci
- External: none
- Pipeline: none
- Pipeline Stage: none
- Planning Issue: no

## Detail

Add a CI job to run .devteam/scripts/reconcile-workspace-state.ps1 in dry-run mode, ensure Pester tests execute, and surface artifacts/logs. This will verify backups and idempotency in CI before enabling non-dry-run runs.

## Latest Run

(none)

## Recent Decisions

(none)